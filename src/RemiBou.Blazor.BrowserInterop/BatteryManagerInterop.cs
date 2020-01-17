using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace RemiBou.Blazor.BrowserInterop
{
    public class BatteryManagerInterop
    {
        private readonly IJSRuntime jsRuntime;

        internal BatteryManagerInterop(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Return whether or not the battery is currently being charged.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Charging()
        {
            return await jsRuntime.InvokeAsync<bool>("navigator.battery.charging");
        }

        /// <summary>
        /// Return the time it will take for the battery to be fully charged
        /// </summary>
        /// <returns></returns>
        public async Task<TimeSpan> ChargingTime()
        {
            return TimeSpan.FromSeconds(await jsRuntime.InvokeAsync<double>("navigator.battery.chargingTime"));
        }

        /// <summary>
        /// Return the time it will take for the battery to be fully uncharged
        /// </summary>
        /// <returns></returns>
        public async Task<TimeSpan> DischargingTime()
        {
            return TimeSpan.FromSeconds(await jsRuntime.InvokeAsync<double>("navigator.battery.dischargingTime"));
        }

        /// <summary>
        /// Return the charge level, a value between 0 and 1
        /// </summary>
        /// <returns></returns>
        public async Task<decimal> Level()
        {
            return await jsRuntime.InvokeAsync<decimal>("navigator.battery.level");
        }
    }
}