using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace RemiBou.Blazor.BrowserInterop
{
    public class NavigatorInterop
    {
        private IJSRuntime jsRuntime;

        public NavigatorInterop()
        {
        }

        internal void SetJSRuntime(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Returns the internal "code" name of the current browser. Do not rely on this property to return the correct value.
        /// </summary>
        /// <returns></returns>
        public string AppCodeName { get; set; }

        /// <summary>
        /// Returns  the official name of the browser. Do not rely on this property to return the correct value.
        /// </summary>
        /// <returns></returns>
        public string AppName { get; set; }

        /// <summary>
        /// Returns  the official name of the browser. Do not rely on this property to return the correct value.
        /// </summary>
        /// <returns></returns>
        public string AppVersion { get; set; }



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


        /// <summary>
        /// Returns the number of logical processor cores available.
        /// </summary>
        /// <returns></returns>
        public async Task<int> HardwareConcurrency()
        {
            return await jsRuntime.InvokeAsync<int>("navigator.hardwareConcurrency");
        }

        /// <summary>
        /// Returns false if the browser enables java
        /// </summary>
        /// <returns></returns>
        public async Task<bool> JavaEnabled()
        {
            return await jsRuntime.InvokeAsync<bool>("navigator.javaEnabled");
        }

        /// <summary>
        /// The user prefred language
        /// </summary>
        /// <returns></returns>
        public async Task<string> Language()
        {
            return await jsRuntime.InvokeAsync<string>("navigator.language");
        }

        /// <summary>
        /// Return all the user set languages
        /// </summary>
        /// <returns></returns>
        public async Task<string[]> Languages()
        {
            return await jsRuntime.InvokeAsync<string[]>("navigator.languages");
        }

        /// <summary>
        /// Returns the maximum number of touch point supported by the current device
        /// </summary>
        /// <returns></returns>
        public async Task<int> MaxTouchPoints()
        {
            return await jsRuntime.InvokeAsync<int>("navigator.maxTouchPoints");
        }

        /// <summary>
        /// Returns the mime types supported by the browser
        /// </summary>
        /// <returns></returns>
        public async Task<MimeTypeInterop[]> MimeTypes()
        {
            return await jsRuntime.InvokeAsync<MimeTypeInterop[]>("browserInterop.navigator.mimeTypes");
        }

        /// <summary>
        /// Returns true if the user is online
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Online()
        {
            return await jsRuntime.InvokeAsync<bool>("navigator.online");
        }

        /// <summary>
        /// Returns a string representing the platform of the browser.
        /// </summary>
        /// <value></value>
        public async Task<string> Platform()
        {
            return await jsRuntime.InvokeAsync<string>("navigator.platform");
        }


        /// <summary>
        /// Returns the plugins installed in this browser
        /// </summary>
        /// <returns></returns>

        protected async Task<PluginInterop[]> Plugins()
        {
            return await jsRuntime.InvokeAsync<PluginInterop[]>("browserInterop.navigator.plugins");
        }

        /// <summary>
        /// Return the user agent string for the browser
        /// </summary>
        /// <value></value>
        public async Task<string> UserAgent()
        {
            return await jsRuntime.InvokeAsync<string>("navigator.userAgent");
        }

    }

}
