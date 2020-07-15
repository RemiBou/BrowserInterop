
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BrowserInterop
{

    public partial class WindowInterop
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
                await jsRuntime.SetInstancePropertyAsync(jsRuntimeObjectRef, "returnValue", false);
            }


        }
    }
}
