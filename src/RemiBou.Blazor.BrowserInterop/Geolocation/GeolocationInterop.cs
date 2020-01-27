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
    }
}