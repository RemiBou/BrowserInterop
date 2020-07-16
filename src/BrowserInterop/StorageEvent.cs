using BrowserInterop.Storage;

namespace BrowserInterop
{
    public class StorageEvent
    {
        public string Key { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }
#pragma warning disable CA1056 // Les propriétés Uri ne doivent pas être des chaînes
        public string Url { get; set; }
#pragma warning restore CA1056 // Les propriétés Uri ne doivent pas être des chaînes
        public WindowStorage Storage { get; set; }
    }
}