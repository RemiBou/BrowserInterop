namespace RemiBou.Blazor.BrowserInterop.Credentials
{
    /// <summary>
    /// The Credential abstract class  of the the Credential Management API provides information about an entity as a prerequisite to a trust decision.
    /// </summary>
    public abstract class Credential
    {
        /// <summary>
        /// Contains the credential's identifier. This might be any one of a GUID, username, or email address.
        /// </summary>
        /// <value></value>
        public string Id { get; set; }

        /// <summary>
        /// Contains the credential's type. Valid values are 'password', 'federated' and 'public-key'
        /// </summary>
        /// <value></value>
        public string Type { get; set; }
    }
}
