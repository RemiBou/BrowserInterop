using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace RemiBou.Blazor.BrowserInterop
{
    /// <summary>
    /// exposes methods to request credentials and notify the user agent when events such as successful sign in or sign out happen
    /// To understand it better read https://techcommunity.microsoft.com/t5/identity-standards-blog/to-understand-webauthn-read-credman/ba-p/339652
    /// </summary>
    public class CredentialsContainer
    {
        private IJSRuntime jsRuntime;

        internal CredentialsContainer(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Creates a new PasswordCredential instance 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public async Task<PasswordCredential> CreatePassword(string id, string name, string iconURL, string password)
        {
            return await jsRuntime.InvokeAsync<PasswordCredential>("browserInterop.credential.create",
            new
            {
                Password = new
                {
                    id,
                    name,
                    iconURL,
                    password
                }
            });
        }

        /// <summary>
        /// Creates a new FederatedCredential instance 
        /// </summary>
        /// <returns></returns>
        public async Task<FederatedCredential> CreateFederatedCredential(string id, string name, string iconURL, string provider)
        {
            return await jsRuntime.InvokeAsync<FederatedCredential>("browserInterop.credential.create",
            new
            {
                Federated = new
                {
                    id,
                    name,
                    iconURL,
                    provider
                }
            });
        }

        public async Task<PublicKeyCredential> CreateüblicKeyCredential(PublicKeyCredentialCreationOptions publicKeyCredentialCreationOptions)
        {
            return await jsRuntime.InvokeAsync<PublicKeyCredential>("browserInterop.credential.create",
           new
           {
               PublicKey = publicKeyCredentialCreationOptions
           });
        }

        public class Credential
        {
            public string Id { get; set; }
            public string Type { get; set; }
        }

        public class PasswordCredential : Credential
        {
            /// <summary>
            /// The data in the objects will be added to the request body and sent to the remote endpoint with the credentials.
            /// </summary>
            /// <value></value>
            public Dictionary<string, string> AdditionalData { get; set; }

            /// <summary>
            /// URL pointing to an image for an icon. This image is intended for display in a credential chooser. The URL must be accessible without authentication.
            /// </summary>
            /// <value></value>
            public string IconURL { get; set; }

            /// <summary>
            /// the name that will be used for the ID field when submitting the current object to a remote endpoint via fetch. This property defaults to 'username', but may be overridden to match whatever the backend service expects.
            /// </summary>
            /// <value></value>
            public string IdName { get; set; }

            /// <summary>
            ///  a human-readable public name for display in a credential chooser.
            /// </summary>
            /// <value></value>
            public string Name { get; set; }

            /// <summary>
            ///  the password of the credential.
            /// </summary>
            /// <value></value>
            public string Password { get; set; }

            /// <summary>
            ///  the name that will be used for the password field when submitting the current object to a remote endpoint via fetch. This property defaults to 'password', but may be overridden to match whatever the backend service expects.
            /// </summary>
            /// <value></value>
            public string PasswordName { get; set; }
        }

        public class FederatedCredential : Credential
        {
            /// <summary>
            ///  credential's federated identity provider.
            /// </summary>
            /// <value></value>
            public string Provider { get; set; }

            public string IconURL { get; set; }

            public string Name { get; set; }
        }

        public class PublicKeyCredential
        {
        }

        public class PublicKeyCredentialCreationOptions
        {
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
            public byte[] Challenge { get; set; }

            /// <summary>
            /// Provide an easy way for setting the Challenge property from a UTF8 string
            /// </summary>
            /// <value></value>

            public string ChallengeStr
            {
                get
                {
                    return Encoding.UTF8.GetString(Challenge);
                }
                set
                {
                    Challenge = Encoding.UTF8.GetBytes(value);
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
            public double Timeout { get; set; }

            /// <summary>
            /// Provide an easy way for setting the timeout as a TImeSpan
            /// </summary>
            /// <returns></returns>
            public TimeSpan TimeoutTimeSpan { get => TimeSpan.FromMilliseconds(Timeout); set => Timeout = value.TotalMilliseconds; }

            /// <summary>
            /// elements are descriptors for the public keys already existing for a given user. This is provided by the relying party's server if it wants to prevent creation of new credentials for an existing user.
            /// </summary>
            /// <value></value>
            public List<PublicKeyCredentialCreationOptionsExcludeCredentials> ExcludeCredentials { get; set; }

            /// <summary>
            /// An object whose properties are criteria used to filter out the potential authenticators for the creation operation.
            /// </summary>
            /// <value></value>
            public PublicKeyCredentialCreationOptionsAuthenticatorSelection AuthenticatorSelection { get; set; }

            /// <summary>
            /// A String which indicates how the attestation (for the authenticator's origin) should be transported.
            /// </summary>
            /// <value></value>
            public string Attestation { get; set; }

            /// <summary>
            /// An object with several client extensions' inputs. Those extensions are used to request additional processing (e.g. dealing with legacy FIDO APIs credentials, prompting a specific text on the authenticator, etc.).
            /// </summary>
            /// <value></value>
            public PublicKeyCredentialCreationOptionsExtensions Extensions { get; set; }
        }

        /// <summary>
        /// An object with several client extensions' inputs. Those extensions are used to request additional processing (e.g. dealing with legacy FIDO APIs credentials, prompting a specific text on the authenticator, etc.).
        /// </summary>
        public class PublicKeyCredentialCreationOptionsExtensions
        {
            /// <summary>
            /// Authenticator selection. Restricts the list of authenticator models which may be used. If no matching authenticator is available, the credential is still generated with another available authenticator.
            /// </summary>
            /// <value></value>
            public byte[] AuthnSel { get; set; }

            /// <summary>
            /// Easy way for accessing AuthnSel from UTF8 string
            /// </summary>
            /// <returns></returns>
            public string AuthnSelStr { get => Encoding.UTF8.GetString(AuthnSel); set => AuthnSel = Encoding.UTF8.GetBytes(value); }

            /// <summary>
            /// Supported extensions. If true, the client outputs an array of strings containing the extensions which are supported by the authenticator.
            /// </summary>
            /// <value></value>
            public bool Exts { get; set; }
        }

        /// <summary>
        /// descriptor for the public keys already existing for a given user. This is provided by the relying party's server if it wants to prevent creation of new credentials for an existing user.
        /// </summary>
        public class PublicKeyCredentialCreationOptionsExcludeCredentials
        {
            /// <summary>
            /// A string describing type of public-key credential to be created. As of this writing (March 2019), only "public-key" may be used.
            /// </summary>
            /// <value></value>
            public string Type { get; set; } = "public-key";

            /// <summary>
            /// Matches an existing public key credential identifier (PublicKeyCredential.rawId). This identifier is generated during the creation of the PublicKeyCredential instance.
            /// </summary>
            /// <value></value>
            public byte[] Id { get; set; }

            /// <summary>
            /// Provide an easy way for setting the Id property from a UTF8 string
            /// </summary>
            /// <value></value>
            public string IdStr
            {
                get
                {
                    return Encoding.UTF8.GetString(Id);
                }
                set
                {
                    Id = Encoding.UTF8.GetBytes(value);
                }
            }

            /// <summary>
            /// Descriebs the possible transports between the client and the authenticator. 
            /// </summary>
            /// <value></value>
            public List<PublicKeyCredentialCreationOptionsExcludeCredentialsEnum> Transports { get; set; }

            public enum PublicKeyCredentialCreationOptionsExcludeCredentialsEnum
            {
                ///<summary> the authenticator can be contacted via a removable USB link</summary>
                Usb,
                ///<summary> the authenticator may be used over NFC (Near Field Communication)</summary>
                Nfc,
                ///<summary> the authenticator may be used over BLE (Bluetooth Low Energy)</summary>
                Ble,
                ///<summary> the authenticator is specifically bound to the client device (cannot be removed).</summary>
                Internal
            }

        }
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
            public bool AuthenticatorAttachementIsCrossPlatform { get => AuthenticatorAttachement == "cross-platform"; set => AuthenticatorAttachement = value ? "cross-platform" : "platform"; }

            /// <summary>
            /// A boolean which indicated that the credential private key must be stored in the authenticator, the client or in a client device. The default value is false.
            /// </summary>
            /// <value></value>
            public bool RequireResidentKey { get; set; }

            public AuthenticatorSelectionUserVerificationEnum UserVerification { get; set; } = AuthenticatorSelectionUserVerificationEnum.Preferred;

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

        /// <summary>
        /// Describes the desired features of the credential to be created. These objects define the type of public-key and the algorithm used for cryptographic signature operations.
        /// </summary>
        public class PublicKeyCredentialsCreationPubKeyCredParam
        {
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

        /// <summary>
        /// Describes the user account for which the credentials are generated 
        /// </summary>
        public class PublicKeyCredentialCreationOptionsUser
        {
            /// <summary>
            ///  an image resource which can be the avatar image for the user.
            /// </summary>
            /// <value></value>
            public string Icon { get; set; }

            /// <summary>
            /// This an opaque identifier which can be used by the authenticator to link the user account with its corresponding credentials. This value will later be used when fetching the credentials
            /// </summary>
            /// <value></value>
            public byte[] Id { get; set; }

            /// <summary>
            /// Provide an easy way for setting the Id property from a UTF8 string
            /// </summary>
            /// <value></value>
            public string IdStr
            {
                get
                {
                    return Encoding.UTF8.GetString(Id);
                }
                set
                {
                    Id = Encoding.UTF8.GetBytes(value);
                }
            }

            /// <summary>
            /// a human-readable name for the user's identifier (e.g. "jdoe@example.com").This property is intended for display and may be use to distinguish different account with the same displayName.
            /// </summary>
            /// <value></value>
            public string Name { get; set; }

            /// <summary>
            /// human readable and intended for display. It may be the full name of the user (e.g. "John Doe"). This is not intended to store the login of the user (see name below).
            /// </summary>
            /// <value></value>
            public string DisplayName { get; set; }
        }

        /// <summary>
        /// Describes the relying party which requested the credential creation
        /// </summary>
        public class PublicKeyCredentialCreationOptionsRelyingPary
        {
            /// <summary>
            /// points to an image resource which can be the logo/icon of the relying party.
            /// </summary>
            /// <value></value>
            public string Icon { get; set; }

            /// <summary>
            /// points to an image resource which can be the logo/icon of the relying party.
            /// </summary>
            /// <value></value>
            public string Id { get; set; }

            /// <summary>
            ///  a human-readable name for the relying party. This property is intended for display (e.g. "Example CORP").
            /// </summary>
            /// <value></value>
            public string Name { get; set; }
        }
    }
}