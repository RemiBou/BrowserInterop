namespace BrowserInterop
{
    /// <summary>
    /// Event send when a new message is received
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public class MessageEvent<T>
    {
        /// <summary>
        /// The object passed from the other window.
        /// </summary>
        /// <value></value>
        public T Data { get; set; }

        /// <summary>
        /// The origin of the window that sent the message at the time postMessage was called. 
        /// </summary>
        /// <value></value>
        public string Origin { get; set; }

        /// <summary>
        /// A reference to the window object that sent the message; you can use this to establish two-way communication between two windows with different origins.
        /// </summary>
        /// <value></value>
        public WindowInterop Source { get; set; }
    }
}
