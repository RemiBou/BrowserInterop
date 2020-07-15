
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BrowserInterop
{
    public class BeforeInstallPromptEvent : JsObjectWrapperBase
    {

        /// <summary>
        ///  the platforms on which the event was dispatched. This is provided for user agents that want to present a choice of versions to the user such as, for example, "web" or "play" which would allow the user to chose between a web version or an Android version.
        /// </summary>
        /// <value></value>
        public string[] Platforms { get; set; }


        /// <summary>
        /// Returns the user choice
        /// </summary>
        /// <returns></returns>
        public async ValueTask<bool> IsAccepted()
        {
            return (await jsRuntime.GetInstancePropertyAsync<string>(this.JsRuntimeObjectRef, "userChoice") == "accepted");
        }

        /// <summary>
        /// Allows a developer to show the install prompt at a time of their own choosing. 
        /// </summary>
        /// <returns></returns>
        public async ValueTask Prompt()
        {
            await jsRuntime.InvokeInstanceMethod(JsRuntimeObjectRef, "prompt");
        }

    }
}
