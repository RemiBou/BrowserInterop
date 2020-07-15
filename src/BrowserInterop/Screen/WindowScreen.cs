using Microsoft.JSInterop;

namespace BrowserInterop.Screen
{
    /// <summary>
    /// The Screen interface represents a screen, usually the one on which the current window is being rendered
    /// </summary>
    public class WindowScreen : JsObjectWrapperBase
    {

        /// <summary>
        /// Returns the amount of horizontal space in pixels available to the window.
        /// </summary>
        /// <value></value>
        public int AvailWidth { get; set; }

        /// <summary>
        /// Specifies the height of the screen, in pixels, minus permanent or semipermanent user interface features displayed by the operating system, such as the Taskbar on Windows.
        /// </summary>
        /// <value></value>
        public int AvailHeight { get; set; }

        /// <summary>
        /// Returns the color depth of the screen.
        /// </summary>
        /// <value></value>
        public int ColorDepth { get; set; }

        /// <summary>
        /// Returns the height of the screen in pixels.
        /// </summary>
        /// <value></value>
        public int Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public ScreenOrientation Orientation { get; set; }

        /// <summary>
        /// Gets the bit depth of the screen.
        /// </summary>
        /// <value></value>
        public int PixelDepth { get; set; }

        /// <summary>
        /// Returns the width of the screen.
        /// </summary>
        /// <value></value>
        public int Width { get; set; }

        internal override void SetJsRuntime(IJSRuntime jsRuntime, JsRuntimeObjectRef screenRef)
        {
            base.SetJsRuntime(jsRuntime, screenRef);
            Orientation.SetJsRuntime(jsRuntime, screenRef);

        }
    }
}