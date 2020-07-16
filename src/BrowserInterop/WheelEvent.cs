namespace BrowserInterop
{
    public class WheelEvent
    {
        /// <summary>
        /// the horizontal scroll amount.
        /// </summary>
        /// <value></value>
        public decimal DeltaX { get; set; }

        /// <summary>
        ///  the vertical scroll amount.
        /// </summary>
        /// <value></value>
        public decimal DeltaY { get; set; }

        /// <summary>
        ///  the scroll amount for the z-axis.
        /// </summary>
        /// <value></value>
        public decimal DeltaZ { get; set; }

        public int DeltaMode { get; set; }

        public DeltaModeEnum DeltaModeEnum => (DeltaModeEnum) DeltaMode;
    }

    public enum DeltaModeEnum
    {
        Pixel = 0,
        Line = 1,
        Page = 2
    }
}