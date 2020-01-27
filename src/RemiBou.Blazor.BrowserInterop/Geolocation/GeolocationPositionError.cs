using System;

namespace RemiBou.Blazor.BrowserInterop.Geolocation
{
    public class GeolocationPositionError
    {
        /// <summary>
        /// The error code
        /// </summary>
        /// <value></value>
        public int Code { get; set; }

        /// <summary>
        /// Easy access to error code
        /// </summary>
        /// <value></value>
        public GeolocationPositionErrorEnum CodeEnum
        {
            get => Code switch
            {
                1 => GeolocationPositionErrorEnum.PermissionDenied,
                2 => GeolocationPositionErrorEnum.PositionUnavailable,
                3 => GeolocationPositionErrorEnum.Timeout,
                _ => throw new ArgumentOutOfRangeException($"GeolocationPositionError.Code:{Code}")
            };
        }

        /// <summary>
        ///  the details of the error. Specifications note that this is primarily intended for debugging use and not to be shown directly in a user interface.
        /// </summary>
        /// <value></value>
        public string Message { get; set; }

        public enum GeolocationPositionErrorEnum
        {
            PermissionDenied,
            Timeout,
            PositionUnavailable
        }
    }
}