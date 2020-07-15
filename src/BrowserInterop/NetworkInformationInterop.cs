using Microsoft.JSInterop;

using System;
using System.Threading.Tasks;

namespace BrowserInterop
{
    public class NetworkInformationInterop
    {
        private IJSRuntime jSRuntime;
        private JsRuntimeObjectRef windowObject;

        public NetworkInformationInterop()
        {
        }

        internal void SetJsRuntime(IJSRuntime jSRuntime, JsRuntimeObjectRef windowObject)
        {
            this.jSRuntime = jSRuntime;
            this.windowObject = windowObject;
        }

        /// <summary>
        /// Returns the effective bandwidth estimate in megabits per second, rounded to the nearest multiple of 25 kilobits per seconds.
        /// </summary>
        /// <returns></returns>
        public double Downlink { get; set; }

        /// <summary>
        /// Returns the maximum downlink speed, in megabits per second (Mbps), for the underlying connection technology.
        /// </summary>
        /// <returns></returns>
        public double DownlinkMax { get; set; }


        public string EffectiveType { get; set; }

        /// <summary>
        /// Returns the maximum downlink speed, in megabits per second (Mbps), for the underlying connection technology.
        /// </summary>
        /// <returns></returns>
        public EffectiveTypeEnum EffectiveTypeEnum
        {
            get
            {
                return EffectiveType switch
                {
                    "slow-2g" => EffectiveTypeEnum.Slow2G,
                    "2g" => EffectiveTypeEnum._2G,
                    "3g" => EffectiveTypeEnum._3G,
                    "4g" => EffectiveTypeEnum._4G,
                    _ => EffectiveTypeEnum.Unknown,
                };
            }
        }

        /// <summary>
        /// Returns the estimated effective round-trip time of the current connection, rounded to the nearest multiple of 25 milliseconds
        /// </summary>
        /// <returns></returns>
        public double Rtt { get; set; }
        /// <summary>
        /// Returns true if the user has set a reduced data usage option on the user agent.
        /// </summary>
        /// <returns></returns>
        public bool SaveData { get; set; }


        public string Type { get; set; }

        /// <summary>
        /// Returns true if the user has set a reduced data usage option on the user agent.
        /// </summary>
        /// <returns></returns>
        public ConnectionTypeEnum TypeEnum
        {
            get
            {
                return Type switch
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
        }

        /// <summary>
        /// toDo will be called when the network informations changes
        /// </summary>
        /// <param name="toDo">Action to call</param>
        /// <returns></returns> 
        public async ValueTask<IAsyncDisposable> OnChange(Func<ValueTask> toDo)
        {
            return await jSRuntime.AddEventListener(windowObject, "connection", "change", CallBackInteropWrapper.Create(toDo));
        }
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