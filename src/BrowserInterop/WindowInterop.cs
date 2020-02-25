using Microsoft.JSInterop;
using System.Threading.Tasks;
using System;
using BrowserInterop.Performance;
using BrowserInterop.Screen;

namespace BrowserInterop
{
    public interface IJsObjectWrapper
    {
        void SetJsRuntime(IJSRuntime jsRuntime, JsRuntimeObjectRef jsObjectRef);
        JsRuntimeObjectRef JsRuntimeObjectRef { get; }
    }

    public class WindowInterop : IJsObjectWrapper
    {
        private JsRuntimeObjectRef windowRef;

        private Lazy<HistoryInterop> historyInteropLazy;
        private Lazy<FramesArrayInterop> framesArrayInteropLazy;
        private Lazy<StorageInterop> localStorageLazy;
        private Lazy<StorageInterop> sessionStorageLazy;
        private Lazy<ConsoleInterop> consoleInteropLazy;
        private Lazy<PerformanceInterop> performanceInteropLazy;
        private Lazy<BarPropInterop> locationBarLazy;
        private Lazy<BarPropInterop> menuBarLazy;
        private Lazy<BarPropInterop> personalBarLazy;
        private Lazy<BarPropInterop> scrollBarsLazy;
        private Lazy<BarPropInterop> statusBarLazy;
        private Lazy<BarPropInterop> toolBarLazy;
        private IJSRuntime jsRuntime;

        public JsRuntimeObjectRef JsRuntimeObjectRef
        {
            get
            {
                return windowRef;
            }
        }

        public void SetJsRuntime(IJSRuntime jsRuntime, JsRuntimeObjectRef windowRef)
        {
            localStorageLazy = new Lazy<StorageInterop>(() => new StorageInterop(jsRuntime, windowRef, "localStorage"));
            sessionStorageLazy = new Lazy<StorageInterop>(() => new StorageInterop(jsRuntime, windowRef, "sessionStorage"));

            consoleInteropLazy = new Lazy<ConsoleInterop>(() => new ConsoleInterop(jsRuntime, windowRef));
            historyInteropLazy = new Lazy<HistoryInterop>(() => new HistoryInterop(jsRuntime, windowRef));
            performanceInteropLazy = new Lazy<PerformanceInterop>(() => new PerformanceInterop(jsRuntime, windowRef));
            framesArrayInteropLazy = new Lazy<FramesArrayInterop>(() => new FramesArrayInterop(windowRef, jsRuntime));
            personalBarLazy = new Lazy<BarPropInterop>(() => new BarPropInterop(windowRef, "personalbar", jsRuntime));
            locationBarLazy = new Lazy<BarPropInterop>(() => new BarPropInterop(windowRef, "locationbar", jsRuntime));
            menuBarLazy = new Lazy<BarPropInterop>(() => new BarPropInterop(windowRef, "menubar", jsRuntime));
            scrollBarsLazy = new Lazy<BarPropInterop>(() => new BarPropInterop(windowRef, "scrollbars", jsRuntime));
            statusBarLazy = new Lazy<BarPropInterop>(() => new BarPropInterop(windowRef, "statusbar", jsRuntime));
            toolBarLazy = new Lazy<BarPropInterop>(() => new BarPropInterop(windowRef, "toolbar", jsRuntime));
            this.jsRuntime = jsRuntime;
            this.windowRef = windowRef;
        }



        /// <summary>
        /// Will return an instance of ConsoleInteorp that'll give access to window.console API
        /// </summary>
        /// <value></value>
        public ConsoleInterop Console => consoleInteropLazy.Value;

        /// <summary>
        /// Indicates whether the referenced window is closed or not.
        /// </summary>
        /// <value></value>
        public bool Closed { get; set; }

