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

        /// <summary>
        /// Adds javascript side debounce to the callback (in milliseconds)
        /// see https://davidwalsh.name/javascript-debounce-function
        /// </summary>
        public double? Debounce { get; set; }

        /// <summary>
        /// Determines whether the callback should be invoked at the first occurrance 
        /// or if the invocation shall be postponed until the debounce timeout is expired.
        /// </summary>
        public bool Immediate { get; set; }

        /// <summary>
        /// Determines whether the callback shall be invoked always when the debounce time is expired
        /// or if it should be postponed until no event is incomming within one debounce time period.
        /// </summary>
        public bool TriggerPermanent { get; set; }

        /// <summary>
        /// Determines whether default values of the serialized callback data shall be transmitted
        /// </summary>
        public bool IncludeDefaults { get; set; }

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