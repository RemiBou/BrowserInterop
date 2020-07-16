using System;
using System.Text.Json.Serialization;

namespace BrowserInterop.Geolocation
{
    /// <summary>
    /// Position getting options
    /// </summary>
    public class PositionOptions
    {
        /// <summary>
        /// maximum cached position age in ms
        /// </summary>
        /// <value></value>
        public double? MaximumAge { get; set; }

        /// <summary>
        /// Easy way to setupmax age property
        /// </summary>
        /// <value></value>
        [JsonIgnore]
        public TimeSpan? MaximumAgeTimeSpan
        {
            get => MaximumAge.HasValue ? TimeSpan.FromMilliseconds(MaximumAge.Value) : (TimeSpan?) null;
            set => MaximumAge = value?.TotalMilliseconds;
        }

        /// <summary>
        ///  integer (milliseconds) - amount of time before the error callback is invoked, if 0 it will never invoke.
        /// </summary>
        /// <value></value>
        public double? Timeout { get; set; }

        /// <summary>
        /// Easy way to setup timeout property
        /// </summary>
        /// <value></value>
        [JsonIgnore]
        public TimeSpan? TimeoutTimeSpan
        {
            get => Timeout.HasValue ? TimeSpan.FromMilliseconds(Timeout.Value) : (TimeSpan?) null;
            set => Timeout = value?.TotalMilliseconds;
        }

        /// <summary>
        ///  indicates the application would like to receive the best possible results. If true and if the device is able to provide a more accurate position, it will do so. Note that this can result in slower response times or increased power consumption (with a GPS chip on a mobile device for example). On the other hand, if false (the default value), the device can take the liberty to save resources by responding more quickly and/or using less power.
        /// </summary>
        /// <value></value>
        public bool EnableHighAccuracy { get; set; }
    }
}