using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace RemiBou.Blazor.BrowserInterop.Credentials
{
    /// <summary>
    /// holds the options passed to navigator.credentials.get() in order to fetch a given PublicKeyCredential.
    /// </summary>
    public class CredentialRequestOptionsPublicKey
    {
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
        /// indicates the relying party's identifier (ex. "login.example.org"). If this option is not provided, the client will use the current origin's domain.
        /// </summary>
        /// <value></value>
        public string RpId { get; set; }

        /// <summary>
        /// An Array of credentials descriptor which restricts the acceptable existing credentials for retrieval.
        /// </summary>
        /// <value></value>
        public List<PublicKeyCredentialFilter> AllowCredentials { get; set; }

        /// <summary>
        /// A string qualifying how the user verification should be part of the authentication process.
        /// </summary>
        /// <returns></returns>
        public string UserVerification { get => UserVerificationEnum.ToString().ToLower(); set => UserVerificationEnum = Enum.Parse<PublicKeyCredentialCreationOptionsAuthenticatorSelection.AuthenticatorSelectionUserVerificationEnum>(value); }

        /// <summary>
        /// A string qualifying how the user verification should be part of the authentication process.
        /// </summary>
        [JsonIgnore]
        public PublicKeyCredentialCreationOptionsAuthenticatorSelection.AuthenticatorSelectionUserVerificationEnum UserVerificationEnum { get; set; } = PublicKeyCredentialCreationOptionsAuthenticatorSelection.AuthenticatorSelectionUserVerificationEnum.Preferred;

        /// <summary>
        /// An object with several client extensions' inputs. Those extensions are used to request additional processing (e.g. dealing with legacy FIDO APIs credentials, prompting a specific text on the authenticator, etc.).
        /// </summary>
        /// <value></value>
        public PublicKeyCredentialCreationOptionsExtensions Extensions { get; set; }

    }
}
