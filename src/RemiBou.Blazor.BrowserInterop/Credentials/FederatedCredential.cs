namespace RemiBou.Blazor.BrowserInterop.Credentials
{
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
}
