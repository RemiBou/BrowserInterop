using Microsoft.JSInterop;
using System.Threading.Tasks;
using System;
using BrowserInterop.Performance;
using BrowserInterop.Screen;
using System.Collections.Generic;

namespace BrowserInterop
{
    public class JsObjectWrapperBase : IAsyncDisposable
    {
        public JsRuntimeObjectRef JsRuntimeObjectRef { get; protected set; }
        protected IJSRuntime jsRuntime;
        public virtual void SetJsRuntime(IJSRuntime jsRuntime, JsRuntimeObjectRef jsObjectRef)
        {
            this.JsRuntimeObjectRef = jsObjectRef;
            this.jsRuntime = jsRuntime;
        }

        public async ValueTask DisposeAsync()
        {
            await this.JsRuntimeObjectRef.DisposeAsync();
        }

    }

    public class WindowInterop : JsObjectWrapperBase
    {

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

        public override void SetJsRuntime(IJSRuntime jsRuntime, JsRuntimeObjectRef jsRuntimeObjectRef)
        {
            localStorageLazy = new Lazy<StorageInterop>(() => new StorageInterop(jsRuntime, jsRuntimeObjectRef, "localStorage"));
            sessionStorageLazy = new Lazy<StorageInterop>(() => new StorageInterop(jsRuntime, jsRuntimeObjectRef, "sessionStorage"));

            consoleInteropLazy = new Lazy<ConsoleInterop>(() => new ConsoleInterop(jsRuntime, jsRuntimeObjectRef));
            historyInteropLazy = new Lazy<HistoryInterop>(() => new HistoryInterop(jsRuntime, jsRuntimeObjectRef));
            performanceInteropLazy = new Lazy<PerformanceInterop>(() => new PerformanceInterop(jsRuntime, jsRuntimeObjectRef));
            framesArrayInteropLazy = new Lazy<FramesArrayInterop>(() => new FramesArrayInterop(jsRuntimeObjectRef, jsRuntime));
            personalBarLazy = new Lazy<BarPropInterop>(() => new BarPropInterop(jsRuntimeObjectRef, "personalbar", jsRuntime));
            locationBarLazy = new Lazy<BarPropInterop>(() => new BarPropInterop(jsRuntimeObjectRef, "locationbar", jsRuntime));
            menuBarLazy = new Lazy<BarPropInterop>(() => new BarPropInterop(jsRuntimeObjectRef, "menubar", jsRuntime));
            scrollBarsLazy = new Lazy<BarPropInterop>(() => new BarPropInterop(jsRuntimeObjectRef, "scrollbars", jsRuntime));
            statusBarLazy = new Lazy<BarPropInterop>(() => new BarPropInterop(jsRuntimeObjectRef, "statusbar", jsRuntime));
            toolBarLazy = new Lazy<BarPropInterop>(() => new BarPropInterop(jsRuntimeObjectRef, "toolbar", jsRuntime));
            base.SetJsRuntime(jsRuntime, jsRuntimeObjectRef);
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
            NavigatorInterop navigatorInterop = await jsRuntime.GetInstancePropertyAsync<NavigatorInterop>(JsRuntimeObjectRef, "navigator");
            navigatorInterop.SetJSRuntime(jsRuntime, this.JsRuntimeObjectRef);
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
            await jsRuntime.SetInstancePropertyAsync(JsRuntimeObjectRef, "name", name);
        }

        /// <summary>
        /// Returns a reference to the window that opened this current window.
        /// </summary>
        /// <returns></returns>
        public async Task<WindowInterop> Opener()
        {
            var propertyRef = await jsRuntime.GetInstancePropertyRefAsync(JsRuntimeObjectRef, "opener");
            var window = await jsRuntime.GetInstancePropertyAsync<WindowInterop>(JsRuntimeObjectRef, "opener", false);
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
            var propertyRef = await jsRuntime.GetInstancePropertyRefAsync(JsRuntimeObjectRef, "parent");
            var window = await jsRuntime.GetInstancePropertyAsync<WindowInterop>(JsRuntimeObjectRef, "parent", false);
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
            ScreenInterop screeninterop = await jsRuntime.GetInstancePropertyAsync<ScreenInterop>(JsRuntimeObjectRef, "screen");
            screeninterop.SetJSRuntime(jsRuntime, this.JsRuntimeObjectRef);
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
            var propertyRef = await jsRuntime.GetInstancePropertyRefAsync(JsRuntimeObjectRef, "top");
            var window = await jsRuntime.GetInstancePropertyAsync<WindowInterop>(JsRuntimeObjectRef, "top", false);
            window?.SetJsRuntime(jsRuntime, propertyRef);
            return window;
        }

