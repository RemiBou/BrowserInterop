using BrowserInterop.Extensions;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BrowserInterop.Storage
{
    /// <summary>
    /// provides an interface for managing persistence permissions and estimating available storage
    /// </summary>
    public class WindowStorageManager
    {
        private readonly IJSRuntime jsRuntime;

        internal WindowStorageManager(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Returns a StorageEstimate object containing usage and quota numbers for your origin.
        /// </summary>
        /// <returns></returns>
        public async ValueTask<StorageEstimate> Estimate()
        {
            return await jsRuntime.InvokeAsync<StorageEstimate>("navigator.storage.estimate").ConfigureAwait(false);
        }

        /// <summary>
        /// Requests permission to use persistent storage, and returns  true if permission is granted and box mode is persistent, and false otherwise.
        /// </summary>
        /// <param name="timeout">In some browser the user will be prompted for validation, this method will return false if the user did not provide an answer before</param>
        /// <returns></returns>
        public async ValueTask<bool> Persist(TimeSpan? timeout = null)
        {
            return timeout.HasValue
                ? await jsRuntime.InvokeOrDefault<bool>("navigator.storage.persist", timeout.Value, null).ConfigureAwait(false)
                : await jsRuntime.InvokeAsync<bool>("navigator.storage.persist", null).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns true if box mode is persistent for your site's storage.
        /// </summary>
        /// <returns></returns>
        public async ValueTask<bool> Persisted(TimeSpan? timeout = null)
        {
            return timeout.HasValue
                ? await jsRuntime.InvokeOrDefault<bool>("navigator.storage.persisted", timeout.Value, null).ConfigureAwait(false)
                : await jsRuntime.InvokeAsync<bool>("navigator.storage.persisted", null).ConfigureAwait(false);
        }
    }
}