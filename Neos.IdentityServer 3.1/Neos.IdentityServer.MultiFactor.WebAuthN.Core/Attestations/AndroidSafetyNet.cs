﻿//******************************************************************************************************************************************************************************************//
// Copyright (c) 2021 abergs (https://github.com/abergs/fido2-net-lib)                                                                                                                      //                        
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
//******************************************************************************************************************************************************************************************//
// Copyright (c) 2022 @redhook62 (adfsmfa@gmail.com)                                                                                                                                    //                        
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
//                                                                                                                                                             //
// https://github.com/neos-sdi/adfsmfa                                                                                                                                                      //
//                                                                                                                                                                                          //
//******************************************************************************************************************************************************************************************//
using Microsoft.IdentityModel.Tokens;
using Neos.IdentityServer.MultiFactor.WebAuthN.Library.Cbor;
using Neos.IdentityServer.MultiFactor.WebAuthN.Objects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Neos.IdentityServer.MultiFactor.WebAuthN.AttestationFormat
{
    internal sealed class AndroidSafetyNet : AttestationVerifier
    {
        private double _driftTolerance = 0;

        public AndroidSafetyNet(int driftTolerance = 30000)
        {
            _driftTolerance = driftTolerance;
        }

        private X509Certificate2 GetX509Certificate(string certString)
        {
            try
            {
                var certBytes = Convert.FromBase64String(certString);
                return new X509Certificate2(certBytes);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Could not parse X509 certificate.", ex);
            }
        }

        public override (AttestationType, X509Certificate2[]) Verify()
        {
            // 1. Verify that attStmt is valid CBOR conforming to the syntax defined above and perform 
            // CBOR decoding on it to extract the contained fields
            // (handled in base class)
            if ((CBORType.TextString != attStmt["ver"].Type) ||
                (0 == attStmt["ver"].AsString().Length))
                throw new VerificationException("Invalid version in SafetyNet data");

            // 2. Verify that response is a valid SafetyNet response of version ver
            var ver = attStmt["ver"].AsString();

            if ((CBORType.ByteString != attStmt["response"].Type) ||
                (0 == attStmt["response"].GetByteString().Length))
                throw new VerificationException("Invalid response in SafetyNet data");

            var response = attStmt["response"].GetByteString();
            var responseJWT = Encoding.UTF8.GetString(response);

            if (string.IsNullOrWhiteSpace(responseJWT))
                throw new VerificationException("SafetyNet response null or whitespace");

            var jwtParts = responseJWT.Split('.');

            if (jwtParts.Length != 3)
                throw new VerificationException("SafetyNet response JWT does not have the 3 expected components");

            var jwtHeaderString = jwtParts.First();
            var jwtHeaderJSON = JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(jwtHeaderString)));

            JArray x5cArray = jwtHeaderJSON["x5c"] as JArray;

            if (x5cArray == null)
                throw new VerificationException("SafetyNet response JWT header missing x5c");
            var x5cStrings = x5cArray.Values<string>().ToList();

            if (x5cStrings.Count == 0)
                throw new VerificationException("No keys were present in the TOC header in SafetyNet response JWT");

            var certs = new List<X509Certificate2>();
            var keys = new List<SecurityKey>();

            foreach (var certString in x5cStrings)
            {
                var cert = GetX509Certificate(certString);
                certs.Add(cert);

                var ecdsaPublicKey = cert.GetECDsaPublicKey();
                if (ecdsaPublicKey != null)
                    keys.Add(new ECDsaSecurityKey(ecdsaPublicKey));

                var rsaPublicKey = cert.GetRSAPublicKey();
                if (rsaPublicKey != null)
                    keys.Add(new RsaSecurityKey(rsaPublicKey));
            }

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = keys
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken = null;
            try
            {
                tokenHandler.ValidateToken(
                    responseJWT,
                    validationParameters,
                    out validatedToken);
            }
            catch (SecurityTokenException ex)
            {
                throw new VerificationException("SafetyNet response security token validation failed", ex);
            }

            var nonce = "";
            bool? ctsProfileMatch = null;
            var timestampMs = DateTimeHelper.UnixEpoch;

            var jwtToken = validatedToken as JwtSecurityToken;

            foreach (var claim in jwtToken.Claims)
            {
                if (("nonce" == claim.Type) && ("http://www.w3.org/2001/XMLSchema#string" == claim.ValueType) && (0 != claim.Value.Length))
                    nonce = claim.Value;
                if (("ctsProfileMatch" == claim.Type) && ("http://www.w3.org/2001/XMLSchema#boolean" == claim.ValueType))
                {
                    ctsProfileMatch = bool.Parse(claim.Value);
                }
                if (("timestampMs" == claim.Type) && ("http://www.w3.org/2001/XMLSchema#integer64" == claim.ValueType))
                {
                    timestampMs = DateTimeHelper.UnixEpoch.AddMilliseconds(double.Parse(claim.Value));
                }
            }

            var notAfter = DateTime.UtcNow.AddMilliseconds(_driftTolerance);
            var notBefore = DateTime.UtcNow.AddMinutes(-1).AddMilliseconds(-(_driftTolerance));
            if ((notAfter < timestampMs) || ((notBefore) > timestampMs))
            {
                throw new VerificationException(string.Format("SafetyNet timestampMs must be present and between one minute ago and now, got: {0}", timestampMs.ToString()));
            }

            // 3. Verify that the nonce in the response is identical to the SHA-256 hash of the concatenation of authenticatorData and clientDataHash
            if ("" == nonce)
                throw new VerificationException("Nonce value not found in SafetyNet attestation");

            byte[] nonceHash = null;
            try
            {
                nonceHash = Convert.FromBase64String(nonce);
            }
            catch (Exception ex)
            {
                throw new VerificationException("Nonce value not base64string in SafetyNet attestation", ex);
            }

            using (var hasher = CryptoUtils.GetHasher(HashAlgorithmName.SHA256))
            {
                var dataHash = hasher.ComputeHash(Data);
                if (false == dataHash.SequenceEqual(nonceHash))
                    throw new VerificationException(
                        string.Format(
                            "SafetyNet response nonce / hash value mismatch, nonce {0}, hash {1}",
                            BitConverter.ToString(nonceHash).Replace("-", ""),
                            BitConverter.ToString(dataHash).Replace("-", "")
                            )
                        );
            }

            // 4. Let attestationCert be the attestation certificate
            var attestationCert = certs[0];
            var subject = attestationCert.GetNameInfo(X509NameType.DnsName, false);

            // 5. Verify that the attestation certificate is issued to the hostname "attest.android.com"
            if (false == ("attest.android.com").Equals(subject))
                throw new VerificationException(string.Format("SafetyNet attestation cert DnsName invalid, want {0}, got {1}", "attest.android.com", subject));

            // 6. Verify that the ctsProfileMatch attribute in the payload of response is true
            if (null == ctsProfileMatch)
                throw new VerificationException("SafetyNet response ctsProfileMatch missing");

            if (true != ctsProfileMatch)
                throw new VerificationException("SafetyNet response ctsProfileMatch false");

            return (AttestationType.Basic, new X509Certificate2[] { attestationCert });
        }
    }
}
