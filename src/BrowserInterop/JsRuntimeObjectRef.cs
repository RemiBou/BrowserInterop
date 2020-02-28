using System.Text.Json.Serialization;

namespace BrowserInterop
{
    /// <summary>
    /// Represents a js object reference, send it to the js inteor api and it will be seen as an instance instead of a serialized/deserialized object
    /// </summary>
    public struct JsRuntimeObjectRef
    {
        [JsonPropertyName("__jsObjectRefId")]
        public int JsObjectRefId { get; set; }
    }

}
