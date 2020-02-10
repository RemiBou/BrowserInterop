using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BrowserInterop
{
    /// <summary>
    /// Give access to the direct sub-frames of the current window.
    /// </summary>
    public class FramesArrayInterop
    {
        private readonly JsRuntimeObjectRef jsRuntimeObjectRef;
        private readonly IJSRuntime jsRuntime;

        internal FramesArrayInterop(JsRuntimeObjectRef jsRuntimeObjectRef, IJSRuntime jsRuntime)
        {
            this.jsRuntimeObjectRef = jsRuntimeObjectRef;
            this.jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Give access to the direct sub-frames of the current window.
        /// </summary>
        /// <param name="index">Frame index</param>
        /// <returns></returns>
        public async Task<WindowInterop> Get(int index)
        {
            var jsObjectRef = await jsRuntime.GetInstancePropertyRefAsync(jsRuntimeObjectRef, $"frames[{index}]");

            return new WindowInterop(jsRuntime, jsObjectRef);
        }
        /// <summary>
        /// Count of direct subframes
        /// </summary>
        /// <returns></returns>
        public async Task<int> Length()
        {
            return await jsRuntime.GetInstancePropertyAsync<int>(jsRuntimeObjectRef, "frames.length");
        }
    }
}
