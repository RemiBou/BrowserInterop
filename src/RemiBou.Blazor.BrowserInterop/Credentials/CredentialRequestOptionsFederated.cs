using System.Collections.Generic;

namespace RemiBou.Blazor.BrowserInterop.Credentials
{
    /// <summary>
    /// Contains requirements for returned federated credentials. 
    /// </summary>
    public class CredentialRequestOptionsFederated
    {
        /// <summary>
        ///  identity providers to search for.
        /// </summary>
        /// <value></value>
        public List<string> Providers { get; set; }

        /// <summary>
        /// federation protocols to search for.
        /// </summary>
        /// <value></value>
        public List<string> Protocols { get; set; }
    }
}
