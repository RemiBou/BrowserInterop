using System;
using System.Threading.Tasks;

namespace BrowserInterop.Extensions
{
    internal class EmptyAsyncDisposable : IAsyncDisposable
    {
        internal static readonly EmptyAsyncDisposable Instance = new EmptyAsyncDisposable();

        public ValueTask DisposeAsync()
        {
            return new ValueTask();
        }
    }
}