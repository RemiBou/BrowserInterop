
using BrowserInterop.Extensions;

using System;
using System.Threading.Tasks;

namespace BrowserInterop
{
    /// <summary>
    /// The IdleDeadline interface is used as the data type of the input parameter to idle callbacks established by calling Window.requestIdleCallback()
    /// </summary>
    public class IdleDeadline : JsObjectWrapperBase
    {

        /// <summary>
        /// A Boolean whose value is true if the callback is being executed because the timeout specified when the idle callback was installed has expired.
        /// </summary>
        /// <value></value>
        public bool DidTimeout { get; set; }

        /// <summary>
        /// Returns a DOMHighResTimeStamp, which is a floating-point value providing an estimate of the number of milliseconds remaining in the current idle period. If the idle period is over, the value is 0. Your callback can call this repeatedly to see if there's enough time left to do more work before returning.
        /// </summary>
        /// <returns></returns>
        public async ValueTask<TimeSpan> TimeRemaining()
        {
            return TimeSpan.FromMilliseconds(await jsRuntime.InvokeInstanceMethodAsync<double>(jsObjectRef, "timeRemaining"));
        }
    }
}