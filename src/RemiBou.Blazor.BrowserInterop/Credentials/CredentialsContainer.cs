using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace RemiBou.Blazor.BrowserInterop.Credentials
{
    /// <summary>
    /// exposes methods to request credentials and notify the user agent when events such as successful sign in or sign out happen
    /// To understand it better read https://techcommunity.microsoft.com/t5/identity-standards-blog/to-understand-webauthn-read-credman/ba-p/339652
    /// </summary>
    public class CredentialsContainer
    {
        private readonly IJSRuntime jsRuntime;

        internal CredentialsContainer(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }


        /// <summary>
        /// Return true if credentail API is supported by the current browser
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsSupported()
        {
            return await jsRuntime.HasProperty("navigator.credentials");
        }



        /// <summary>
        /// Creates a new PasswordCredential instance 
        /// </summary>
        /// <param name="id">Mandatory</param>
        /// <param name="password">Mandatory</param>
        /// <param name="name"></param>
        /// <param name="iconURL"></param>
        /// <returns></returns>
        public async Task<PasswordCredential> CreatePassword(string id, string password, string name = null, string iconURL = null)
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
        ///  Creates a new FederatedCredential instance 
        /// </summary>
        /// <param name="id">Mandatory</param>
        /// <param name="provider">Mandatory</param>
        /// <param name="name"></param>
        /// <param name="iconURL"></param>
        /// <returns></returns>
        public async Task<FederatedCredential> CreateFederatedCredential(string id, string provider, string name = null, string iconURL = null)
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
                }
            );
        }

        public async Task<PublicKeyCredential> CreatePublicKeyCredential(PublicKeyCredentialCreationOptions publicKeyCredentialCreationOptions)
        {
            if (publicKeyCredentialCreationOptions is null)
            {
                throw new ArgumentNullException(nameof(publicKeyCredentialCreationOptions));
            }

            return await jsRuntime.InvokeAsync<PublicKeyCredential>("browserInterop.credential.create",
                new
                {
                    PublicKey = publicKeyCredentialCreationOptions
                }
            );
        }

        public async Task<PasswordCredential> GetPassword()
        {

            return await jsRuntime.InvokeAsync<PasswordCredential>("navigator.credentials.get", new { Password = true });
        }

        public async Task<FederatedCredential> GetFederated(CredentialRequestOptionsFederated federatedRequest)
        {

            return await jsRuntime.InvokeAsync<FederatedCredential>("navigator.credentials.get", new { Federated = federatedRequest });
        }

        public async Task<PublicKeyCredential> GetPublicKey(CredentialRequestOptionsPublicKey publicKeyRequest)
        {
            return await jsRuntime.InvokeAsync<PublicKeyCredential>("navigator.credentials.get", new { PublicKey = publicKeyRequest });
        }
    }
}
