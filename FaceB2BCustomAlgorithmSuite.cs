using System;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security;

    public class FaceB2BCustomAlgorithmSuite : SecurityAlgorithmSuite
    {
        public override string DefaultAsymmetricKeyWrapAlgorithm
        {
            get { return SecurityAlgorithms.RsaV15KeyWrap; } //
        }

        public override string DefaultAsymmetricSignatureAlgorithm
        {
            get { return SecurityAlgorithms.RsaSha256Signature; }
        }

        public override string DefaultCanonicalizationAlgorithm
        {
            get { return SecurityAlgorithms.ExclusiveC14n; }
        }

        public override string DefaultDigestAlgorithm
        {
            get { return SecurityAlgorithms.Sha256Digest; }
        }

        public override string DefaultEncryptionAlgorithm
        {
            get { return SecurityAlgorithms.Aes128Encryption; }
        }

        public override int DefaultEncryptionKeyDerivationLength
        {
            get { return 128; }
        }

        public override int DefaultSignatureKeyDerivationLength
        {
            get { return 256; }
        }

        public override int DefaultSymmetricKeyLength
        {
            get { return 256; }
        }

        public override string DefaultSymmetricKeyWrapAlgorithm
        {
            get { return SecurityAlgorithms.Aes128KeyWrap; }
        }

        public override string DefaultSymmetricSignatureAlgorithm
        {
            get { return SecurityAlgorithms.HmacSha256Signature; }
        }

        public override bool IsAsymmetricKeyLengthSupported(int length)
        {
            return length >= 1024 && length <= 4096;
        }

        public override bool IsSymmetricKeyLengthSupported(int length)
        {
            return length >= 128 && length <= 256;
        }
    }
