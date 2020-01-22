using System.Collections.Generic;
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
        public async Task<FederatedCredential> CreateFederatedCredential(string id, string name, string iconURL, string provider, string protocol)
        {
            return await jsRuntime.InvokeAsync<FederatedCredential>("browserInterop.credential.create",
            new
            {
                Password = new
                {
                    id,
                    name,
                    iconURL,
                    provider,
                    protocol
                }
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

            /// <summary>
            /// credential's federated identity protocol.
            /// </summary>
            /// <value></value>
            public string Protocol { get; set; }

            public string IconURL { get; set; }

            public string Name { get; set; }
        }
    }
}