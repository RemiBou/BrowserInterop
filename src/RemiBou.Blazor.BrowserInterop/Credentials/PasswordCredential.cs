using System.Collections.Generic;

namespace RemiBou.Blazor.BrowserInterop.Credentials
{
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
}
