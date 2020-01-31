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
        /// <returns></returns>
        public async Task<bool> Persist()
        {
            return await jsRuntime.InvokeAsync<bool>("navigator.storage.persist");
        }

        /// <summary>
        /// Returns true if box mode is persistent for your site's storage.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Persisted()
        {
            return await jsRuntime.InvokeAsync<bool>("navigator.storage.persisted");
        }

    }
}