using System;
using System.Text.Json.Serialization;

namespace BrowserInterop
{
    /// <summary>
    /// contains properties specifying where an element should be scrolled to, and whether the scrolling should be smooth.
    /// </summary>
    public class ScrollToOptions
    {
        public ScrollToOptions()
        {
        }

        /// <summary>
        /// Specifies the number of pixels along the Y axis to scroll the window or element.
        /// </summary>
        /// <value></value>
        public ScrollToOptions(int top, int left, ScrollToOptionsBehaviorEnum behavior)
        {
            Top = top;
            Left = left;
            Behavior = behavior;

        }
        public int Top { get; set; }

        /// <summary>
        /// Specifies the number of pixels along the X axis to scroll the window or element.
        /// </summary>
        /// <value></value>
        public int Left { get; set; }

        /// <summary>
        /// Specifies whether the scrolling should animate smoothly, or happen instantly in a single jump.
        /// </summary>
        /// <value></value>
        [JsonIgnore]
        public ScrollToOptionsBehaviorEnum Behavior { get; set; }

        [JsonPropertyName("behavior")]

        public string BehaviorStr
        {
            get
            {
                return Behavior.ToString().ToLower();
            }
            set
            {
                Behavior = Enum.Parse<ScrollToOptionsBehaviorEnum>(value);
            }
        }

        public enum ScrollToOptionsBehaviorEnum
        {
            Auto,
            Smooth
        }
    }
}