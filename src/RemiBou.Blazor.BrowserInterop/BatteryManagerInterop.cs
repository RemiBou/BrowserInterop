using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace RemiBou.Blazor.BrowserInterop
{
    public class BatteryManagerInterop
    {
        private IJSRuntime jsRuntime;

        internal BatteryManagerInterop(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;

        }

        /// <summary>
        /// Return whether or not the battery is currently being charged.
        /// </summary>
        /// <returns></returns>
        public bool Charging { get; set; }

        protected double ChargingTime { get; set; }

        /// <summary>
        /// Return the time it will take for the battery to be fully charged
        /// </summary>
        /// <returns></returns>
        public TimeSpan ChargingTimeSpan
        {
            get
            {
                return TimeSpan.FromSeconds(ChargingTime);
            }
        }

        protected double DischargingTime { get; set; }

        /// <summary>
        /// Return the time it will take for the battery to be fully uncharged
        /// </summary>
        /// <returns></returns>
        public TimeSpan DischargingTimeSpan
        {
            get
            {
                return TimeSpan.FromSeconds(DischargingTime);
            }
        }

        /// <summary>
        /// Return the charge level, a value between 0 and 1
        /// </summary>
        /// <returns></returns>
        public decimal Level { get; set; }
    }
}