using System;

namespace RemiBou.Blazor.BrowserInterop.Credentials
{
    /// <summary>
    /// Describes the desired features of the credential to be created. These objects define the type of public-key and the algorithm used for cryptographic signature operations.
    /// </summary>
    public class PublicKeyCredentialsCreationPubKeyCredParam
    {
        [Obsolete("THis is only for Serialization")]
        public PublicKeyCredentialsCreationPubKeyCredParam()
        {
        }

        public PublicKeyCredentialsCreationPubKeyCredParam(int alg, string type = "public-key")
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Alg = alg;
        }

        /// <summary>
        /// A string describing type of public-key credential to be created. As of this writing (March 2019), only "public-key" may be used.
        /// </summary>
        /// <value></value>
        public string Type { get; set; } = "public-key";

        /// <summary>
        /// A numeric identifier for the algorithm to be used to generate the key pair. The links between identifier and algorithms are defined in this IANA registry (e.g. -7 indicates the elliptic curve algorithm ECDSA with SHA-256).
        /// </summary>
        /// <value></value>
        public int Alg { get; set; }
    }
}
