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
using Neos.IdentityServer.MultiFactor.WebAuthN.Library.Cbor;
using System;
using System.IO;
using System.Linq;

namespace Neos.IdentityServer.MultiFactor.WebAuthN.Objects
{
    public class AuthenticatorData
    {
        /// <summary>
        /// Minimum length of the authenticator data structure.
        /// <see cref="https://www.w3.org/TR/webauthn/#sec-authenticator-data"/>
        /// </summary>
        private const int MinLength = SHA256HashLenBytes + sizeof(AuthenticatorFlags) + sizeof(UInt32);

        private const int SHA256HashLenBytes = 32; // 256 bits, 8 bits per byte

        /// <summary>
        /// SHA-256 hash of the RP ID the credential is scoped to.
        /// </summary>
        public byte[] RpIdHash;

        /// <summary>
        /// Flags contains information from the authenticator about the authentication 
        /// and whether or not certain data is present in the authenticator data.
        /// </summary>
        private readonly AuthenticatorFlags _flags;

        /// <summary>
        /// UserPresent indicates that the user presence test has completed successfully.
        /// <see cref="https://www.w3.org/TR/webauthn/#up"/>
        /// </summary>
        public bool UserPresent => _flags.HasFlag(AuthenticatorFlags.UP);

        /// <summary>
        /// UserVerified indicates that the user verification process has completed successfully.
        /// <see cref="https://www.w3.org/TR/webauthn/#uv"/>
        /// </summary>
        public bool UserVerified => _flags.HasFlag(AuthenticatorFlags.UV);

        /// <summary>
        /// HasAttestedCredentialData indicates that the authenticator added attested credential data to the authenticator data.
        /// <see cref="https://www.w3.org/TR/webauthn/#attested-credential-data"/>
        /// </summary>
        public bool HasAttestedCredentialData => _flags.HasFlag(AuthenticatorFlags.AT);

        /// <summary>
        /// HasExtensionsData indicates that the authenticator added extension data to the authenticator data.
        /// <see cref="https://www.w3.org/TR/webauthn/#authdataextensions"/>
        /// </summary>
        public bool HasExtensionsData => _flags.HasFlag(AuthenticatorFlags.ED);

        /// <summary>
        /// Signature counter, 32-bit unsigned big-endian integer. 
        /// </summary>
        public uint SignCount;

        /// <summary>
        /// Attested credential data is a variable-length byte array added to the 
        /// authenticator data when generating an attestation object for a given credential.
        /// </summary>
        public AttestedCredentialData AttestedCredentialData;

        /// <summary>
        /// Optional extensions to suit particular use cases.
        /// </summary>
        public Extensions Extensions;

        public AuthenticatorData(byte[] rpIdHash, AuthenticatorFlags flags, uint signCount, AttestedCredentialData acd, Extensions exts)
        {
            RpIdHash = rpIdHash;
            _flags = flags;
            SignCount = signCount;
            AttestedCredentialData = acd;
            Extensions = exts;
        }

        public AuthenticatorData(byte[] authData)
        {
            // Input validation
            if (authData == null)
                throw new VerificationException("Authenticator data cannot be null");
            if (authData.Length < MinLength)
                throw new VerificationException($"Authenticator data is less than the minimum structure length of {MinLength}");

            // Input parsing
            using (var stream = new MemoryStream(authData, false))
            {
                using (var reader = new BinaryReader(stream))
                {
                    RpIdHash = reader.ReadBytes(SHA256HashLenBytes);

                    _flags = (AuthenticatorFlags)reader.ReadByte();

                    var signCountBytes = reader.ReadBytes(sizeof(uint));
                    if (BitConverter.IsLittleEndian)
                    {
                        // Sign count is provided by the authenticator as big endian, convert if we are on little endian system
                        signCountBytes = signCountBytes.Reverse().ToArray();
                    }
                    SignCount = BitConverter.ToUInt32(signCountBytes, 0);

                    // Attested credential data is only present if the AT flag is set
                    if (HasAttestedCredentialData)
                    {
                        // Decode attested credential data, which starts at the next byte past the minimum length of the structure.
                        AttestedCredentialData = new AttestedCredentialData(reader);
                    }

                    // Extensions data is only present if the ED flag is set
                    if (HasExtensionsData)
                    {

                        // "CBORObject.Read: This method will read from the stream until the end 
                        // of the CBOR object is reached or an error occurs, whichever happens first."
                        //
                        // Read the CBOR object from the stream
                        var ext = CBORObject.Read(reader.BaseStream);

                        // Encode the CBOR object back to a byte array.
                        Extensions = new Extensions(ext.EncodeToBytes());
                    }
                    // There should be no bytes left over after decoding all data from the structure
                    if (stream.Position != stream.Length)
                        throw new VerificationException("Leftover bytes decoding AuthenticatorData");
                }
            }
        }

        public byte[] ToByteArray()
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms))
                {
                    writer.Write(RpIdHash);

                    writer.Write((byte)_flags);

                    var signCount = BitConverter.GetBytes(SignCount);
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(signCount);
                    }
                    writer.Write(signCount);

                    if (HasAttestedCredentialData)
                    {
                        writer.Write(AttestedCredentialData.ToByteArray());
                    }

                    if (HasExtensionsData)
                    {
                        writer.Write(Extensions.GetBytes());
                    }
                }
                return ms.ToArray();
            }
        }
    }
}
