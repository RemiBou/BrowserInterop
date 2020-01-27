using System;
using System.Text.Json.Serialization;

namespace RemiBou.Blazor.BrowserInterop.Credentials
{
    public class PublicKeyCredentialCreationOptionsAuthenticatorSelection
    {
        /// <summary>
        /// A string which is either "platform" or "cross-platform". The former describes an authenticator which is bound to the client and which is generally not removable. The latter describes a device which may be used across different platform (such as a USB or NFC device).
        /// </summary>
        /// <value></value>
        public string AuthenticatorAttachement { get; set; }

        /// <summary>
        /// Easy way to setup Authenticator attachment
        /// </summary>
        /// <value></value>
        [JsonIgnore]
        public bool AuthenticatorAttachementIsCrossPlatform { get => AuthenticatorAttachement == "cross-platform"; set => AuthenticatorAttachement = value ? "cross-platform" : "platform"; }

        /// <summary>
        /// A boolean which indicated that the credential private key must be stored in the authenticator, the client or in a client device. The default value is false.
        /// </summary>
        /// <value></value>
        public bool RequireResidentKey { get; set; }

        /// <summary>
        /// A string qualifying how the user verification should be part of the authentication process.
        /// </summary>
        /// <returns></returns>
        public string UserVerification { get => UserVerificationEnum.ToString().ToLower(); set => UserVerificationEnum = Enum.Parse<AuthenticatorSelectionUserVerificationEnum>(value); }

        /// <summary>
        /// A string qualifying how the user verification should be part of the authentication process.
        /// </summary>
        [JsonIgnore]
        public AuthenticatorSelectionUserVerificationEnum UserVerificationEnum { get; set; } = AuthenticatorSelectionUserVerificationEnum.Preferred;

        public enum AuthenticatorSelectionUserVerificationEnum
        {
            ///<summary>user verification is required, the operation will fail if the response does not have the UV flag (as part of the authenticatorData property of AuthenticatorAttestationResponse.attestationObject)</summary>
            Required,
            ///<summary>user verification is prefered, the operation will not fail if the response does not have the UV flag (as part of the authenticatorData property of AuthenticatorAttestationResponse.attestationObject)</summary>
            Preferred,
            ///<summary>user verification should not be employed as to minimize the user interaction during the process.</summary>
            Discouraged
        }
    }
}
