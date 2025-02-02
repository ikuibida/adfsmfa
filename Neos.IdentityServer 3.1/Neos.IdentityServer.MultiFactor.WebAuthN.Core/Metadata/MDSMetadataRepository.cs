﻿//******************************************************************************************************************************************************************************************//
// Copyright (c) 2022 @redhook62 (adfsmfa@gmail.com)                                                                                                                                        //                        
//                                                                                                                                                                                          //
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),                                       //
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,   //
// and to permit persons to whom the Software is furnished to do so, subject to the following conditions:                                                                                   //
//                                                                                                                                                                                          //
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.                                                           //
//                                                                                                                                                                                          //
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,                                      //
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,                            //
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.                               //
//                                                                                                                                                                                          //
//                                                                                                                                                                                          //
// https://github.com/neos-sdi/adfsmfa                                                                                                                                                      //
//                                                                                                                                                                                          //
//******************************************************************************************************************************************************************************************//
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Neos.IdentityServer.MultiFactor.WebAuthN.Metadata
{
    public class MDSMetadataRepository : BaseSystemMetadataRepository
    {
        protected readonly string _blobUrl;
        protected readonly HttpClient _httpClient;

        /// <summary>
        /// MDSMetadataRepository constructor
        /// </summary>
        public MDSMetadataRepository()
        {
            _blobUrl = SystemUtilities.PayloadDownloadBlobUrl;
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// GetBLOB method implmentation
        /// </summary>
        public override async Task<MetadataBLOBPayload> GetBLOB()
        {
            bool needrawblob = false;
            MetadataBLOBPayload result = null;
            BLOBPayloadInformations infos = new BLOBPayloadInformations();
            if (HasBLOBPayloadCache())
            {
                infos = GetBLOBPayloadCache();
                if (string.IsNullOrEmpty(infos.BLOB))
                    return null;
                int oldnumber = infos.Number;
                DateTime oldnextupdate = infos.NextUpdate;
                result = DeserializeAndValidateBlob(infos);                
                if ((infos.Number > oldnumber) || (Convert.ToDateTime(infos.NextUpdate) > Convert.ToDateTime(oldnextupdate)))
                   needrawblob = true;
            }
            else
                needrawblob = true;
            if (needrawblob)
            {
                infos.BLOB = await GetRawBlob();
                if (string.IsNullOrEmpty(infos.BLOB))
                    return null;
                result = DeserializeAndValidateBlob(infos);
                SetBLOBPayloadCache(infos);
            }
            return result;
        }

        /// <summary>
        /// GetRawBlob method implementation
        /// </summary>
        protected override async Task<string> GetRawBlob()
        {
            var url = _blobUrl;
            return await DownloadStringAsync(url);
        }

        /// <summary>
        /// DeserializeAndValidateBlob method implementation 
        /// </summary>
        protected override MetadataBLOBPayload DeserializeAndValidateBlob(BLOBPayloadInformations infos)
        {
              if (string.IsNullOrWhiteSpace(infos.BLOB))
                  throw new ArgumentNullException(nameof(infos.BLOB));

              var jwtParts = infos.BLOB.Split('.');

              if (jwtParts.Length != 3)
                  throw new ArgumentException("The JWT does not have the 3 expected components");

              var blobHeaderString = jwtParts.First();
              var blobHeader = JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(blobHeaderString)));

              var blobAlg = blobHeader["alg"]?.Value<string>();

              if (blobAlg == null)
                  throw new ArgumentNullException("No alg value was present in the BLOB header.");

              JArray x5cArray = blobHeader["x5c"] as JArray;

              if (x5cArray == null)
                  throw new Exception("No x5c array was present in the BLOB header.");

              var keyStrings = x5cArray.Values<string>().ToList();

              if (keyStrings.Count == 0)
                  throw new ArgumentException("No keys were present in the BLOB header.");

              var rootCert = GetX509Certificate(ROOT_CERT);
              var blobCerts = keyStrings.Select(o => GetX509Certificate(o)).ToArray();

              var keys = new List<SecurityKey>();

              foreach (var certString in keyStrings)
              {
                  var cert = GetX509Certificate(certString);

                  var ecdsaPublicKey = cert.GetECDsaPublicKey();
                  if (ecdsaPublicKey != null)
                  {
                      keys.Add(new ECDsaSecurityKey(ecdsaPublicKey));
                      continue;
                  }

                  var rsaPublicKey = cert.GetRSAPublicKey();
                  if (rsaPublicKey != null)
                  {
                      keys.Add(new RsaSecurityKey(rsaPublicKey));
                      continue;
                  }
                  throw new MetadataException("Unknown certificate algorithm");
              }
              var blobPublicKeys = keys.ToArray();

              var certChain = new X509Chain();
              certChain.ChainPolicy.ExtraStore.Add(rootCert);
              certChain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;

              var validationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = false,
                  ValidateAudience = false,
                  ValidateLifetime = false,
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKeys = blobPublicKeys,
              };

              var tokenHandler = new JwtSecurityTokenHandler()
              {
                  // 250k isn't enough bytes for conformance test tool
                  // https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/issues/1097
                  MaximumTokenSizeInBytes = infos.BLOB.Length
              };

              tokenHandler.ValidateToken(infos.BLOB, validationParameters,out var validatedToken); 

              if (blobCerts.Length > 1)
              {
                 certChain.ChainPolicy.ExtraStore.AddRange(blobCerts.Skip(1).ToArray());
              }

              var certChainIsValid = certChain.Build(blobCerts.First());
               // if the root is trusted in the context we are running in, valid should be true here
              if (!certChainIsValid)
              {
                 foreach (var element in certChain.ChainElements)
                 {
                     if (element.Certificate.Issuer != element.Certificate.Subject)
                     {
                         var cdp = CryptoUtils.CDPFromCertificateExts(element.Certificate.Extensions);
                         var crlFile = DownloadData(cdp);
                         if (true == CryptoUtils.IsCertInCRL(crlFile, element.Certificate))
                             throw new VerificationException(string.Format("Cert {0} found in CRL {1}", element.Certificate.Subject, cdp));
                     }
                 }

                 // otherwise we have to manually validate that the root in the chain we are testing is the root we downloaded
                 if (rootCert.Thumbprint == certChain.ChainElements[certChain.ChainElements.Count - 1].Certificate.Thumbprint &&
                     // and that the number of elements in the chain accounts for what was in x5c plus the root we added
                     certChain.ChainElements.Count == (keyStrings.Count + 1) &&
                     // and that the root cert has exactly one status listed against it
                     certChain.ChainElements[certChain.ChainElements.Count - 1].ChainElementStatus.Length == 1 &&
                     // and that that status is a status of exactly UntrustedRoot
                     certChain.ChainElements[certChain.ChainElements.Count - 1].ChainElementStatus[0].Status == X509ChainStatusFlags.UntrustedRoot)
                 {
                     // if we are good so far, that is a good sign
                     certChainIsValid = true;
                     for (var i = 0; i < certChain.ChainElements.Count - 1; i++)
                     {
                         // check each non-root cert to verify zero status listed against it, otherwise, invalidate chain
                         if (0 != certChain.ChainElements[i].ChainElementStatus.Length)
                             certChainIsValid = false;
                     }
                 }
              }

              if (!certChainIsValid)
                 throw new VerificationException("Failed to validate cert chain while parsing BLOB");

              var blobPayload = ((JwtSecurityToken)validatedToken).Payload.SerializeToJson();

              var blob = Newtonsoft.Json.JsonConvert.DeserializeObject<MetadataBLOBPayload>(blobPayload);
              infos.Number = blob.Number;
              infos.NextUpdate = Convert.ToDateTime(blob.NextUpdate);
              infos.CanDownload = true; 
              blob.JwtAlg = blobAlg;
              return blob;
        }

        #region downloads
        /// <summary>
        /// DownloadStringAsync method implementation
        /// </summary>
        protected async Task<string> DownloadStringAsync(string url)
        {
            return await _httpClient.GetStringAsync(url);
        }

        /// <summary>
        /// DownloadData method implementation
        /// </summary>
        protected byte[] DownloadData(string url)
        {
            return _httpClient.GetByteArrayAsync(url).Result;
        }
        #endregion
    }
}
