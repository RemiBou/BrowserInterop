using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BrowserInterop
{
    /// <summary>
    /// Represents a js object reference, send it to the js inteor api and it will be seen as an instance instead of a serialized/deserialized object
    /// </summary>
    public class JsRuntimeObjectRef : IAsyncDisposable
    {
        internal IJSRuntime JSRuntime { get; set; }

        public JsRuntimeObjectRef()
        {
        }

        [JsonPropertyName("__jsObjectRefId")]
        public int JsObjectRefId { get; set; }

        public async ValueTask DisposeAsync()
        {
            await JSRuntime.InvokeVoidAsync("browserInterop.removeObjectRef", JsObjectRefId);
            JsObjectRefId = 0;
        }
    }

}
