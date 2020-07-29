using Microsoft.JSInterop;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BrowserInterop.Extensions
{
    /// <summary>
    /// This class enables using a C# action as a js callback function (like in event handling)
    /// </summary>
    public class CallBackInteropWrapper
    {
        [JsonPropertyName("__isCallBackWrapper")]
        // ReSharper disable once UnusedMember.Global
        public string IsCallBackWrapper { get; set; } = "";

        public object SerializationSpec { get; set; }

        public bool GetJsObjectRef { get; set; }


        private CallBackInteropWrapper()
        {
        }
        
        /// <summary>
        /// Create js interop wrapper for this c# action 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="serializationSpec">
        /// An object specifying the member we'll want from the JS object.
        /// "new { allChild = "*", onlyMember = true, ignore = false }" will get all the fields in allChild,
        /// the value of "onlyMember" and will ignore "ignore"
        /// "true" or null will get everything, false will get nothing
        /// </param>
        /// <param name="getJsObjectRef">If true (default false) the call back will get the js object ref instead of the js object content</param>
        /// <returns>A wrapper for the event handling</returns>
        public static CallBackInteropWrapper CreateWithResult<T, TResult>(Func<T, ValueTask<TResult>> callback, object serializationSpec = null,
            bool getJsObjectRef = false)
        {
            var res = new CallBackInteropWrapper
            {
                CallbackRef = DotNetObjectReference.Create(new JsInteropActionWrapperWithResult<T, TResult>(callback)),
                SerializationSpec = serializationSpec,
                GetJsObjectRef = getJsObjectRef
            };
            return res;
        }

        /// <summary>
        /// Create js interop wrapper for this c# action 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="serializationSpec">
        /// An object specifying the member we'll want from the JS object.
        /// "new { allChild = "*", onlyMember = true, ignore = false }" will get all the fields in allChild,
        /// the value of "onlyMember" and will ignore "ignore"
        /// "true" or null will get everything, false will get nothing
        /// </param>
        /// <param name="getJsObjectRef">If true (default false) the call back will get the js object ref instead of the js object content</param>
        /// <returns>A wrapper for the event handling</returns>
        public static CallBackInteropWrapper Create<T>(Func<T, ValueTask> callback, object serializationSpec = null,
            bool getJsObjectRef = false)
        {
            var res = new CallBackInteropWrapper
            {
                CallbackRef = DotNetObjectReference.Create(new JsInteropActionWrapper<T>(callback)),
                SerializationSpec = serializationSpec,
                GetJsObjectRef = getJsObjectRef
            };
            return res;
        }

        /// <summary>
        /// Create js interop wrapper for this c# action 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="serializationSpec">
        /// An object specifying the member we'll want from the JS object.
        /// "new { allChild = "*", onlyMember = true, ignore = false }" will get all the fields in allChild,
        /// the value of "onlyMember" and will ignore "ignore"
        /// "true" or null will get everything, false will get nothing
        /// </param>
        /// <param name="getJsObjectRef">If true (default false) the call back will get the js object ref instead of the js object content</param>
        /// <returns>Object that needs to be send to js interop api call</returns>
        public static CallBackInteropWrapper Create(Func<ValueTask> callback, object serializationSpec = null,
            bool getJsObjectRef = false)
        {
            var res = new CallBackInteropWrapper
            {
                CallbackRef = DotNetObjectReference.Create(new JsInteropActionWrapper(callback)),
                SerializationSpec = serializationSpec,
                GetJsObjectRef = getJsObjectRef
            };
            return res;
        }

        public object CallbackRef { get; set; }
    }
}