namespace BrowserInterop
{

    public partial class WindowInterop
    {
        private class PopStateEvent<T>
        {
            public T State { get; set; }
        }
    }
}
