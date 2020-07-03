namespace BrowserInterop
{
    public class StorageEvent
    {
        public string Key { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }
        public string Url { get; set; }
        public StorageInterop Storage { get; set; }
    }
}