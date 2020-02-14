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
        private readonly IJSRuntime jsRuntime;
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
            return (await jsRuntime.GetInstancePropertyAsync<decimal>(jsRuntimeObjectRef, "performance.timeOrigin")).HighResolutionTimeStampToDateTimeOffset();
        }

        /// <summary>
        /// removes the named mark from the browser's performance entry buffer. If the method is called with no arguments, all performance entries with an entry type of "mark" will be removed from the performance entry buffer.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task ClearMarks(string name = null)
        {
            await jsRuntime.InvokeInstanceMethodAsync(jsRuntimeObjectRef, "performance.clearMarks", name);
        }

        /// <summary>
        /// emoves the named measure from the browser's performance entry buffer. If the method is called with no arguments, all performance entries with an entry type of "measure" will be removed from the performance entry buffer.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task ClearMeasures(string name = null)
        {
            await jsRuntime.InvokeInstanceMethodAsync(jsRuntimeObjectRef, "performance.clearMeasures", name);
        }


        /// <summary>
        /// removes all performance entries with an entryType of "resource" from the browser's performance data buffer and sets the size of the performance data buffer to zero. To set the size of the browser's performance data buffer, use the Performance.setResourceTimingBufferSize() method.
        /// </summary>        
        /// <returns></returns>
        public async Task ClearResourceTimings()
        {
            await jsRuntime.InvokeInstanceMethodAsync(jsRuntimeObjectRef, "performance.clearResourceTimings");
        }

        /// <summary>
        /// returns a list of all PerformanceEntry objects for the page.
        /// </summary>
        /// <returns></returns>
        public async Task<PerformanceEntry[]> GetEntries()
        {
            return await jsRuntime.InvokeInstanceMethodAsync<PerformanceEntry[]>(jsRuntimeObjectRef, "performance.getEntries");
        }
    }
}