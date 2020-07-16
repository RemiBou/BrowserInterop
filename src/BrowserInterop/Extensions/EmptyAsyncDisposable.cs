
using System;
using System.Threading.Tasks;

namespace BrowserInterop.Extensions
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