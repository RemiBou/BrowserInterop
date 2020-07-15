
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BrowserInterop
{
    public class BeforeUnloadEvent
    {
        private readonly IJSRuntime jsRuntime;
        private readonly JsRuntimeObjectRef jsRuntimeObjectRef;

        public BeforeUnloadEvent(IJSRuntime jsRuntime, JsRuntimeObjectRef jsRuntimeObjectRef)
        {
            this.jsRuntime = jsRuntime;
            this.jsRuntimeObjectRef = jsRuntimeObjectRef;
        }


        /// <summary>
        /// Prompt the user before quitting
        /// </summary>
        /// <returns></returns>
        public async ValueTask Prompt()
        {
            await jsRuntime.SetInstanceProperty(jsRuntimeObjectRef, "returnValue", false);
        }



    }
}
