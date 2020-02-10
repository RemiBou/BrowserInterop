using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BrowserInterop
{
    public class FramesArrayInterop
    {
        private readonly JsRuntimeObjectRef jsRuntimeObjectRef;
        private readonly IJSRuntime jsRuntime;

        public FramesArrayInterop(JsRuntimeObjectRef jsRuntimeObjectRef, IJSRuntime jsRuntime)
        {
            this.jsRuntimeObjectRef = jsRuntimeObjectRef;
            this.jsRuntime = jsRuntime;
        }

        public async Task<WindowInterop> Get(int index)
        {
            var jsObjectRef = await jsRuntime.GetInstancePropertyRefAsync(jsRuntimeObjectRef, $"frames[{index}]");

            return new WindowInterop(jsRuntime, jsObjectRef);
        }

        public async Task<int> Length()
        {
            return await jsRuntime.GetInstancePropertyAsync<int>(jsRuntimeObjectRef, "frames.length");
        }
    }
}
