namespace RemiBou.Blazor.BrowserInterop.Storage
{
    /// <summary>
    /// breakdown of usage by storage system.  All included members will have a usage greater than 0 and any storage system with 0 usage will be excluded from the dictionary.  
    /// </summary>
    public class StorageEstimateUsageDetails
    {
        public long ApplicationCache { get; set; }
        public long IndexedDB { get; set; }
        public long Caches { get; set; }
        public long ServiceWorkerRegistrations { get; set; }
        public long FileSystem { get; set; }
    }
}