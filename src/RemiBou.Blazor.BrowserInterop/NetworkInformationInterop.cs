using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace RemiBou.Blazor.BrowserInterop
{
    public class NetworkInformationInterop
    {
        private readonly IJSRuntime jsRuntime;

        public NetworkInformationInterop(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Returns the effective bandwidth estimate in megabits per second, rounded to the nearest multiple of 25 kilobits per seconds.
        /// </summary>
        /// <returns></returns>
        public async Task<double> Downlink()
        {
            return await jsRuntime.InvokeAsync<double>("navigator.network.downlink");
        }

        /// <summary>
        /// Returns the maximum downlink speed, in megabits per second (Mbps), for the underlying connection technology.
        /// </summary>
        /// <returns></returns>
        public async Task<double> DownlinkMax()
        {
            return await jsRuntime.InvokeAsync<double>("navigator.network.downlinkMax");
        }

        /// <summary>
        /// Returns the maximum downlink speed, in megabits per second (Mbps), for the underlying connection technology.
        /// </summary>
        /// <returns></returns>
        public async Task<EffectiveTypeEnum> EffectiveType()
        {
            return await jsRuntime.InvokeAsync<string>("navigator.network.effectiveType") switch
            {
                "slow-2g" => EffectiveTypeEnum.Slow2G,
                "2g" => EffectiveTypeEnum._2G,
                "3g" => EffectiveTypeEnum._3G,
                "4g" => EffectiveTypeEnum._4G,
                _ => EffectiveTypeEnum.Unknown,
            };
        }

        /// <summary>
        /// Returns the estimated effective round-trip time of the current connection, rounded to the nearest multiple of 25 milliseconds
        /// </summary>
        /// <returns></returns>
        public async Task<double> Rtt()
        {
            return await jsRuntime.InvokeAsync<double>("navigator.network.rtt");
        }

        /// <summary>
        /// Returns true if the user has set a reduced data usage option on the user agent.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveData()
        {
            return await jsRuntime.InvokeAsync<bool>("navigator.network.saveData");
        }

        /// <summary>
        /// Returns true if the user has set a reduced data usage option on the user agent.
        /// </summary>
        /// <returns></returns>
        public async Task<ConnectionTypeEnum> Type()
        {
            return await jsRuntime.InvokeAsync<string>("navigator.network.type") switch
            {
                "bluetooth" => ConnectionTypeEnum.Bluetooth,
                "cellular" => ConnectionTypeEnum.Cellular,
                "ethernet" => ConnectionTypeEnum.Ethernet,
                "none" => ConnectionTypeEnum.None,
                "wifi" => ConnectionTypeEnum.Wifi,
                "wimax" => ConnectionTypeEnum.Wimax,
                "other" => ConnectionTypeEnum.Other,
                "unknown" => ConnectionTypeEnum.Unknown,
                _ => ConnectionTypeEnum.Unknown
            };
        }

        public enum EffectiveTypeEnum
        {
            Unknown,
            Slow2G,
            _2G,
            _3G,
            _4G
        }

        public enum ConnectionTypeEnum
        {
            Bluetooth,
            Cellular,
            Ethernet,
            None,
            Wifi,
            Wimax,
            Other,
            Unknown
        }
    }
}