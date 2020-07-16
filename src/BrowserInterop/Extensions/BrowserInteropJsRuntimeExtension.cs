
using Microsoft.JSInterop;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace BrowserInterop.Extensions
{
    /// <summary>
    /// Extension to the JSRuntime for using Browser API
    /// </summary>
    public static class BrowserInteropJsRuntimeExtension
    {
        /// <summary>
        /// Create a WIndowInterop instance that can be used for using Browser API
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static async ValueTask<WindowInterop> Window(this IJSRuntime jsRuntime)
        {
            JsRuntimeObjectRef jsObjectRef = await jsRuntime.GetInstanceProperty("window");
            WindowInterop wsInterop = await jsRuntime.GetInstancePropertyAsync<WindowInterop>(jsObjectRef, "self", WindowInterop.SerializationSpec);
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
        public static async ValueTask<JsRuntimeObjectRef> GetInstanceProperty(this IJSRuntime jsRuntime, string propertyPath)
        {
            return await jsRuntime.InvokeAsync<JsRuntimeObjectRef>("browserInterop.getPropertyRef", propertyPath);

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
        public static async ValueTask<T> GetInstancePropertyAsync<T>(this IJSRuntime jsRuntime, JsRuntimeObjectRef jsObjectRef, string propertyPath, Object serializationSpec = null)
        {
            return await jsRuntime.InvokeAsync<T>("browserInterop.getInstancePropertySerializable", jsObjectRef, propertyPath, serializationSpec);

        }

        /// <summary>
        /// Get the js object property value and initialize its js object reference
        /// </summary>
        /// <param name="jsRuntime">current js runtime</param>
        /// <param name="propertyPath">path of the property</param>
        /// <param name="jsObjectRef">Ref to the js object from which we'll get the property</param>
        /// <param name="deep">If true,(default) then the full object is received.await If false, only the object root</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async ValueTask<T> GetInstancePropertyWrapperAsync<T>(this IJSRuntime jsRuntime, JsRuntimeObjectRef jsObjectRef, string propertyPath, Object serializationSpec = null) where T : JsObjectWrapperBase
        {
            ValueTask<T> taskContent = GetInstancePropertyAsync<T>(jsRuntime, jsObjectRef, propertyPath, serializationSpec);
            ValueTask<JsRuntimeObjectRef> taskRef = GetInstancePropertyRef(jsRuntime, jsObjectRef, propertyPath);
            T res = await taskContent;
            JsRuntimeObjectRef jsRuntimeObjectRef = await taskRef;
            res.SetJsRuntime(jsRuntime, jsRuntimeObjectRef);
            return res;
        }

        /// <summary>
        /// Set the js object property value
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="propertyPath"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async ValueTask SetInstanceProperty(this IJSRuntime jsRuntime, JsRuntimeObjectRef jsObjectRef, string propertyPath, object value)
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
        public static async ValueTask<JsRuntimeObjectRef> GetInstancePropertyRef(this IJSRuntime jsRuntime, JsRuntimeObjectRef jsObjectRef, string propertyPath)
        {
            JsRuntimeObjectRef jsRuntimeObjectRef = await jsRuntime.InvokeAsync<JsRuntimeObjectRef>("browserInterop.getInstancePropertyRef", jsObjectRef, propertyPath);
            jsRuntimeObjectRef.JsRuntime = jsRuntime;
            return jsRuntimeObjectRef;
        }


        /// <summary>
        /// Call the method on the js instance
        /// </summary>
        /// <param name="jsRuntime1">Curent JS Runtime</param>
        /// <param name="windowObject">Reference to the JS instance</param>
        /// <param name="methodName">Methdod name/path </param>
        /// <param name="arguments">method arguments</param>
        /// <returns></returns>
        public static async ValueTask InvokeInstanceMethod(this IJSRuntime jsRuntime, JsRuntimeObjectRef windowObject, string methodName, params object[] arguments)
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
        public static async ValueTask<T> InvokeInstanceMethodAsync<T>(this IJSRuntime jsRuntime, JsRuntimeObjectRef windowObject, string methodName, params object[] arguments)
        {
            if (jsRuntime is null)
            {
                throw new ArgumentNullException(nameof(jsRuntime));
            }

            if (windowObject is null)
            {
                throw new ArgumentNullException(nameof(windowObject));
            }

            return await jsRuntime.InvokeAsync<T>("browserInterop.callInstanceMethod", new object[] { windowObject, methodName }.Concat(arguments).ToArray());
        }

        /// <summary>
        /// Get the js object content
        /// </summary>
        /// <param name="jsRuntime">Curent JS Runtime</param>
        /// <param name="jsObject">Reference to the JS instance</param>
        /// <returns></returns>
        public static async ValueTask<T> GetInstanceContent<T>(this IJSRuntime jsRuntime, JsRuntimeObjectRef jsObject, Object serializationSpec)
        {
            return await jsRuntime.InvokeAsync<T>("browserInterop.returnInstance", jsObject, serializationSpec);
        }

        /// <summary>
        /// Get the js object content updated
        /// </summary>
        /// <param name="jsRuntime">Curent JS Runtime</param>
        /// <param name="jsObject">The JS object for wich you want the content updated</param>
        /// <param name="serializationSpec"></param>
        /// <returns></returns>
        public static async ValueTask<T> GetInstanceContent<T>(this IJSRuntime jsRuntime, T jsObject, Object serializationSpec = null) where T : JsObjectWrapperBase
        {
            if (jsObject is null)
            {
                throw new ArgumentNullException(nameof(jsObject));
            }

            T res = await GetInstanceContent<T>(jsRuntime, jsObject.jsObjectRef, serializationSpec);
            res.SetJsRuntime(jsRuntime, jsObject.jsObjectRef);
            return res;
        }


        /// <summary>
        /// Call the method on the js instance and return the reference to the js object
        /// </summary>
        /// <param name="jsRuntime1">Curent JS Runtime</param>
        /// <param name="windowObject">Reference to the JS instance</param>
        /// <param name="methodName">Methdod name/path </param>
        /// <param name="arguments">method arguments</param>
        /// <returns></returns>
        public static async ValueTask<JsRuntimeObjectRef> InvokeInstanceMethodGetRef(this IJSRuntime jsRuntime, JsRuntimeObjectRef windowObject, string methodName, params object[] arguments)
        {
            if (jsRuntime is null)
            {
                throw new ArgumentNullException(nameof(jsRuntime));
            }

            JsRuntimeObjectRef jsRuntimeObjectRef = await jsRuntime.InvokeAsync<JsRuntimeObjectRef>("browserInterop.callInstanceMethodGetRef", new object[] { windowObject, methodName }.Concat(arguments).ToArray());
            jsRuntimeObjectRef.JsRuntime = jsRuntime;
            return jsRuntimeObjectRef;
        }

        public static async ValueTask<bool> HasProperty(this IJSRuntime jsRuntime, JsRuntimeObjectRef jsObject, string propertyPath)
        {
            return await jsRuntime.InvokeAsync<bool>("browserInterop.hasProperty", jsObject, propertyPath);
        }

        /// <summary>
        /// Add an event listener to the given property and event Type
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="jsRuntimeObject"></param>
        /// <param name="propertyName"></param>
        /// <param name="eventName"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public static async ValueTask<IAsyncDisposable> AddEventListener(this IJSRuntime jsRuntime, JsRuntimeObjectRef jsRuntimeObject, string propertyName, string eventName, CallBackInteropWrapper callBack)
        {
            int listenerId = await jsRuntime.InvokeAsync<int>("browserInterop.addEventListener", jsRuntimeObject, propertyName, eventName, callBack);

            return new ActionAsyncDisposable(async () => await jsRuntime.InvokeVoidAsync("browserInterop.removeEventListener", jsRuntimeObject, propertyName, eventName, listenerId));
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
                //when timeout is reached, it raises an exception
                return await Task.FromResult(default(T)).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Return the value of a DOMHighResTimeStamp to DateTimeOffset
        /// </summary>
        /// <param name="timeStamp">value of a DOMHighResTimeStamp</param>
        /// <returns></returns>
        public static DateTimeOffset HighResolutionTimeStampToDateTimeOffset(this double timeStamp)
        {
            long ms = (long)Math.Floor(timeStamp);
            long tick = (long)Math.Floor((timeStamp - ms) * 10000);
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



}
