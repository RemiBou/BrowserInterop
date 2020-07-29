using BrowserInterop.Extensions;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrowserInterop.Storage
{
    /// <summary>
    /// provides access to a particular domain's session or local storage. It allows, for example, the addition, modification, or deletion of stored data items.
    /// </summary>
    public class WindowStorage
    {
        private readonly IJSRuntime jsRuntime;
        private JsRuntimeObjectRef jsRuntimeObjectRef;
        private readonly JsRuntimeObjectRef windowRuntimeObjectRef;
        private readonly string memberName;

        internal WindowStorage(IJSRuntime jsRuntime, JsRuntimeObjectRef windowRuntimeObjectRef, string memberName)
        {
            this.jsRuntime = jsRuntime;
            this.windowRuntimeObjectRef = windowRuntimeObjectRef;
            this.memberName = memberName;
        }


        internal WindowStorage(IJSRuntime jsRuntime, JsRuntimeObjectRef runtimeObjectRef)
        {
            this.jsRuntime = jsRuntime;
            jsRuntimeObjectRef = runtimeObjectRef;
        }

        /// <summary>
        ///  the number of data items stored in the Storage object.
        ///  </summary>
        /// <returns></returns>
        public async ValueTask<int> Length()
        {
            return await jsRuntime.GetInstanceProperty<int>(await GetJsRuntimeObjectRef().ConfigureAwait(false), "length").ConfigureAwait(false);
        }

        /// <summary>
        /// returns the name of the nth key in a given Storage object. The order of keys is user-agent defined, so you should not rely on it.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public async ValueTask<string> Key(int index)
        {
            return await jsRuntime.InvokeInstanceMethod<string>(await GetJsRuntimeObjectRef().ConfigureAwait(false), "key", index).ConfigureAwait(false);
        }

        /// <summary>
        /// will return that key's value, or null if the key does not exist, in the given Storage object. As the data is stored serialized in json, we'll try to deserialize it
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public async ValueTask<T> GetItem<T>(string keyName)
        {
            var strValue =
                await jsRuntime.InvokeInstanceMethod<string>(await GetJsRuntimeObjectRef().ConfigureAwait(false), "getItem", keyName).ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(strValue);
        }

        /// <summary>
        ///  will add that key to the storage (serialized as json), or update that key's value if it already exists.
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="value">Value to be stored (will be serialized in json)</param>
        /// <returns></returns>
        public async ValueTask SetItem(string keyName, object value)
        {
            await jsRuntime.InvokeInstanceMethod(await GetJsRuntimeObjectRef().ConfigureAwait(false), "setItem", keyName,
                JsonSerializer.Serialize(value)).ConfigureAwait(false);
        }

        /// <summary>
        ///  will remove that key from the storage.
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public async ValueTask RemoveItem(string keyName)
        {
            await jsRuntime.InvokeInstanceMethod(await GetJsRuntimeObjectRef().ConfigureAwait(false), "removeItem", keyName).ConfigureAwait(false);
        }

        /// <summary>
        ///  will empty all keys out of the storage..
        /// </summary>
        /// <returns></returns>
        public async ValueTask Clear()
        {
            await jsRuntime.InvokeInstanceMethod(await GetJsRuntimeObjectRef().ConfigureAwait(false), "clear").ConfigureAwait(false);
        }


        private async ValueTask<JsRuntimeObjectRef> GetJsRuntimeObjectRef()
        {
            return jsRuntimeObjectRef ??= await jsRuntime.GetInstancePropertyRef(windowRuntimeObjectRef, memberName).ConfigureAwait(false);
        }
    }
}