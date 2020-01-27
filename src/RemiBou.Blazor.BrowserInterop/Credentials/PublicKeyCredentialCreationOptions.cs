using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace RemiBou.Blazor.BrowserInterop.Credentials
{
    public class PublicKeyCredentialCreationOptions
    {
        [Obsolete("THis is only for Serialization")]
        public PublicKeyCredentialCreationOptions()
        {
        }

        public PublicKeyCredentialCreationOptions(PublicKeyCredentialCreationOptionsRelyingPary rp, PublicKeyCredentialCreationOptionsUser user, string challengeStr, List<PublicKeyCredentialsCreationPubKeyCredParam> pubKeyCredParams)
        {
            Rp = rp ?? throw new ArgumentNullException(nameof(rp));
            User = user ?? throw new ArgumentNullException(nameof(user));
            ChallengeStr = challengeStr ?? throw new ArgumentNullException(nameof(challengeStr));
            PubKeyCredParams = pubKeyCredParams ?? throw new ArgumentNullException(nameof(pubKeyCredParams));

        }

        /// <summary>
        ///  Describes the relying party which requested the credential creation.
        /// </summary>
        /// <value></value>
        public PublicKeyCredentialCreationOptionsRelyingPary Rp { get; set; }

        /// <summary>
        /// Describes the user account for which the credentials are generated 
        /// </summary>
        /// <value></value>
        public PublicKeyCredentialCreationOptionsUser User { get; set; }

        /// <summary>
        /// Value emitted by the relying party's server and used as a cryptographic challenge. This value will be signed by the authenticator and the signature will be sent back as part of AuthenticatorAttestationResponse.attestationObject.
        /// </summary>
        /// <value></value>
        public ushort[] Challenge { get; set; }

        /// <summary>
        /// Provide an easy way for setting the Challenge property from a Base64 string
        /// </summary>
        /// <value></value>
        [JsonIgnore]

        public string ChallengeStr
        {
            get
            {
                return System.Convert.ToBase64String(Challenge.Select(u => Convert.ToByte(u)).ToArray());
            }
            set
            {
                Challenge = System.Convert.FromBase64String(value).Select(b => Convert.ToUInt16(b)).ToArray();
            }
        }
        /// <summary>
        /// Describes the desired features of the credential to be created. These objects define the type of public-key and the algorithm used for cryptographic signature operations.
        /// </summary>
        /// <value></value>
        public List<PublicKeyCredentialsCreationPubKeyCredParam> PubKeyCredParams { get; set; }

        /// <summary>
        /// A numerical hint, in milliseconds, which indicates the time the caller is willing to wait for the creation operation to complete. This hint may be overridden by the browser.
        /// </summary>
        /// <value></value>
        public double Timeout { get; set; } = 6000;

        /// <summary>
        /// Provide an easy way for setting the timeout as a TImeSpan
        /// </summary>
        /// <returns></returns>
        [JsonIgnore]
        public TimeSpan TimeoutTimeSpan { get => TimeSpan.FromMilliseconds(Timeout); set => Timeout = value.TotalMilliseconds; }

        /// <summary>
        /// elements are descriptors for the public keys already existing for a given user. This is provided by the relying party's server if it wants to prevent creation of new credentials for an existing user.
        /// </summary>
        /// <value></value>
        public List<PublicKeyCredentialFilter> ExcludeCredentials { get; set; }

        /// <summary>
        /// An object whose properties are criteria used to filter out the potential authenticators for the creation operation.
        /// </summary>
        /// <value></value>
        public PublicKeyCredentialCreationOptionsAuthenticatorSelection AuthenticatorSelection { get; set; }


        /// <summary>
        /// Indicates how the attestation (for the authenticator's origin) should be transported.
        /// </summary>
        /// <value></value>      
        public string Attestation { get => AttestationEnum.ToString().ToLower(); set => AttestationEnum = Enum.Parse<PublicKeyCredentialCreationOptionsAttestationEnum>(value); }

        /// <summary>
        /// Indicates how the attestation (for the authenticator's origin) should be transported.
        /// </summary>
        /// <value></value>            
        [JsonIgnore]
        public PublicKeyCredentialCreationOptionsAttestationEnum AttestationEnum { get; set; } = PublicKeyCredentialCreationOptionsAttestationEnum.None;

        /// <summary>
        /// An object with several client extensions' inputs. Those extensions are used to request additional processing (e.g. dealing with legacy FIDO APIs credentials, prompting a specific text on the authenticator, etc.).
        /// </summary>
        /// <value></value>
        public PublicKeyCredentialCreationOptionsExtensions Extensions { get; set; }

        public enum PublicKeyCredentialCreationOptionsAttestationEnum
        {
            ///<summary>
            ///the relying party is not interested in this attestation. This avoids making a check with the attestation certificate authority and asking the user consent for sharing identifying information.
            ///</summary>
            None,
            ///<summary>
            ///the client may change the assertion from the authenticator (for instance, using an anonymization CA). This value is used when the relying party wishes to verify the attestation.
            ///</summary>
            Indirect,
            ///<summary>the relying party wants to receive the attestation as generated by the authenticator.</summary>
            Direct
        }
    }
}
