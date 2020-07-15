
using Microsoft.JSInterop;

using System;
using System.Threading.Tasks;

namespace BrowserInterop
{
    public class JsObjectWrapperBase : IAsyncDisposable
    {
        public JsRuntimeObjectRef JsRuntimeObjectRef { get; protected set; }
        protected IJSRuntime jsRuntime;
        public virtual void SetJsRuntime(IJSRuntime jsRuntime, JsRuntimeObjectRef jsObjectRef)
        {
            JsRuntimeObjectRef = jsObjectRef;
            this.jsRuntime = jsRuntime;
        }

        public async ValueTask DisposeAsync()
        {
            await JsRuntimeObjectRef.DisposeAsync();
        }

    }
}
