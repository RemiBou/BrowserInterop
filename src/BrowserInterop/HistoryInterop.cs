using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BrowserInterop
{
    public class HistoryInterop
    {
        private IJSRuntime jsRuntime;
        private JsRuntimeObjectRef jsRuntimeObjectRef;

        /// <summary>
        /// Represents the number of elements in the session history, including the currently loaded page. For example, for a page loaded in a new tab this property returns 1.
        /// </summary>
        /// <value></value>
        public int Length { get; set; }

        internal void SetJSRuntime(IJSRuntime jsRuntime, JsRuntimeObjectRef jsRuntimeObjectRef)
        {
            this.jsRuntime = jsRuntime;
            this.jsRuntimeObjectRef = jsRuntimeObjectRef;
        }

        /// <summary>
        ///  allows web applications to explicitly set default scroll restoration behavior on history navigation.
        /// </summary>
        /// <value></value>
        public string ScrollRestauration { get; set; }

        public ScrollRestaurationEnum ScrollRestaurationEnum
        {
            get
            {
                return Enum.Parse<ScrollRestaurationEnum>(ScrollRestauration);
            }
        }

        public async Task SetScrollRestauration(ScrollRestaurationEnum value)
        {
            await jsRuntime.SetInstancePropertyAsync(jsRuntimeObjectRef, "history.scrollRestoration", value.ToString().ToLower());
        }


    }
    public enum ScrollRestaurationEnum
    {
        ///<summary>The location on the page to which the user has scrolled will be restored.</summary>
        Auto,
        ///<summary>The location on the page is not restored. The user will have to scroll to the location manually.</summary>
        Manual
    }
}