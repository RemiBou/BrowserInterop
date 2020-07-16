
using Microsoft.JSInterop;

using System;
using System.Threading.Tasks;

namespace BrowserInterop.Extensions
{
    public class JsObjectWrapperBase : IAsyncDisposable
    {
        internal JsRuntimeObjectRef JsObjectRef { get; private set; }
        internal IJSRuntime JsRuntime { get; private set; }
        internal virtual void SetJsRuntime(IJSRuntime jsRuntime, JsRuntimeObjectRef jsObjectRef)
        {
            this.JsObjectRef = jsObjectRef ?? throw new ArgumentNullException(nameof(jsObjectRef));
            this.JsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
        }

        public async ValueTask DisposeAsync()
        {
            await JsObjectRef.DisposeAsync();
        }

    }
}
