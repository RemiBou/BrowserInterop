using System.Text.Json.Serialization;

namespace RemiBou.Blazor.BrowserInterop.Credentials
{
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
        /// Easy way for accessing AuthnSel from Base64 string
        /// </summary>
        /// <returns></returns>
        [JsonIgnore]
        public string AuthnSelStr { get => System.Convert.ToBase64String(AuthnSel); set => AuthnSel = System.Convert.FromBase64String(value); }

        /// <summary>
        /// Supported extensions. If true, the client outputs an array of strings containing the extensions which are supported by the authenticator.
        /// </summary>
        /// <value></value>
        public bool Exts { get; set; }

        /// <summary>
        /// 	User verification index. If true, the client outputs an ArrayBuffer which contains a value uniquely identifying a user verification data record. In other words, this may be used server side to check if the current operation is based on the same biometric data that the previous authentication.
        /// </summary>
        /// <value></value>
        public bool Uvi { get; set; }
        /// <summary>
        /// Location. If true, the client outputs a Coordinates object representing the geolocation of the authenticator.
        /// </summary>
        /// <value></value>
        public bool Loc { get; set; }

        /// <summary>
        /// 	User verification method. If true, the client outputs an array of arrays with 3 values containing information about how the user was verified (e.g. fingerprint, pin, pattern), how the key is protected, how the matcher (tool used for the authentication operation) is protected.
        /// </summary>
        /// <value></value>
        public bool Uvm { get; set; }


        /// <summary>
        /// Biometric authenticator performance bounds. The client must not use any authenticator with false acceptance rate (FAR) and false rejection rate (FRR) below the inputs. The client outputs true if this was taken into account.
        /// </summary>
        /// <value></value>
        public PublicKeyCredentialCreationOptionsExtensionsBiometricPerfBounds MyProperty { get; set; }

    }
}
