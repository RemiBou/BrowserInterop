using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace RemiBou.Blazor.BrowserInterop.Credentials
{
    /// <summary>
    /// Describes the user account for which the credentials are generated 
    /// </summary>
    public class PublicKeyCredentialCreationOptionsUser
    {
        [Obsolete("THis is only for Serialization")]
        public PublicKeyCredentialCreationOptionsUser()
        {

        }


        public PublicKeyCredentialCreationOptionsUser(string idStr, string name, string displayName)
        {
            IdStr = idStr ?? throw new ArgumentNullException(nameof(idStr));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
        }

        /// <summary>
        ///  an image resource which can be the avatar image for the user.
        /// </summary>
        /// <value></value>
        public string Icon { get; set; }

        /// <summary>
        /// This an opaque identifier which can be used by the authenticator to link the user account with its corresponding credentials. This value will later be used when fetching the credentials
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
}
