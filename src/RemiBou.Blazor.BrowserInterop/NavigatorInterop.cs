using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace RemiBou.Blazor.BrowserInterop
{
    public class NavigatorInterop
    {
        private readonly IJSRuntime jsRuntime;

        internal NavigatorInterop(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Returns the internal "code" name of the current browser. Do not rely on this property to return the correct value.
        /// </summary>
        /// <returns></returns>
        public async Task<string> AppCodeName()
        {
            return await jsRuntime.InvokeAsync<string>("navigator.appCodeName");
        }

        /// <summary>
        /// Returns  the official name of the browser. Do not rely on this property to return the correct value.
        /// </summary>
        /// <returns></returns>
        public async Task<string> AppName()
        {
            return await jsRuntime.InvokeAsync<string>("navigator.appName");
        }

        /// <summary>
        /// Returns  the official name of the browser. Do not rely on this property to return the correct value.
        /// </summary>
        /// <returns></returns>
        public async Task<string> AppVersion()
        {
            return await jsRuntime.InvokeAsync<string>("navigator.appVersion");
        }

        /// <summary>
        /// Return a JS Interop wrapper for getting information about the battery charging state
        /// </summary>
        /// <returns></returns>
        public BatteryManagerInterop Battery()
        {
            return new BatteryManagerInterop(jsRuntime);
        }

        /// <summary>
        /// Return a JS Interop wrapper for getting information about the network connection of a device.
        /// </summary>
        /// <returns></returns>
        public NetworkInformationInterop Connection()
        {
            return new NetworkInformationInterop(jsRuntime);
        }

        /// <summary>
        /// Returns false if setting a cookie will be ignored and true otherwise.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CookieEnabled()
        {
            return await jsRuntime.InvokeAsync<bool>("navigator.cookieEnabled");
        }
    }
}