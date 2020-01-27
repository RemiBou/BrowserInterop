namespace RemiBou.Blazor.BrowserInterop.Credentials
{
    /// <summary>
    /// Biometric authenticator performance bounds. The client must not use any authenticator with false acceptance rate (FAR) and false rejection rate (FRR) below the inputs. The client outputs true if this was taken into account.
    /// </summary>
    public class PublicKeyCredentialCreationOptionsExtensionsBiometricPerfBounds
    {
        /// <summary>
        /// false acceptance rate
        /// </summary>
        /// <value></value>
        public int FAR { get; set; }

        /// <summary>
        ///  false rejection rate 
        /// </summary>
        /// <value></value>
        public int FRR { get; set; }
    }
}
