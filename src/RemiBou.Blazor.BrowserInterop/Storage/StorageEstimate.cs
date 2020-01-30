namespace RemiBou.Blazor.BrowserInterop.Storage
{
    /// <summary>
    /// Provide estimates of the size of a site's or application's data store and how much of it is in use.
    /// </summary>
    public class StorageEstimate
    {
        /// <summary>
        /// Provides a conservative approximation of the total storage the user's device or computer has available for the site origin or Web app. It's possible that there's more than this amount of space available though you can't rely on that being the case.
        /// </summary>
        /// <value></value>
        public decimal Quota { get; set; }

        /// <summary>
        /// A numeric value approximating the amount of storage space currently being used by the site or Web app, out of the available space as indicated by quota.
        /// </summary>
        /// <value></value>
        public decimal Usage { get; set; }

        /// <summary>
        /// breakdown of usage by storage system.  All included members will have a usage greater than 0 and any storage system with 0 usage will be excluded from the dictionary.  
        /// </summary>
        /// <value></value>
        public StorageEstimateUsageDetails UsageDetails { get; set; }
    }
}