using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BrowserInterop.Geolocation
{
    public class WindowNavigatorGeolocation
    {
        private readonly IJSRuntime jsRuntime;

        internal WindowNavigatorGeolocation(IJSRuntime jsRuntime)
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
            return await jsRuntime.InvokeAsync<GeolocationResult>(
                "browserInterop.navigator.geolocation.getCurrentPosition", options);
        }

        /// <summary>
        ///  register a handler function that will be called automatically each time the position of the device changes. 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async ValueTask<IAsyncDisposable> WatchPosition(Func<GeolocationResult, ValueTask> callback,
            PositionOptions options = null)
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
            private readonly Func<GeolocationResult, ValueTask> callback;
            private readonly IJSRuntime jsRuntime;
            private int watchId;

            public WatchGeolocationWrapper(Func<GeolocationResult, ValueTask> callback, IJSRuntime jsRuntime)
            {
                this.callback = callback;
                this.jsRuntime = jsRuntime;
            }

            [JSInvokable]
            // ReSharper disable once UnusedMember.Local
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
                await jsRuntime.InvokeVoidAsync("navigator.geolocation.clearWatch", watchId);
            }
        }
    }
}