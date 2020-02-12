using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BrowserInterop
{
    /// <summary>
    /// Represent property of a menu element
    /// </summary>
    public class BarPropInterop
    {
        private JsRuntimeObjectRef jsRuntimeObjectRef;
        private string propertyName;
        private IJSRuntime jSRuntime;

        internal BarPropInterop(JsRuntimeObjectRef jsRuntimeObjectRef, string propertyName, IJSRuntime jSRuntime)
        {
            this.jsRuntimeObjectRef = jsRuntimeObjectRef;
            this.propertyName = propertyName;
            this.jSRuntime = jSRuntime;
        }

        /// <summary>
        /// Return true if the element is visible or not
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetVisible()
        {
            return await jSRuntime.GetInstancePropertyAsync<bool>(jsRuntimeObjectRef, $"{propertyName}.visible");
        }

        /// <summary>
        /// Tries to change visibility of the element
        /// </summary>
        /// <param name="visible"></param>
        /// <returns></returns>
        public async Task SetVisible(bool visible)
        {
            await jSRuntime.SetInstancePropertyAsync(jsRuntimeObjectRef, $"{propertyName}.visible", visible);
        }
    }
}
