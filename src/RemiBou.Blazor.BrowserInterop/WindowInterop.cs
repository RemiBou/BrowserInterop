using Microsoft.JSInterop;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RemiBou.Blazor.BrowserInterop
{

    public class WindowInterop
    {

        private readonly Lazy<ConsoleInterop> consoleInteropLazy;
        private readonly Lazy<NavigatorInterop> navigatorInteropLazy;
        internal WindowInterop(IJSRuntime jsRuntime)
        {
            consoleInteropLazy = new Lazy<ConsoleInterop>(() => new ConsoleInterop(jsRuntime));
            navigatorInteropLazy = new Lazy<NavigatorInterop>(() => new NavigatorInterop(jsRuntime));
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
        public NavigatorInterop Navigator
        {
            get
            {
                return navigatorInteropLazy.Value;
            }
        }
    }
}
