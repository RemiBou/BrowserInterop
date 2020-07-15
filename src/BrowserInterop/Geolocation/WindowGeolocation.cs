using Microsoft.JSInterop;

using System;
using System.Threading.Tasks;

namespace BrowserInterop.Geolocation
{
    public class WindowGeolocation
    {
        private readonly IJSRuntime jsRuntime;

        internal WindowGeolocation(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        /// <summary>
        /// used to get the current position of the device.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public async ValueTask<GeolocationResult> GetCurrentPosition(PositionOptions options = null)
        {
            return await jsRuntime.InvokeAsync<GeolocationResult>("browserInterop.navigator.geolocation.getCurrentPosition", options);
        }

        /// <summary>
        ///  register a handler function that will be called automatically each time the position of the device changes. 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async ValueTask<IAsyncDisposable> WatchPosition(Func<GeolocationResult, ValueTask> callback, PositionOptions options = null)
        {
            WatchGeolocationWrapper wrapper = new WatchGeolocationWrapper(callback, jsRuntime);

            int watchId = await jsRuntime.InvokeAsync<int>(
                "browserInterop.navigator.geolocation.watchPosition",
                 options,
                  DotNetObjectReference.Create(wrapper));

            wrapper.SetWatchId(watchId);

            return wrapper;
        }
        private class WatchGeolocationWrapper : IAsyncDisposable
        {
            private readonly Func<GeolocationResult, ValueTask> callback;
            private readonly IJSRuntime jSRuntime;
            private int watchId;

            public WatchGeolocationWrapper(Func<GeolocationResult, ValueTask> callback, IJSRuntime jSRuntime)
            {
                this.callback = callback;
                this.jSRuntime = jSRuntime;
            }

            [JSInvokable]
            public async ValueTask Invoke(GeolocationResult result)
            {
                await callback.Invoke(result);
            }

            internal void SetWatchId(int watchId)
            {
                this.watchId = watchId;
            }

            public async ValueTask DisposeAsync()
            {
                await jSRuntime.InvokeVoidAsync("navigator.geolocation.clearWatch", watchId);
            }
        }
    }
}