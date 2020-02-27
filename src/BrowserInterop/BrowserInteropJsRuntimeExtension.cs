using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
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
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static async Task<WindowInterop> Window(this IJSRuntime jsRuntime)
        {

            // I don't handle concurrent access, multiple initialization are not a problem and we can't await in a lock
            if (!ScriptInitialized)
            {
                var assembly = typeof(WindowInterop).Assembly;

                using var ressourceStream = assembly.GetManifestResourceStream("BrowserInterop.scripts.js");
                using var ressourceReader = new StreamReader(ressourceStream);
                await jsRuntime.InvokeVoidAsync("eval", ressourceReader.ReadToEnd());
                ScriptInitialized = true;
            }
            var jsObjectRef = await jsRuntime.GetInstancePropertyAsync<JsRuntimeObjectRef>("window");
            var wsInterop = await jsRuntime.GetInstancePropertyAsync<WindowInterop>(jsObjectRef, "self", false);
            wsInterop.SetJsRuntime(jsRuntime, jsObjectRef);
            return wsInterop;
        }

        /// <summary>
        /// Get the window js object property value reference
        /// </summary>
        /// <param name="jsRuntime">current js runtime</param>
        /// <param name="propertyPath">path of the property</param>
        /// <param name="jsObjectRef">Ref to the js object from which we'll get the property</param>
        /// <param name="deep">If true,(default) then the full object is received.await If false, only the object root</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> GetInstancePropertyAsync<T>(this IJSRuntime jsRuntime, string propertyPath)
        {
            return await jsRuntime.InvokeAsync<T>("browserInterop.getPropertyRef", propertyPath);

        }

        /// <summary>
        /// Get the js object property value
        /// </summary>
        /// <param name="jsRuntime">current js runtime</param>
        /// <param name="propertyPath">path of the property</param>
        /// <param name="jsObjectRef">Ref to the js object from which we'll get the property</param>
        /// <param name="deep">If true,(default) then the full object is received.await If false, only the object root</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> GetInstancePropertyAsync<T>(this IJSRuntime jsRuntime, JsRuntimeObjectRef jsObjectRef, string propertyPath, bool deep = true)
        {
            return await jsRuntime.InvokeAsync<T>("browserInterop.getInstancePropertySerializable", jsObjectRef, propertyPath, deep);

        }

        /// <summary>
        /// Set the js object property value
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="propertyPath"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task SetInstancePropertyAsync(this IJSRuntime jsRuntime, JsRuntimeObjectRef jsObjectRef, string propertyPath, object value)
        {
            await jsRuntime.InvokeVoidAsync("browserInterop.setInstanceProperty", jsObjectRef, propertyPath, value);

        }

        /// <summary>
        /// Return a reference to the JS instance located on the given property 
        /// </summary>
        /// <param name="jsRuntime">Current JS rntime</param>
        /// <param name="jsObjectRef">Refernece to the parent instance</param>
        /// <param name="propertyPath">property path</param>
        /// <returns></returns>
        public static async Task<JsRuntimeObjectRef> GetInstancePropertyRefAsync(this IJSRuntime jsRuntime, JsRuntimeObjectRef jsObjectRef, string propertyPath)
        {
            return await jsRuntime.InvokeAsync<JsRuntimeObjectRef>("browserInterop.getInstancePropertyRef", jsObjectRef, propertyPath);
        }


        /// <summary>
        /// Call the method on the js instance
        /// </summary>
        /// <param name="jsRuntime1">Curent JS Runtime</param>
        /// <param name="windowObject">Reference to the JS instance</param>
        /// <param name="methodName">Methdod name/path </param>
        /// <param name="arguments">method arguments</param>
        /// <returns></returns>
        public static async Task InvokeInstanceMethodAsync(this IJSRuntime jsRuntime, JsRuntimeObjectRef windowObject, string methodName, params object[] arguments)
        {
            await jsRuntime.InvokeVoidAsync("browserInterop.callInstanceMethod", new object[] { windowObject, methodName }.Concat(arguments).ToArray());
        }

        /// <summary>
        /// Call the method on the js instance and return the result
        /// </summary>
        /// <param name="jsRuntime1">Curent JS Runtime</param>
        /// <param name="windowObject">Reference to the JS instance</param>
        /// <param name="methodName">Methdod name/path </param>
        /// <param name="arguments">method arguments</param>
        /// <returns></returns>
        public static async Task<T> InvokeInstanceMethodAsync<T>(this IJSRuntime jsRuntime, JsRuntimeObjectRef windowObject, string methodName, params object[] arguments)
        {
            return await jsRuntime.InvokeAsync<T>("browserInterop.callInstanceMethod", new object[] { windowObject, methodName }.Concat(arguments).ToArray());
        }

        /// <summary>
        /// Get the js object content
        /// </summary>
        /// <param name="jsRuntime1">Curent JS Runtime</param>
        /// <param name="windowObject">Object holding reference to the js object</param>
        /// <returns></returns>
        public static async Task<T> GetInstanceContent<T>(this IJSRuntime jsRuntime, T jsObject, bool deep) where T : IJsObjectWrapper
        {
            var content = await jsRuntime.InvokeAsync<T>("browserInterop.returnInstance", jsObject.JsRuntimeObjectRef, deep);
            content.SetJsRuntime(jsRuntime, jsObject.JsRuntimeObjectRef);
            return content;
        }

        /// <summary>
        /// Get the js object content
        /// </summary>
        /// <param name="jsRuntime1">Curent JS Runtime</param>
        /// <param name="windowObject">Reference to the JS instance</param>
        /// <returns></returns>
        public static async Task<T> GetInstanceContent<T>(this IJSRuntime jsRuntime, JsRuntimeObjectRef jsObject, bool deep)
        {
            var content = await jsRuntime.InvokeAsync<T>("browserInterop.returnInstance", jsObject, deep);
            return content;
        }


        /// <summary>
        /// Call the method on the js instance and return the reference to the js object
        /// </summary>
        /// <param name="jsRuntime1">Curent JS Runtime</param>
        /// <param name="windowObject">Reference to the JS instance</param>
        /// <param name="methodName">Methdod name/path </param>
        /// <param name="arguments">method arguments</param>
        /// <returns></returns>
        public static async Task<JsRuntimeObjectRef> InvokeInstanceMethodGetRefAsync(this IJSRuntime jsRuntime, JsRuntimeObjectRef windowObject, string methodName, params object[] arguments)
        {
            return await jsRuntime.InvokeAsync<JsRuntimeObjectRef>("browserInterop.callInstanceMethodGetRef", new object[] { windowObject, methodName }.Concat(arguments).ToArray());
        }

        public static async Task<bool> HasProperty(this IJSRuntime jsRuntime, JsRuntimeObjectRef jsObject, string propertyPath)
        {
            return await jsRuntime.InvokeAsync<bool>("browserInterop.hasProperty", jsObject, propertyPath);
        }

        public static async Task<IAsyncDisposable> AddEventListener(this IJSRuntime jsRuntime, JsRuntimeObjectRef jsRuntimeObject, string propertyName, string eventName, Func<Task> callBack)
        {
            JSInteropActionWrapper actionWrapper = new JSInteropActionWrapper(jsRuntime, callBack);
            var listenerId = await jsRuntime.InvokeAsync<int>("browserInterop.addEventListener", jsRuntimeObject, propertyName, eventName, DotNetObjectReference.Create(actionWrapper));
            actionWrapper.SeListenerId(listenerId);
            return actionWrapper;
        }

        /// <summary>
        /// Attach an event handler to the given event on the given js object property
        /// </summary>
        /// <param name="jsRuntime">Current JSRuntime</param>
        /// <param name="jsRuntimeObject">JS Object on which you want to listen to event</param>
        /// <param name="propertyName">Property that will emit the event, "" if it's the js object itself</param>
        /// <param name="eventName">The event name</param>        
        /// <param name="callBack">Called method when the event is raised</param>
        /// <param name="callbackWithRefToJsObject">if true then the event callback object parameters will be sent to c# as JsRuntimeObjectRef instead of their value serialized </param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<IAsyncDisposable> AddEventListener<T>(
            this IJSRuntime jsRuntime,
            JsRuntimeObjectRef jsRuntimeObject,
            string propertyName,
            string eventName,
            Func<T, Task> callBack,
            bool callbackWithRefToJsObject = false)
        {
            JSInteropActionWrapper<T> actionWrapper = new JSInteropActionWrapper<T>(jsRuntime, callBack);
            var listenerId = await jsRuntime.InvokeAsync<int>("browserInterop.addEventListener", jsRuntimeObject, propertyName, eventName, DotNetObjectReference.Create(actionWrapper), callbackWithRefToJsObject);
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

        /// <summary>
        /// Return the value of a DOMHighResTimeStamp to DateTimeOffset
        /// </summary>
        /// <param name="timeStamp">value of a DOMHighResTimeStamp</param>
        /// <returns></returns>
        public static DateTimeOffset HighResolutionTimeStampToDateTimeOffset(this double timeStamp)
        {
            var ms = (long)Math.Floor(timeStamp);
            var tick = (long)Math.Floor((timeStamp - ms) * 10000);
            return DateTimeOffset.FromUnixTimeMilliseconds(ms).AddTicks(tick);
        }

        /// <summary>
        /// Return the value of a DOMHighResTimeStamp to TimeSpan
        /// </summary>
        /// <param name="timeStamp">value of a DOMHighResTimeStamp</param>
        /// <returns></returns>
        public static TimeSpan HighResolutionTimeStampToTimeSpan(this double timeStamp)
        {
            return TimeSpan.FromTicks((long)timeStamp * 10000);
        }
    }

    public struct JsRuntimeObjectRef
    {
        [JsonPropertyName("__jsObjectRefId")]
        public int JsObjectRefId { get; set; }
    }

    internal class CallBackWrapper<T> where T : class
    {
        public CallBackWrapper(DotNetObjectReference<T> dotNetObjectRef)
        {
            DotNetObjectRef = dotNetObjectRef;
        }

        public string IsCallBackWrapper { get; set; } = "IsCallBackWrapper";
        public DotNetObjectReference<T> DotNetObjectRef { get; set; }
    }
}
