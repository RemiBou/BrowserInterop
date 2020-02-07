using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BrowserInterop.Storage
{
    /// <summary>
    /// provides an interface for managing persistance permissions and estimating available storage
    /// </summary>
    public class StorageInterop
    {
        private readonly IJSRuntime jsRuntime;

        internal StorageInterop(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Returns a StorageEstimate object containing usage and quota numbers for your origin.
        /// </summary>
        /// <returns></returns>
        public async Task<StorageEstimate> Estimate()
        {
            return await jsRuntime.InvokeAsync<StorageEstimate>("navigator.storage.estimate");
        }

        /// <summary>
        /// Requests permission to use persistent storage, and returns  true if permission is granted and box mode is persistent, and false otherwise.
        /// </summary>
        /// <param name="timeout">In some browser the user will be prompted for validation, this method will return false if the user did not povide an swner before</param>
        /// <returns></returns>
        public async Task<bool> Persist(TimeSpan? timeout = null)
        {
            return await (timeout.HasValue ? jsRuntime.InvokeAsync<bool>("navigator.storage.persist", timeout.Value, null) : jsRuntime.InvokeAsync<bool>("navigator.storage.persist", null));
        }

        /// <summary>
        /// Returns true if box mode is persistent for your site's storage.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Persisted(TimeSpan? timeout = null)
        {
            return await (timeout.HasValue ? jsRuntime.InvokeAsync<bool>("navigator.storage.persisted", timeout.Value, null) : jsRuntime.InvokeAsync<bool>("navigator.storage.persisted", null));
        }

    }
}