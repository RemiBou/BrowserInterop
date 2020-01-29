using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace RemiBou.Blazor.BrowserInterop.Geolocation
{
    public class GeolocationInterop
    {
        private IJSRuntime jsRuntime;

        internal GeolocationInterop(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        /// <summary>
        /// used to get the current position of the device.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<GeolocationResult> GetCurrentPosition(PositionOptions options = null)
        {
            return await jsRuntime.InvokeAsync<GeolocationResult>("browserInterop.navigator.geolocation.getCurrentPosition", options);
        }

        /// <summary>
        ///  register a handler function that will be called automatically each time the position of the device changes. 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<IAsyncDisposable> WatchPosition(Func<GeolocationResult, Task> callback, PositionOptions options = null)
        {
            var wrapper = new WatchGeolocationWrapper(callback, jsRuntime);

            var watchId = await jsRuntime.InvokeAsync<int>(
                "browserInterop.navigator.geolocation.watchPosition",
                 options,
                  DotNetObjectReference.Create(wrapper));

            wrapper.SetWatchId(watchId);

            return wrapper;
        }
        private class WatchGeolocationWrapper : IAsyncDisposable
        {
            private readonly Func<GeolocationResult, Task> callback;
            private readonly IJSRuntime jSRuntime;
            private int watchId;

            public WatchGeolocationWrapper(Func<GeolocationResult, Task> callback, IJSRuntime jSRuntime)
            {
                this.callback = callback;
                this.jSRuntime = jSRuntime;
            }

            [JSInvokable]
            public async Task Invoke(GeolocationResult result)
            {
                await this.callback.Invoke(result);
            }

            internal void SetWatchId(int watchId)
            {
                this.watchId = watchId;
            }

            public async ValueTask DisposeAsync()
            {
                await this.jSRuntime.InvokeVoidAsync("navigator.geolocation.clearWatch", watchId);
            }
        }
    }
}