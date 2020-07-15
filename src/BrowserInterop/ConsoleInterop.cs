
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
    internal class ActionAsyncDisposable : IAsyncDisposable
    {
        private readonly Func<ValueTask> todoOnDispose;
        public ActionAsyncDisposable(Func<ValueTask> todoOnDispose)
        {
            this.todoOnDispose = todoOnDispose;
        }
        public async ValueTask DisposeAsync()
        {
            await todoOnDispose();
        }
    }
}