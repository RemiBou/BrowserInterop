using Microsoft.JSInterop;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RemiBou.Blazor.BrowserInterop
{

    public class WindowInterop
    {
        private IJSRuntime jsRuntime;
        private ConsoleInterop consoleInterop;
        internal WindowInterop(IJSRuntime jsRuntime)
        {
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
                if (consoleInterop == null)
                {
                    consoleInterop = new ConsoleInterop(jsRuntime);
                }
                return consoleInterop;
            }
        }
    }
}
