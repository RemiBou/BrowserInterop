using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace RemiBou.Blazor.BrowserInterop
{
    /// <summary>
    /// Extension to the JSRuntime for using Browser API
    /// </summary>
    public static class BrowserInteropJsRuntimeExtension
    {

        private static bool ScriptInitialized = false;

        private static string Script = @"
        browserInterop = {
            getProperty: function(propertyName){
                var splitProperty = propertyName.split('.');
                var currentProperty = window;
                for(i = 0; i < splitProperty.length; i++){
                    currentProperty = currentProperty[splitProperty[i]];
                }
                return currentProperty;
            },
            navigator: {
                mimeTypes: function(){
                    var res = [];
                    for(i = 0; i <= navigator.mimeTypes.length; i++){
                        var mimeType = navigator.mimeTypes[i];
                        var current = {
                            type: mimeType.type,
                            suffix: mimeType.suffix,
                            description: mimeType.description
                        };
                        if(mimeType.enabledPlugin){
                            current.enabledPlugin = {
                                name: mimeType.enabledPlugin.name,
                                filename: mimeType.enabledPlugin.filename,
                                description: mimeType.enabledPlugin.description,
                                version: mimeType.enabledPlugin.version
                            }
                        }
                        res.push(current);
                    }
                    return res;
                },
                plugins: function(){
                    var res = [];
                    for(i = 0; i <= navigator.plugins.length; i++){
                        var plugin = navigator.plugins[i];
                        var current = {
                            name: plugin.name,
                            filename: plugin.filename,
                            description: plugin.description,
                            version: plugin.version
                        };
                        res.push(current);
                    }
                    return res;
                }

            }
        }
        ";

        /// <summary>
        /// Create a WIndowInterop instance that can be used for using Browser API
        /// </summary>
        /// <param name="jSRuntime"></param>
        /// <returns></returns>
        public static async Task<WindowInterop> Window(this IJSRuntime jSRuntime)
        {

            // I don't handle concurrent access, multiple initialization are not a problem and we can't await in a lock
            if (!ScriptInitialized)
            {
                await jSRuntime.InvokeVoidAsync("eval", Script);
                ScriptInitialized = true;
            }


            return new WindowInterop(jSRuntime);
        }
    }
}
