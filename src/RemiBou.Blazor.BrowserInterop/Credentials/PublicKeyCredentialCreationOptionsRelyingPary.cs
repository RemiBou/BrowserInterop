using System;

namespace RemiBou.Blazor.BrowserInterop.Credentials
{
    /// <summary>
    /// Describes the relying party which requested the credential creation
    /// </summary>
    public class PublicKeyCredentialCreationOptionsRelyingPary
    {
        [Obsolete("THis is only for Serialization")]
        public PublicKeyCredentialCreationOptionsRelyingPary()
        {
        }

        public PublicKeyCredentialCreationOptionsRelyingPary(string id, string name)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

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
