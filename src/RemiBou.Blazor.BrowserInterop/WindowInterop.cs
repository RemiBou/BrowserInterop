using Microsoft.JSInterop;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RemiBou.Blazor.BrowserInterop
{

    public class WindowInterop
    {

        private readonly Lazy<ConsoleInterop> consoleInteropLazy;
        private readonly IJSRuntime jsRuntime;

        internal WindowInterop(IJSRuntime jsRuntime)
        {
            consoleInteropLazy = new Lazy<ConsoleInterop>(() => new ConsoleInterop(jsRuntime));
            this.jsRuntime = jsRuntime;
        }



        /// <summary>
        /// Will return an instance of ConsoleInteorp that'll give access to window.console API
        /// </summary>
        /// <value></value>
        public ConsoleInterop Console
        {
            get
            {
                return consoleInteropLazy.Value;
            }
        }

        /// <summary>
        /// Will return an instance of NavigatorInterop that'll give access to window.navigator API
        /// </summary>
        /// <value></value>
        public async Task<NavigatorInterop> Navigator()
        {
            NavigatorInterop navigatorInterop = await jsRuntime.InvokeAsync<NavigatorInterop>("browserInterop.getAsJson", "navigator");
            navigatorInterop.SetJSRuntime(jsRuntime);
            return navigatorInterop;
        }
    }
}
