using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BrowserInterop
{
    /// <summary>
    /// provides access to a particular domain's session or local storage. It allows, for example, the addition, modification, or deletion of stored data items.
    /// </summary>
    public class StorageInterop
    {
        private IJSRuntime jsRuntime;
        private JsRuntimeObjectRef jsRuntimeObjectRef;
        private readonly string memberName;

        internal StorageInterop(IJSRuntime jsRuntime, JsRuntimeObjectRef jsRuntimeObjectRef, string memberName)
        {
            this.jsRuntime = jsRuntime;
            this.jsRuntimeObjectRef = jsRuntimeObjectRef;
            this.memberName = memberName;
        }

        /// <summary>
        ///  the number of data items stored in the Storage object.
        ///  </summary>
        /// <returns></returns>
        public async Task<int> Length()
        {
            return await jsRuntime.GetInstancePropertyAsync<int>(jsRuntimeObjectRef, memberName + ".length");
        }

        /// <summary>
        /// returns the name of the nth key in a given Storage object. The order of keys is user-agent defined, so you should not rely on it.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<string> Key(int index)
        {
            return await jsRuntime.InvokeInstanceMethodAsync<string>(jsRuntimeObjectRef, memberName + ".key", index);
        }

        /// <summary>
        /// will return that key's value, or null if the key does not exist, in the given Storage object. As the data is stored serialized in json, we'll try to deserialize it
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public async Task<T> GetItem<T>(string keyName)
        {
            var strValue = await jsRuntime.InvokeInstanceMethodAsync<string>(jsRuntimeObjectRef, memberName + ".getItem", keyName);
            return JsonSerializer.Deserialize<T>(strValue);
        }

        /// <summary>
        ///  will add that key to the storage (serialized as json), or update that key's value if it already exists.
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public async Task SetItem(string keyName, object value)
        {
            await jsRuntime.InvokeInstanceMethodAsync(jsRuntimeObjectRef, memberName + ".setItem", keyName, JsonSerializer.Serialize(value));
        }

        /// <summary>
        ///  will remove that key from the storage.
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public async Task RemoveItem(string keyName)
        {
            await jsRuntime.InvokeInstanceMethodAsync(jsRuntimeObjectRef, memberName + ".removeItem", keyName);
        }

        /// <summary>
        ///  will empty all keys out of the storage..
        /// </summary>
        /// <returns></returns>
        public async Task Clear()
        {
            await jsRuntime.InvokeInstanceMethodAsync(jsRuntimeObjectRef, memberName + ".clear");
        }
    }
}