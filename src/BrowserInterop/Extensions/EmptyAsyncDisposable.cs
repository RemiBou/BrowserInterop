
using System;
using System.Threading.Tasks;

namespace BrowserInterop
{
    internal class EmptyAsyncDisposable : IAsyncDisposable
    {
        internal static EmptyAsyncDisposable Instance = new EmptyAsyncDisposable();
        internal EmptyAsyncDisposable()
        {
        }
        public ValueTask DisposeAsync()
        {
            return new ValueTask();
        }
    }
}