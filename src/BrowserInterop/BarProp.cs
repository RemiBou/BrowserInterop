using Microsoft.JSInterop;

using System.Threading.Tasks;

namespace BrowserInterop
{
    /// <summary>
    /// Represent property of a menu element
    /// </summary>
    public class BarProp
    {
        private readonly JsRuntimeObjectRef windowRef;
        private readonly string propertyName;
        private readonly IJSRuntime jsRuntime;

        internal BarProp(JsRuntimeObjectRef windowRef, string propertyName, IJSRuntime jsRuntime)
        {
            this.windowRef = windowRef;
            this.propertyName = propertyName;
            this.jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Return true if the element is visible or not
        /// </summary>
        /// <returns></returns>
        public async ValueTask<bool> GetVisible()
        {
            return await jsRuntime.GetInstancePropertyAsync<bool>(windowRef, $"{propertyName}.visible");
        }

        /// <summary>
        /// Tries to change visibility of the element
        /// </summary>
        /// <param name="visible"></param>
        /// <returns></returns>
        public async ValueTask SetVisible(bool visible)
        {
            await jsRuntime.SetInstanceProperty(windowRef, $"{propertyName}.visible", visible);
        }
    }
}
