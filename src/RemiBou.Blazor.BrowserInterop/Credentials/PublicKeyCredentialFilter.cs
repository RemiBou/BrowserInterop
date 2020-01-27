using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace RemiBou.Blazor.BrowserInterop.Credentials
{
    /// <summary>
    /// descriptor for the public keys already existing for a given user. This is provided by the relying party's server if it wants to prevent creation of new credentials for an existing user.
    /// </summary>
    public class PublicKeyCredentialFilter
    {
        /// <summary>
        /// A string describing type of public-key credential to be created. As of this writing (March 2019), only "public-key" may be used.
        /// </summary>
        /// <value></value>
        public string Type { get; set; } = "public-key";

        /// <summary>0
        /// Matches an existing public key credential identifier (PublicKeyCredential.rawId). This identifier is generated during the creation of the PublicKeyCredential instance.
        /// </summary>
        /// <value></value>
        public ushort[] Id { get; set; }

        /// <summary>
        /// Provide an easy way for setting the Id property from a Base64 string
        /// </summary>
        /// <value></value>
        [JsonIgnore]
        public string IdStr
        {
            get
            {
                return System.Convert.ToBase64String(Id.Select(u => Convert.ToByte(u)).ToArray());
            }
            set
            {
                Id = System.Convert.FromBase64String(value).Select(b => Convert.ToUInt16(b)).ToArray();
            }
        }

        /// <summary>
        /// Describes the possible transports between the client and the authenticator. 
        /// </summary>
        /// <value></value>      
        public List<string> Transports { get => TransportsEnum.Select(t => t.ToString().ToLower()).ToList(); set => TransportsEnum = value?.Select(s => Enum.Parse<PublicKeyCredentialCreationOptionsExcludeCredentialsEnum>(s))?.ToList(); }

        /// <summary>
        /// Describes the possible transports between the client and the authenticator. 
        /// </summary>
        /// <value></value>
        public List<PublicKeyCredentialCreationOptionsExcludeCredentialsEnum> TransportsEnum { get; set; }

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
}
