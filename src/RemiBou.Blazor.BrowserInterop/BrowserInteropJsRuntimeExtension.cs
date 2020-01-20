using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace RemiBou.Blazor.BrowserInterop
{
    /// <summary>
    /// Extension to the JSRuntime for using Browser API
    /// </summary>
    public static class BrowserInteropJsRuntimeExtension
    {

        private static bool ScriptInitialized = false;

       

        /// <summary>
        /// Create a WIndowInterop instance that can be used for using Browser API
        /// </summary>
        /// <param name="jSRuntime"></param>
        /// <returns></returns>
        public static async Task<WindowInterop> Window(this IJSRuntime jSRuntime)
        {

            // I don't handle concurrent access, multiple initialization are not a problem and we can't await in a lock
            if (!ScriptInitialized)
            {
                var assembly = typeof(WindowInterop).Assembly;

                using var ressourceStream =assembly.GetManifestResourceStream("RemiBou.Blazor.BrowserInterop.scripts.js");
                using var ressourceReader = new StreamReader(ressourceStream);
                await jSRuntime.InvokeVoidAsync("eval",ressourceReader.ReadToEnd());
                ScriptInitialized = true;
            }


            return new WindowInterop(jSRuntime);
        }

        public static async Task<T> GetWindowProperty<T>(this IJSRuntime jsRuntime, string propertyPath)
        {
            return await jsRuntime.InvokeAsync<T>("browserInterop.getProperty",propertyPath);

        }
    }
}
