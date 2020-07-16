using System;
using System.Threading.Tasks;

namespace BrowserInterop.Extensions
{
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