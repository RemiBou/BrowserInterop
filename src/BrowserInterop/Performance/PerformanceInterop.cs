using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BrowserInterop.Performance
{
    /// <summary>
    /// provides access to performance-related information for the current page.
    /// </summary>
    public class PerformanceInterop
    {
        private IJSRuntime jsRuntime;
        private JsRuntimeObjectRef jsRuntimeObjectRef;

        internal PerformanceInterop(IJSRuntime jsRuntime, JsRuntimeObjectRef jsRuntimeObjectRef)
        {
            this.jsRuntime = jsRuntime;
            this.jsRuntimeObjectRef = jsRuntimeObjectRef;
        }


        /// <summary>
        /// Represents the time at which the location was retrieved.
        /// </summary>
        /// <value></value>
        public async Task<DateTimeOffset> TimeOrigin()
        {
            var time = await jsRuntime.GetInstancePropertyAsync<decimal>(jsRuntimeObjectRef, "performance.timeOrigin");
            var ms = (long)Math.Floor(time);
            var tick = (long)Math.Floor((time - ms) * 10000);
            Console.WriteLine($"time {time} md {ms} tick {tick}");
            return DateTimeOffset.FromUnixTimeMilliseconds(ms).AddTicks(tick);
        }

        /// <summary>
        /// removes the named mark from the browser's performance entry buffer. If the method is called with no arguments, all performance entries with an entry type of "mark" will be removed from the performance entry buffer.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task ClearMarks(string name = null)
        {
            await jsRuntime.InvokeInstanceMethodAsync(jsRuntimeObjectRef, "clearMarks", name);
        }
    }
}