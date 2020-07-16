using System;

namespace BrowserInterop.Geolocation
{
    /// <summary>
    /// represents the position of the concerned device at a given time. The position, represented by a GeolocationCoordinates object, comprehends the 2D position of the device, on a spheroid representing the Earth, but also its altitude and its speed.
    /// </summary>
    public class GeolocationPosition
    {
        /// <summary>
        /// Returns a GeolocationCoordinates object defining the current location.
        /// </summary>
        /// <value></value>
        public GeolocationCoordinates Coords { get; set; }

        /// <summary>
        /// Represents the time at which the location was retrieved.
        /// </summary>
        /// <value></value>
        public long Timestamp { get; set; }

        /// <summary>
        /// Represents the time at which the location was retrieved.
        /// </summary>
        /// <value></value>
        public DateTimeOffset TimestampDateTime => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp);
    }
}