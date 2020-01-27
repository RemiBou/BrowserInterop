namespace RemiBou.Blazor.BrowserInterop.Geolocation
{
    /// <summary>
    /// Resultof a GetCurrentPosition call
    /// </summary>
    public class GeolocationResult
    {
        /// <summary>
        /// Current user location from his browser
        /// </summary>
        /// <value></value>
        public GeolocationPosition Location { get; set; }

        /// <summary>
        /// Error received when getting the current location
        /// </summary>
        /// <value></value>
        public GeolocationPositionError Error { get; set; }
    }
}