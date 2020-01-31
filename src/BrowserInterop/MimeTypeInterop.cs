namespace BrowserInterop
{
    public class MimeTypeInterop
    {
        public string Type { get; set; }
        public string Suffix { get; set; }
        public string Description { get; set; }

        public PluginInterop Plugin { get; set; }
    }

    public class PluginInterop
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
    }
}