
using Microsoft.JSInterop;

using System;
using System.Threading.Tasks;

namespace BrowserInterop.Extensions
{
    public class JsObjectWrapperBase : IAsyncDisposable
    {
        internal JsRuntimeObjectRef jsObjectRef { get; private set; }
        internal IJSRuntime jsRuntime { get; private set; }
        internal virtual void SetJsRuntime(IJSRuntime jsRuntime, JsRuntimeObjectRef jsObjectRef)
        {
            this.jsObjectRef = jsObjectRef ?? throw new ArgumentNullException(nameof(jsObjectRef));
            this.jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
        }

        public async ValueTask DisposeAsync()
        {
            await jsObjectRef.DisposeAsync();
        }

    }
}
