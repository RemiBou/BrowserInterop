namespace BrowserInterop.Geolocation
{
    /// <summary>
    /// represents the position and altitude of the device on Earth, as well as the accuracy with which these properties are calculated.
    /// </summary>
    public class GeolocationCoordinates
    {
        /// <summary>
        ///  the position's latitude in decimal degrees.
        /// </summary>
        /// <value></value>
        public decimal Latitude { get; set; }

        /// <summary>
        /// the position's longitude in decimal degrees.
        /// </summary>
        /// <value></value>
        public decimal Longitude { get; set; }

        /// <summary>
        ///  the position's altitude in meters, relative to sea level. This value can be null if the implementation cannot provide the data.
        /// </summary>
        /// <value></value>
        public decimal? Altitude { get; set; }

        /// <summary>
        /// the accuracy of the latitude and longitude properties, expressed in meters.
        /// </summary>
        /// <value></value>
        public decimal Accuracy { get; set; }

        /// <summary>
        ///  the accuracy of the altitude expressed in meters. This value can be null.
        /// </summary>
        /// <value></value>
        public decimal? AltitudeAccuracy { get; set; }

        /// <summary>
        ///  the direction in which the device is traveling. This value, specified in degrees, indicates how far off from heading true north the device is. 0 degrees represents true north, and the direction is determined clockwise (which means that east is 90 degrees and west is 270 degrees). If speed is 0, ignore this value. If the device is unable to provide heading information, this value is null
        /// </summary>
        /// <value></value>
        public decimal? Heading { get; set; }

        /// <summary>
        /// the velocity of the device in meters per second. This value can be null.
        /// </summary>
        /// <value></value>
        public decimal? Speed { get; set; }
    }
}