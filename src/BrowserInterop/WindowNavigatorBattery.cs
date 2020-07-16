namespace BrowserInterop
{
    using Extensions;
    using System;
    using System.Text.Json.Serialization;

    public class WindowNavigatorBattery
    {
        /// <summary>
        /// A Boolean value indicating whether or not the battery is currently being charged.
        /// </summary>
        /// <value></value>
        public bool Charging { get; set; }

        /// <summary>
        /// A number representing the remaining time in seconds until the battery is fully charged, or 0 if the battery is already fully charged.
        /// </summary>
        /// <value></value>
        [JsonConverter(typeof(HandleSpecialDoublesAsStrings))]
        public double ChargingTime { get; set; }

        /// <summary>
        /// TimeSpan value of ChargingTime
        /// </summary>
        /// <returns></returns>
        public TimeSpan ChargingTimeSpan => TimeSpan.FromSeconds(ChargingTime);

        /// <summary>
        /// A number representing the remaining time in seconds until the battery is completely discharged and the system will suspend.
        /// </summary>
        /// <value></value>
        [JsonConverter(typeof(HandleSpecialDoublesAsStrings))]
        public double DischargingTime { get; set; }

        /// <summary>
        /// TimeSpan value of DischargingTime
        /// </summary>
        /// <returns></returns>
        public TimeSpan DischargingTimeSpan => TimeSpan.FromSeconds(DischargingTime);

        /// <summary>
        /// A number representing the system's battery charge level scaled to a value between 0.0 and 1.0.
        /// </summary>
        /// <value></value>
        public decimal Level { get; set; }
    }
}