using BrowserInterop.Geolocation;
using BrowserInterop.Storage;

using Microsoft.JSInterop;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrowserInterop
{
    public class WindowNavigator : JsObjectWrapperBase
    {

        public WindowNavigator()
        {
        }

        internal override void SetJsRuntime(IJSRuntime jsRuntime, JsRuntimeObjectRef navigatorRef)
        {
            base.SetJsRuntime(jsRuntime, navigatorRef);
            Geolocation = new WindowGeolocation(jsRuntime);
            Storage = new WindowStorageManager(jsRuntime);
            Connection?.SetJsRuntime(jsRuntime, navigatorRef);
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
        ///  provides information about the system's battery
        /// </summary>
        /// <returns></returns>
        public async ValueTask<BatteryManager> GetBattery()
        {
            return await jsRuntime.InvokeAsync<BatteryManager>("browserInterop.navigator.getBattery");

        }



        /// <summary>
        /// Return a JS Interop wrapper for getting information about the network connection of a device.
        /// </summary>
        /// <returns></returns>
        public WindowNavigatorConnection Connection { get; set; }

        /// <summary>
        /// Returns false if setting a cookie will be ignored and true otherwise.
        /// </summary>
        /// <returns></returns>
        public bool CookieEnabled { get; set; }

        /// <summary>
        /// Returns the number of logical processor cores available.
        /// </summary>
        /// <returns></returns>
        public int HardwareConcurrency { get; set; }

        public WindowGeolocation Geolocation { get; private set; }

        /// <summary>
        /// Returns false if the browser enables java
        /// </summary>
        /// <returns></returns>
        public async ValueTask<bool> JavaEnabled()
        {
            return await jsRuntime.InvokeInstanceMethodAsync<bool>(JsRuntimeObjectRef, "javaEnabled");
        }

        /// <summary>
        /// The user prefred language
        /// </summary>
        /// <returns></returns>
        public string Language { get; set; }

        /// <summary>
        /// Return all the user set languages
        /// </summary>
        /// <returns></returns>
        public string[] Languages { get; set; }

        /// <summary>
        /// Returns the maximum number of touch point supported by the current device
        /// </summary>
        /// <returns></returns>
        public int MaxTouchPoints { get; set; }

        /// <summary>
        /// Returns the mime types supported by the browser
        /// </summary>
        /// <returns></returns>
        public NavigatorMimeTypes[] MimeTypes { get; set; }

        /// <summary>
        /// Returns true if the user is online
        /// </summary>
        /// <returns></returns>
        public bool Online { get; set; }

        /// <summary>
        /// Returns a string representing the platform of the browser.
        /// </summary>
        /// <value></value>
        public string Platform { get; set; }


        /// <summary>
        /// Returns the plugins installed in this browser
        /// </summary>
        /// <returns></returns>

        public NavigatorPlugin[] Plugins { get; set; }

        /// <summary>
        /// Provides an interface for managing persistance permissions and estimating available storage
        /// </summary>
        /// <value></value>
        public WindowStorageManager Storage { get; private set; }

        /// <summary>
        /// Return the user agent string for the browser
        /// </summary>
        /// <value></value>
        public string UserAgent { get; set; }

        /// <summary>
        /// Returns true if a call to Share() would succeed.
        /// Returns false if it would fail or sharing is not supported
        /// </summary>
        /// <returns></returns>
        public async ValueTask<bool> CanShare(ShareData shareData)
        {
            return await jsRuntime.HasProperty(JsRuntimeObjectRef, "canShare") && await jsRuntime.InvokeInstanceMethodAsync<bool>(JsRuntimeObjectRef, "canShare", shareData);
        }

        /// <summary>
        /// Lets web sites register their ability to open or handle particular URL schemes (aka protocols).
        /// 
        /// For example, this API lets webmail sites open mailto: URLs, or VoIP sites open tel: URLs.
        /// </summary>
        /// <param name="protocol">A string containing the protocol the site wishes to handle. For example, you can register to handle SMS text message links by passing the "sms" scheme.</param>
        /// <param name="url">A string containing the URL of the handler. This URL must include %s, as a placeholder that will be replaced with the escaped URL to be handled.</param>
        /// <param name="title">A human-readable title string for the handler. This will be displayed to the user, such as prompting “Allow this site to handle [scheme] links?” or listing registered handlers in the browser’s settings.</param>
        /// <returns></returns>
        public async ValueTask RegisterProtocolHandler(string protocol, string url, string title)
        {
            if (string.IsNullOrEmpty(protocol))
            {
                throw new ArgumentException("Protocol parameter is mandatory", nameof(protocol));
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("URL parameter is mandatory", nameof(url));
            }

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Title parameter is mandatory", nameof(title));
            }

            if (await jsRuntime.HasProperty(JsRuntimeObjectRef, "registerProtocolHandler"))
            {
                await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "registerProtocolHandler", protocol, url, title);
            }
        }

        /// <summary>
        /// Sends a small amount of data over HTTP to a web server. Returns true if the method is supported and succeeds
        /// 
        /// This method is for analytics and diagnostics that send data to a server before the document is unloaded, where sending the data any sooner may miss some possible data collection. For example, which link the user clicked before navigating away and unloading the page.
        /// Ensuring that data has been sent during the unloading of a document has traditionally been difficult, because user agents typically ignore asynchronous XMLHttpRequests made in an unload handler.
        /// See https://developer.mozilla.org/en-US/docs/Web/API/Navigator/sendBeacon
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async ValueTask<bool> SendBeacon(string url, object data)
        {
            return await jsRuntime.HasProperty(JsRuntimeObjectRef, "sendBeacon") && await jsRuntime.InvokeInstanceMethodAsync<bool>(JsRuntimeObjectRef, "sendBeacon", url, data);
        }

        /// <summary>
        /// Invokes the native sharing mechanism of the device.
        /// Use CanShare to check if this is allowed
        /// </summary>
        /// <returns></returns>
        public async ValueTask Share(ShareData shareData)
        {
            await jsRuntime.InvokeInstanceMethodAsync<bool>(JsRuntimeObjectRef, "share", shareData);
        }

        /// <summary>
        /// Pulses the vibration hardware on the device, if such hardware exists. If the device doesn't support vibration, this method has no effect. If a vibration pattern is already in progress when this method is called, the previous pattern is halted and the new one begins instead.
        /// </summary>
        /// <param name="pattern">Each value indicates a number of milliseconds to vibrate or pause, in alternation. An array of values to alternately vibrate, pause, then vibrate again.</param>
        /// <returns></returns>
        public async ValueTask Vibrate(IEnumerable<TimeSpan> pattern)
        {
            await jsRuntime.InvokeInstanceMethodAsync<bool>(JsRuntimeObjectRef, "vibrate", pattern.Select(t => t.TotalMilliseconds).ToArray());
        }

    }

}
