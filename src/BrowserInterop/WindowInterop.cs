using Microsoft.JSInterop;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections;

namespace BrowserInterop
{

    public class WindowInterop
    {
        private JsRuntimeObjectRef jsRuntimeObjectRef;

        private Lazy<HistoryInterop> historyInteropLazy;
        private Lazy<FramesArrayInterop> framesArrayInteropLazy;
        private Lazy<StorageInterop> localStorageLazy;
        private Lazy<ConsoleInterop> consoleInteropLazy;
        private Lazy<BarPropInterop> locationBarLazy;
        private Lazy<BarPropInterop> menuBarLazy;
        private IJSRuntime jsRuntime;

        internal void SetJsRuntime(IJSRuntime jsRuntime, JsRuntimeObjectRef jsRuntimeObjectRef)
        {
            localStorageLazy = new Lazy<StorageInterop>(() => new StorageInterop(jsRuntime, jsRuntimeObjectRef, "localStorage"));
            consoleInteropLazy = new Lazy<ConsoleInterop>(() => new ConsoleInterop(jsRuntime, jsRuntimeObjectRef));
            historyInteropLazy = new Lazy<HistoryInterop>(() => new HistoryInterop(jsRuntime, jsRuntimeObjectRef));
            framesArrayInteropLazy = new Lazy<FramesArrayInterop>(() => new FramesArrayInterop(jsRuntimeObjectRef, jsRuntime));
            locationBarLazy = new Lazy<BarPropInterop>(() => new BarPropInterop(jsRuntimeObjectRef, "locationbar", jsRuntime));
            menuBarLazy = new Lazy<BarPropInterop>(() => new BarPropInterop(jsRuntimeObjectRef, "menubar", jsRuntime));
            this.jsRuntime = jsRuntime;
            this.jsRuntimeObjectRef = jsRuntimeObjectRef;
        }



        /// <summary>
        /// Will return an instance of ConsoleInteorp that'll give access to window.console API
        /// </summary>
        /// <value></value>
        public ConsoleInterop Console => consoleInteropLazy.Value;

        /// <summary>
        /// Will return an instance of NavigatorInterop that'll give access to window.navigator API
        /// </summary>
        /// <value></value>
        public async Task<NavigatorInterop> Navigator()
        {
            NavigatorInterop navigatorInterop = await jsRuntime.GetInstancePropertyAsync<NavigatorInterop>(jsRuntimeObjectRef, "navigator");
            navigatorInterop.SetJSRuntime(jsRuntime, this.jsRuntimeObjectRef);
            return navigatorInterop;
        }

        /// <summary>
        /// Give access to the direct sub-frames of the current window.
        /// </summary>
        public FramesArrayInterop Frames => framesArrayInteropLazy.Value;

        /// <summary>
        /// reference to the History object, which provides an interface for manipulating the browser session history (pages visited in the tab or frame that the current page is loaded in).
        /// </summary>
        public HistoryInterop History => historyInteropLazy.Value;
        /// <summary>
        /// Gets the height of the content area of the browser window including, if rendered, the horizontal scrollbar.
        /// </summary>
        /// <value></value>
        public int InnerHeight { get; set; }

        /// <summary>
        /// Gets the width of the content area of the browser window including, if rendered, the vertical scrollbar.
        /// </summary>
        /// <value></value>
        public int InnerWidth { get; set; }

        /// <summary>
        /// Returns the locationbar object, whose visibility can be checked.
        /// </summary>
        public BarPropInterop LocationBar => locationBarLazy.Value;

        /// <summary>
        /// Returns the menubar object, whose visibility can be checked.
        /// </summary>
        public BarPropInterop MenuBar => menuBarLazy.Value;

        public StorageInterop LocalStorage => localStorageLazy.Value;

    }

    /// <summary>
    /// Represent property of a menu element
    /// </summary>
    public class BarPropInterop
    {
        private JsRuntimeObjectRef jsRuntimeObjectRef;
        private string propertyName;
        private IJSRuntime jSRuntime;

        internal BarPropInterop(JsRuntimeObjectRef jsRuntimeObjectRef, string propertyName, IJSRuntime jSRuntime)
        {
            this.jsRuntimeObjectRef = jsRuntimeObjectRef;
            this.propertyName = propertyName;
            this.jSRuntime = jSRuntime;
        }

        /// <summary>
        /// Return true if the element is visible or not
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetVisible()
        {
            return await jSRuntime.GetInstancePropertyAsync<bool>(jsRuntimeObjectRef, $"{propertyName}.visible");
        }

        /// <summary>
        /// Tries to change visibility of the element
        /// </summary>
        /// <param name="visible"></param>
        /// <returns></returns>
        public async Task SetVisible(bool visible)
        {
            await jSRuntime.SetInstancePropertyAsync(jsRuntimeObjectRef, $"{propertyName}.visible", visible);
        }
    }
}