        /// <summary>
        ///  represents the visual viewport for a given window. For a page containing iframes, each iframe, as well as the containing page, will have a unique window object. Each window on a page will have a unique VisualViewport representing the properties associated with that window.
        /// </summary>
        /// <returns></returns>
        public async Task<VisualViewportInterop> VisualViewport()
        {
            var visualViewport = await jsRuntime.GetInstancePropertyAsync<VisualViewportInterop>(JsRuntimeObjectRef, "visualViewport", false);
            visualViewport?.SetJsRuntime(jsRuntime, this.JsRuntimeObjectRef);
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
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "alert", message);
        }

        /// <summary>
        /// Shifts focus away from the window.
        /// </summary>
        /// <returns></returns>
        public async Task Blur()
        {
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "blur");
        }

        /// <summary>
        /// Closes the current window.
        /// </summary>
        /// <returns></returns>
        public async Task Close()
        {
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "close");
        }

        /// <summary>
        /// The Window.confirm() method displays a modal dialog with an optional message and two buttons: OK and Cancel.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<bool> Confirm(string message)
        {
            return await jsRuntime.InvokeInstanceMethodAsync<bool>(JsRuntimeObjectRef, "confirm", message);
        }

        /// <summary>
        /// Makes a request to bring the window to the front. It may fail due to user settings and the window isn't guaranteed to be frontmost before this method returns.
        /// </summary>
        /// <returns></returns>
        public async Task Focus()
        {
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "focus");
        }

        /// <summary>
        /// moves the current window by a specified amount.
        /// </summary>
        /// <param name="deltaX">the amount of pixels to move the window horizontally. Positive values are to the right, while negative values are to the left.</param>
        /// <param name="deltaY">the amount of pixels to move the window vertically. Positive values are down, while negative values are up.</param>
        /// <returns></returns>
        public async Task MoveBy(int deltaX, int deltaY)
        {
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "moveBy", deltaX, deltaY);
        }

        /// <summary>
        /// moves the current window to the specified coordinates.
        /// </summary>
        /// <param name="x">the horizontal coordinate to be moved to.</param>
        /// <param name="y">the vertical coordinate to be moved to.</param>
        /// <returns></returns>
        public async Task MoveTo(int x, int y)
        {
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "moveTo", x, y);
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

            var windowOpenRef = await jsRuntime.InvokeInstanceMethodGetRefAsync(JsRuntimeObjectRef, "open", url, windowName, windowFeature?.GetOpenString());
            var windowInterop = await jsRuntime.GetInstanceContent<WindowInterop>(windowOpenRef, false);
            windowInterop.SetJsRuntime(jsRuntime, windowOpenRef);
            return windowInterop;
        }

        /// <summary>
        /// Safely enables cross-origin communication between Window objects; e.g., between a page and a pop-up that it spawned, or between a page and an iframe embedded within it.
        /// </summary>
        /// <param name="targetWindow">A reference to the window that will receive the message. Methods for obtaining such a reference include : Open, Frames, Top, Opener, Parent</param>
        /// <param name="message">The object that will be send to the other window </param>
        /// <param name="targetOrigin">Specifies what the origin of targetWindow must be for the event to be dispatched, either as the literal string "*" (indicating no preference) or as a URI. If at the time the event is scheduled to be dispatched the scheme, hostname, or port of targetWindow's document does not match that provided in targetOrigin, the event will not be dispatched; only if all three match will the event be dispatched.  </param>
        /// <returns></returns>
        public async Task PostMessage(object message, string targetOrigin)
        {
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "postMessage", message, targetOrigin);
        }

        /// <summary>
        ///  listen for dispatched messages send by PostMessage
        /// </summary>
        /// <param name="todo"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IAsyncDisposable> OnMessage<T>(Func<OnMessageEventPayload<T>, Task> todo)
        {
            return await jsRuntime.AddEventListener(
                this.JsRuntimeObjectRef,
                 "",
                 "message",
                CallBackInteropWrapper.Create<JsRuntimeObjectRef>(
                    async payload =>
                    {
                        var eventPayload = new OnMessageEventPayload<T>()
                        {
                            Data = await jsRuntime.GetInstancePropertyAsync<T>(payload, "data"),
                            Origin = await jsRuntime.GetInstancePropertyAsync<string>(payload, "origin"),
                            Source = await jsRuntime.GetInstancePropertyAsync<WindowInterop>(payload, "source", false)
                        };
                        eventPayload.Source.SetJsRuntime(jsRuntime, await jsRuntime.GetInstancePropertyRefAsync(payload, "source"));

                        await todo.Invoke(eventPayload);
                    },
                    getJsObjectRef: true
                ));
        }
        /// <summary>
        /// Opens the Print Dialog to print the current document.
        /// </summary>
        /// <returns></returns>
        public async Task Print()
        {
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "print");
        }

        /// <summary>
        /// displays a dialog with an optional message prompting the user to input some text.
        /// </summary>
        /// <param name="message">A string of text to display to the user. Can be omitted if there is nothing to show in the prompt window.</param>
        /// <param name="defaultValue">A string containing the default value displayed in the text input field. </param>
        /// <returns></returns>
        public async Task<string> Prompt(string message, string defaultValue = null)
        {
            return await jsRuntime.InvokeInstanceMethodAsync<string>(JsRuntimeObjectRef, "prompt", message, defaultValue);
        }

        /// <summary>
        /// Tells the browser that you wish to perform an animation and requests that the browser calls a specified function to update an animation before the next repaint. The method takes a callback as an argument to be invoked before the repaint.
        /// </summary>
        /// <param name="callback">ells the browser that you wish to perform an animation and requests that the browser calls a specified function to update an animation before the next repaint. The method takes a callback as an argument to be invoked before the repaint.</param>
        /// <returns>The request ID, can be used for cancelling it</returns>
        public async Task<int> RequestAnimationFrame(Func<double, Task> callback)
        {
            return await jsRuntime.InvokeInstanceMethodAsync<int>(JsRuntimeObjectRef, "requestAnimationFrame", CallBackInteropWrapper.Create(callback));
        }

        /// <summary>
        /// cancels an animation frame request previously scheduled through a call to RequestAnimationFrame().
        /// </summary>
        /// <param name="id">Id returned by RequestAnimationFrame</param>
        /// <returns></returns>
        public async Task CancelAnimationFrame(int id)
        {
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "cancelAnimationFrame", id);
        }

        /// <summary>
        /// queues a function to be called during a browser's idle periods. This enables developers to perform background and low priority work on the main event loop, without impacting latency-critical events such as animation and input response.
        /// </summary>
        /// <param name="callback">ells the browser that you wish to perform an animation and requests that the browser calls a specified function to update an animation before the next repaint. The method takes a callback as an argument to be invoked before the repaint.</param>
        /// <returns>The request ID, can be used for cancelling it</returns>
        public async Task<int> RequestIdleCallback(Func<IdleDeadline, Task> callback, RequestIdleCallbackOptions options = null)
        {
            return await jsRuntime.InvokeInstanceMethodAsync<int>(JsRuntimeObjectRef, "requestIdleCallback", CallBackInteropWrapper.Create<JsRuntimeObjectRef>(async jsRef =>
            {
                IdleDeadline idleDeadline = await jsRuntime.GetInstanceContent<IdleDeadline>(jsRef, true);
                idleDeadline.SetJsRuntime(jsRuntime, jsRef);
                await callback.Invoke(idleDeadline);
            }, getJsObjectRef: true), options);
        }

        /// <summary>
        /// cancels a callback previously scheduled with RequestIdleCallback()
        /// </summary>
        /// <param name="id">Id returned by RequestIdleCallback</param>
        /// <returns></returns>
        public async Task CancelIdleCallback(int id)
        {
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "cancelIdleCallback", id);
        }

        /// <summary>
        ///  resizes the current window by a specified amount.
        /// </summary>
        /// <param name="xDelta">the number of pixels to grow the window horizontally.</param>
        /// <param name="yDelta"> the number of pixels to grow the window vertically.</param>
        /// <returns></returns>
        public async Task ResizeBy(int xDelta, int yDelta)
        {
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "resizeBy", xDelta, yDelta);
        }

        /// <summary>
        /// dynamically resizes the window.
        /// </summary>
        /// <param name="width">An integer representing the new outerWidth in pixels (including scroll bars, title bars, etc).</param>
        /// <param name="height">An integer value representing the new outerHeight in pixels (including scroll bars, title bars, etc).</param>
        /// <returns></returns>
        public async Task ResizeTo(int width, int height)
        {
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "resizeTo", width, height);
        }

        /// <summary>
        /// scrolls the window to a particular place in the document.
        /// </summary>
        /// <param name="xCoord">the pixel along the horizontal axis of the document that you want displayed in the upper left.</param>
        /// <param name="yCoord">the pixel along the vertical axis of the document that you want displayed in the upper left.</param>
        /// <returns></returns>
        public async Task Scroll(int xCoord, int yCoord)
        {
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "scroll", xCoord, yCoord);

        }

        /// <summary>
        /// scrolls the window to a particular place in the document.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task Scroll(ScrollToOptions options)
        {
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "scroll", options);
        }

        /// <summary>
        /// scrolls the document in the window by the given amount.
        /// </summary>
        /// <param name="xCoord">the pixel along the horizontal axis of the document that you want displayed in the upper left.</param>
        /// <param name="yCoord">the pixel along the vertical axis of the document that you want displayed in the upper left.</param>
        /// <returns></returns>
        public async Task ScrollBy(int xCoord, int yCoord)
        {
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "scrollBy", xCoord, yCoord);

        }

        /// <summary>
        /// scrolls the document in the window by the given amount.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task ScrollBy(ScrollToOptions options)
        {
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "scrollBy", options);
        }

        /// <summary>
        ///  stops further resource loading in the current browsing context, equivalent to the stop button in the browser.
        /// </summary>
        /// <returns></returns>
        public async Task Stop()
        {
            await jsRuntime.InvokeInstanceMethodAsync(JsRuntimeObjectRef, "stop");

        }

        /// <summary>
        /// Called when the page is installed as a webapp.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public async Task<IAsyncDisposable> OnAppInstalled(Func<Task> callback)
        {
            return await jsRuntime.AddEventListener(JsRuntimeObjectRef, "", "appinstalled", CallBackInteropWrapper.Create(callback, getDeepObject: false));
        }

        /// <summary>
        /// Fired when a resource failed to load, or can't be used. For example, if a script has an execution error or an image can't be found or is invalid.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public async Task<IAsyncDisposable> OnError(Func<Task> callback)
        {
            return await jsRuntime.AddEventListener(JsRuntimeObjectRef, "", "error", CallBackInteropWrapper.Create(callback, getDeepObject: false));
        }

        /// <summary>
        /// The languagechange event is fired at the global scope object when the user's preferred language changes.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public async Task<IAsyncDisposable> OnLanguageCHange(Func<Task> callback)
        {
            return await jsRuntime.AddEventListener(JsRuntimeObjectRef, "", "languagechange", CallBackInteropWrapper.Create(callback, getDeepObject: false));
        }

        /// <summary>
        /// The orientationchange event is fired when the orientation of the device has changed.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public async Task<IAsyncDisposable> OnOrientationChange(Func<Task> callback)
        {
            return await jsRuntime.AddEventListener(JsRuntimeObjectRef, "", "orientationchange", CallBackInteropWrapper.Create(callback, getDeepObject: false));
        }

        public async Task<IAsyncDisposable> OnBeforeInstallPrompt(Func<BeforeInstallPromptEvent, Task> callback)
        {
            return await jsRuntime.AddEventListener(
                JsRuntimeObjectRef, "",
                "beforeinstallprompt",
                CallBackInteropWrapper.Create<JsRuntimeObjectRef>(
                    async jsObjectRef =>
                    {
                        BeforeInstallPromptEvent beforeInstallPromptEvent = new BeforeInstallPromptEvent();
                        beforeInstallPromptEvent.Platforms = await jsRuntime.GetInstancePropertyAsync<string[]>(jsObjectRef, "platforms");
                        beforeInstallPromptEvent.SetJsRuntime(jsRuntime, jsObjectRef);
                        await callback.Invoke(beforeInstallPromptEvent);
                    },
                    getJsObjectRef: true,
                    getDeepObject: false
                )
            );

        }

        public class BeforeInstallPromptEvent
        {
            private JsRuntimeObjectRef jsObjectRef;
            private IJSRuntime jSRuntime;

            /// <summary>
            ///  the platforms on which the event was dispatched. This is provided for user agents that want to present a choice of versions to the user such as, for example, "web" or "play" which would allow the user to chose between a web version or an Android version.
            /// </summary>
            /// <value></value>
            public string[] Platforms { get; set; }

            internal void SetJsRuntime(IJSRuntime jSRuntime, JsRuntimeObjectRef jsObjectRef)
            {
                this.jsObjectRef = jsObjectRef;
                this.jSRuntime = jSRuntime;
            }

            /// <summary>
            /// Returns the user choice
            /// </summary>
            /// <returns></returns>
            public async Task<bool> IsAccepted()
            {
                return (await jSRuntime.GetInstancePropertyAsync<string>(jsObjectRef, "userChoice") == "accepted");
            }

            /// <summary>
            /// Allows a developer to show the install prompt at a time of their own choosing. 
            /// </summary>
            /// <returns></returns>
            public async Task Prompt()
            {
                await jSRuntime.InvokeInstanceMethodAsync(jsObjectRef, "prompt");
            }
        }
    }

    /// <summary>
    /// Event send when a new message is received
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public class OnMessageEventPayload<T>
    {
        /// <summary>
        /// The object passed from the other window.
        /// </summary>
        /// <value></value>
        public T Data { get; set; }

        /// <summary>
        /// The origin of the window that sent the message at the time postMessage was called. 
        /// </summary>
        /// <value></value>
        public string Origin { get; set; }

        /// <summary>
        /// A reference to the window object that sent the message; you can use this to establish two-way communication between two windows with different origins.
        /// </summary>
        /// <value></value>
        public WindowInterop Source { get; set; }
    }
}
