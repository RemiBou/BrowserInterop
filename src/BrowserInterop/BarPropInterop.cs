using Microsoft.JSInterop;

using System.Threading.Tasks;

namespace BrowserInterop
{
    /// <summary>
    /// Represent property of a menu element
    /// </summary>
    public class BarPropInterop
    {
        private readonly JsRuntimeObjectRef windowRef;
        private readonly string propertyName;
        private readonly IJSRuntime jSRuntime;

        internal BarPropInterop(JsRuntimeObjectRef windowRef, string propertyName, IJSRuntime jSRuntime)
        {
            this.windowRef = windowRef;
            this.propertyName = propertyName;
            this.jSRuntime = jSRuntime;
        }

        /// <summary>
        /// Return true if the element is visible or not
        /// </summary>
        /// <returns></returns>
        public async ValueTask<bool> GetVisible()
        {
            return await jSRuntime.GetInstancePropertyAsync<bool>(windowRef, $"{propertyName}.visible");
        }

        /// <summary>
        /// Tries to change visibility of the element
        /// </summary>
        /// <param name="visible"></param>
        /// <returns></returns>
        public async ValueTask SetVisible(bool visible)
        {
            await jSRuntime.SetInstancePropertyAsync(windowRef, $"{propertyName}.visible", visible);
        }
    }
}
