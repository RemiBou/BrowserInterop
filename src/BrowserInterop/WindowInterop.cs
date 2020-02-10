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
        private readonly JsRuntimeObjectRef jsRuntimeObjectRef;

        private readonly Lazy<FramesArrayInterop> framesArrayInteropLazy;
        private readonly Lazy<ConsoleInterop> consoleInteropLazy;
        private readonly IJSRuntime jsRuntime;

        internal WindowInterop(IJSRuntime jsRuntime, JsRuntimeObjectRef jsRuntimeObjectRef)
        {
            consoleInteropLazy = new Lazy<ConsoleInterop>(() => new ConsoleInterop(jsRuntime, jsRuntimeObjectRef));
            framesArrayInteropLazy = new Lazy<FramesArrayInterop>(() => new FramesArrayInterop(jsRuntimeObjectRef, jsRuntime));

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

        public FramesArrayInterop Frames => framesArrayInteropLazy.Value;
    }
}
