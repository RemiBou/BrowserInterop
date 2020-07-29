using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BrowserInterop.Extensions
{
    public abstract class JsObjectWrapperBase : IAsyncDisposable
    {
        protected JsRuntimeObjectRef JsObjectRef { get; private set; }
        protected IJSRuntime JsRuntime { get; private set; }

        internal virtual void SetJsRuntime(IJSRuntime jsRuntime, JsRuntimeObjectRef jsObjectRef)
        {
            JsObjectRef = jsObjectRef ?? throw new ArgumentNullException(nameof(jsObjectRef));
            JsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
        }

        public async ValueTask DisposeAsync()
        {
            await JsObjectRef.DisposeAsync();
        }
    }
}