        /// <summary>
        /// Will return an instance of NavigatorInterop that'll give access to window.navigator API
        /// </summary>
        /// <value></value>
        public async Task<NavigatorInterop> Navigator()
        {
            NavigatorInterop navigatorInterop = await jsRuntime.GetInstancePropertyAsync<NavigatorInterop>(windowRef, "navigator");
            navigatorInterop.SetJSRuntime(jsRuntime, this.windowRef);
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
            await jsRuntime.SetInstancePropertyAsync(windowRef, "name", name);
        }

        /// <summary>
        /// Returns a reference to the window that opened this current window.
        /// </summary>
        /// <returns></returns>
        public async Task<WindowInterop> Opener()
        {
            var propertyRef = await jsRuntime.GetInstancePropertyRefAsync(windowRef, "opener");
            var window = await jsRuntime.GetInstancePropertyAsync<WindowInterop>(windowRef, "opener", false);
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
            var propertyRef = await jsRuntime.GetInstancePropertyRefAsync(windowRef, "parent");
            var window = await jsRuntime.GetInstancePropertyAsync<WindowInterop>(windowRef, "parent", false);
            window?.SetJsRuntime(jsRuntime, propertyRef);
            return window;
        }

        /// <summary>
        ///  can be used to gather performance information about the current document.
        /// </summary>
        public PerformanceInterop Performance => performanceInteropLazy.Value;

        /// <summary>
        /// Returns the personalbar object, whose visibility can be toggled in the window.
        /// </summary>
        public BarPropInterop PersonalBar => personalBarLazy.Value;

        /// <summary>
        /// Will return an instance of NavigatorInterop that'll give access to window.navigator API
        /// </summary>
        /// <value></value>
        public async Task<ScreenInterop> Screen()
        {
            ScreenInterop screeninterop = await jsRuntime.GetInstancePropertyAsync<ScreenInterop>(windowRef, "screen");
            screeninterop.SetJSRuntime(jsRuntime, this.windowRef);
            return screeninterop;
        }

        /// <summary>
        /// return the horizontal distance from the left border of the user's browser viewport to the left side of the screen.
        /// </summary>
        /// <value></value>
        public int ScreenX { get; set; }

        /// <summary>
        ///  return the vertical distance from the top border of the user's browser viewport to the top side of the screen.
        /// </summary>
        /// <value></value>
        public int ScreenY { get; set; }

        /// <summary>
        /// Returns the scrollbars object, whose visibility can be toggled in the window.
        /// </summary>
        public BarPropInterop ScrollBars => scrollBarsLazy.Value;

        /// <summary>
        /// the number of pixels that the document has already been scrolled horizontally.
        /// </summary>
        /// <value></value>
        public int ScrollX { get; set; }

        /// <summary>
        /// Returns the number of pixels that the document has already been scrolled vertically.
        /// </summary>
        /// <value></value>
        public int ScrollY { get; set; }

        /// <summary>
        /// Returns the statusbar object, whose visibility can be toggled in the window.
        /// </summary>
        public BarPropInterop StatusBar => statusBarLazy.Value;


        /// <summary>
        /// Returns the toolbar object, whose visibility can be toggled in the window.
        /// </summary>
        public BarPropInterop ToolBar => toolBarLazy.Value;

        /// <summary>
        /// Returns a reference to the parent of the current window or subframe
        /// </summary>
        /// <returns></returns>
        public async Task<WindowInterop> Top()
        {
            var propertyRef = await jsRuntime.GetInstancePropertyRefAsync(windowRef, "top");
            var window = await jsRuntime.GetInstancePropertyAsync<WindowInterop>(windowRef, "top", false);
            window?.SetJsRuntime(jsRuntime, propertyRef);
            return window;
        }

        /// <summary>
        ///  represents the visual viewport for a given window. For a page containing iframes, each iframe, as well as the containing page, will have a unique window object. Each window on a page will have a unique VisualViewport representing the properties associated with that window.
        /// </summary>
        /// <returns></returns>
        public async Task<VisualViewportInterop> VisualViewport()
        {
            var visualViewport = await jsRuntime.GetInstancePropertyAsync<VisualViewportInterop>(windowRef, "visualViewport", false);
            visualViewport?.SetJsRuntime(jsRuntime, this.windowRef);
            return visualViewport;
        }

