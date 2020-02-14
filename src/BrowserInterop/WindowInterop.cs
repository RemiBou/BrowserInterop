using Microsoft.JSInterop;
using System.Threading.Tasks;
using System;
using BrowserInterop.Performance;

namespace BrowserInterop
{

    public class WindowInterop
    {
        private JsRuntimeObjectRef jsRuntimeObjectRef;

        private Lazy<HistoryInterop> historyInteropLazy;
        private Lazy<FramesArrayInterop> framesArrayInteropLazy;
        private Lazy<StorageInterop> localStorageLazy;
        private Lazy<StorageInterop> sessionStorageLazy;
        private Lazy<ConsoleInterop> consoleInteropLazy;
        private Lazy<PerformanceInterop> performanceInteropLazy;
        private Lazy<BarPropInterop> locationBarLazy;
        private Lazy<BarPropInterop> menuBarLazy;
        private IJSRuntime jsRuntime;

        internal void SetJsRuntime(IJSRuntime jsRuntime, JsRuntimeObjectRef jsRuntimeObjectRef)
        {
            localStorageLazy = new Lazy<StorageInterop>(() => new StorageInterop(jsRuntime, jsRuntimeObjectRef, "localStorage"));
            sessionStorageLazy = new Lazy<StorageInterop>(() => new StorageInterop(jsRuntime, jsRuntimeObjectRef, "sessionStorage"));

            consoleInteropLazy = new Lazy<ConsoleInterop>(() => new ConsoleInterop(jsRuntime, jsRuntimeObjectRef));
            historyInteropLazy = new Lazy<HistoryInterop>(() => new HistoryInterop(jsRuntime, jsRuntimeObjectRef));
            performanceInteropLazy = new Lazy<PerformanceInterop>(() => new PerformanceInterop(jsRuntime, jsRuntimeObjectRef));
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

        /// <summary>
        /// reference to the local storage object used to store data that may only be accessed by the origin that created it.
        /// </summary>
        public StorageInterop LocalStorage => localStorageLazy.Value;

        /// <summary>
        /// Returns a reference to the session storage object used to store data that may only be accessed by the origin that created it.
        /// </summary>
        public StorageInterop SessionStorage => sessionStorageLazy.Value;

        /// <summary>
        /// Gets the name of the window.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// Set the name of the window
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task SetName(string name)
        {
            await jsRuntime.SetInstancePropertyAsync(jsRuntimeObjectRef, "name", name);
        }

        /// <summary>
        /// Returns a reference to the window that opened this current window.
        /// </summary>
        /// <returns></returns>
        public async Task<WindowInterop> Opener()
        {
            var propertyRef = await jsRuntime.GetInstancePropertyRefAsync(jsRuntimeObjectRef, "opener");
            var window = await jsRuntime.GetInstancePropertyAsync<WindowInterop>(jsRuntimeObjectRef, "opener", false);
            window?.SetJsRuntime(jsRuntime, propertyRef);
            return window;
        }

        /// <summary>
        /// Gets the height of the outside of the browser window.
        /// </summary>
        /// <value></value>
        public int OuterHeight { get; set; }

        /// <summary>
        /// Gets the width of the outside of the browser window.
        /// </summary>
        /// <value></value>
        public int OuterWidth { get; set; }


        /// <summary>
        /// Returns a reference to the parent of the current window or subframe
        /// </summary>
        /// <returns></returns>
        public async Task<WindowInterop> Parent()
        {
            var propertyRef = await jsRuntime.GetInstancePropertyRefAsync(jsRuntimeObjectRef, "parent");
            var window = await jsRuntime.GetInstancePropertyAsync<WindowInterop>(jsRuntimeObjectRef, "parent", false);
            window?.SetJsRuntime(jsRuntime, propertyRef);
            return window;
        }

        /// <summary>
        ///  can be used to gather performance information about the current document.
        /// </summary>
        public PerformanceInterop Performance => performanceInteropLazy.Value;

    }
}
