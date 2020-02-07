using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BrowserInterop
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

                using var ressourceStream = assembly.GetManifestResourceStream("BrowserInterop.scripts.js");
                using var ressourceReader = new StreamReader(ressourceStream);
                await jSRuntime.InvokeVoidAsync("eval", ressourceReader.ReadToEnd());
                ScriptInitialized = true;
            }


            return new WindowInterop(jSRuntime);
        }

        public static async Task<T> GetWindowProperty<T>(this IJSRuntime jsRuntime, string propertyPath)
        {
            return await jsRuntime.InvokeAsync<T>("browserInterop.getProperty", propertyPath);

        }

        public static async Task<bool> HasProperty(this IJSRuntime jsRuntime, string propertyPath)
        {
            return await jsRuntime.InvokeAsync<bool>("browserInterop.hasProperty", propertyPath);
        }

        public static async Task<IAsyncDisposable> AddEventListener(this IJSRuntime jsRuntime, string propertyName, string eventName, Func<Task> callBack)
        {
            JSInteropActionWrapper actionWrapper = new JSInteropActionWrapper(jsRuntime, callBack);
            var listenerId = await jsRuntime.InvokeAsync<int>("browserInterop.addEventListener", propertyName, eventName, DotNetObjectReference.Create(actionWrapper));
            actionWrapper.SeListenerId(listenerId);
            return actionWrapper;
        }

        /// <summary>
        /// Invoke the specified method with JSInterop and returns default(T) if the timeout is reached
        /// </summary>
        /// <param name="jsRuntime">js runtime on which we'll execute the query</param>
        /// <param name="identifier">method identifier</param>
        /// <param name="timeout">timeout until e return default(T)</param>
        /// <param name="args">method arguments</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async ValueTask<T> InvokeOrDefaultAsync<T>(this IJSRuntime jsRuntime, string identifier, TimeSpan timeout, params object[] args)
        {
            try
            {
                return await JSRuntimeExtensions.InvokeAsync<T>(
                    jsRuntime: jsRuntime,
                    identifier: identifier,
                    timeout: timeout,
                    args: args);
            }
            catch (TaskCanceledException)
            {
                //when timeout is reached it raises an exception
                return await Task.FromResult(default(T));
            }
        }
    }

    public readonly struct JsRuntimeObjectRef
    {
        private readonly int id;

        public JsRuntimeObjectRef(int id)
        {
            this.id = id;
        }

        internal int Id => id;
    }
}