        /// <summary>
        /// Returns a boolean indicating whether the current context is secure (true) or not (false).
        /// </summary>
        /// <value></value>
        public bool IsSecureContext { get; set; }

        /// <summary>
        /// Returns the global object's origin, serialized as a string. 
        /// </summary>
        /// <value></value>
        public string Origin { get; set; }

        /// <summary>
        /// A string you want to display in the alert dialog
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task Alert(string message)
        {
            await jsRuntime.InvokeInstanceMethodAsync(windowRef, "alert", message);
        }

        /// <summary>
        /// Shifts focus away from the window.
        /// </summary>
        /// <returns></returns>
        public async Task Blur()
        {
            await jsRuntime.InvokeInstanceMethodAsync(windowRef, "blur");
        }

        /// <summary>
        /// Closes the current window.
        /// </summary>
        /// <returns></returns>
        public async Task Close()
        {
            await jsRuntime.InvokeInstanceMethodAsync(windowRef, "close");
        }

        /// <summary>
        /// The Window.confirm() method displays a modal dialog with an optional message and two buttons: OK and Cancel.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<bool> Confirm(string message)
        {
            return await jsRuntime.InvokeInstanceMethodAsync<bool>(windowRef, "confirm", message);
        }

        /// <summary>
        /// Makes a request to bring the window to the front. It may fail due to user settings and the window isn't guaranteed to be frontmost before this method returns.
        /// </summary>
        /// <returns></returns>
        public async Task Focus()
        {
            await jsRuntime.InvokeInstanceMethodAsync(windowRef, "focus");
        }

        /// <summary>
        /// moves the current window by a specified amount.
        /// </summary>
        /// <param name="deltaX">the amount of pixels to move the window horizontally. Positive values are to the right, while negative values are to the left.</param>
        /// <param name="deltaY">the amount of pixels to move the window vertically. Positive values are down, while negative values are up.</param>
        /// <returns></returns>
        public async Task MoveBy(int deltaX, int deltaY)
        {
            await jsRuntime.InvokeInstanceMethodAsync(windowRef, "moveBy", deltaX, deltaY);
        }

        /// <summary>
        /// moves the current window to the specified coordinates.
        /// </summary>
        /// <param name="x">the horizontal coordinate to be moved to.</param>
        /// <param name="y">the vertical coordinate to be moved to.</param>
        /// <returns></returns>
        public async Task MoveTo(int x, int y)
        {
            await jsRuntime.InvokeInstanceMethodAsync(windowRef, "moveTo", x, y);
        }

        /// <summary>
        /// loads the specified resource into the browsing context (window, <iframe> or tab) with the specified name. If the name doesn't exist, then a new window is opened and the specified resource is loaded into its browsing context.
        /// </summary>
        /// <param name="url">URL of the resource to be loaded. This can be a path or URL to an HTML page, image file, or any other resource which is supported by the browser. If the empty string ("") is specified as url, a blank page is opened into the targeted browsing context.</param>
        /// <param name="windowName">the name of the browsing context (window, <iframe> or tab) into which to load the specified resource; if the name doesn't indicate an existing context, a new window is created and is given the name specified by windowName.</param>
        /// <param name="windowFeature">comma-separated list of window features given with their corresponding values in the form "name=value". These features include options such as the window's default size and position, whether or not to include scroll bars, and so forth. There must be no whitespace in the string.</param>
        /// <returns></returns>
        public async Task<WindowInterop> Open(string url, string windowName = null, WindowFeature windowFeature = null)
        {

            var windowOpenRef = await jsRuntime.InvokeInstanceMethodGetRefAsync(windowRef, "open", url, windowName, windowFeature?.GetOpenString());
            var windowInterop = await jsRuntime.GetInstanceContent<WindowInterop>(windowOpenRef, false);
            windowInterop.SetJsRuntime(jsRuntime, windowOpenRef);
            return windowInterop;
        }

    }
}
