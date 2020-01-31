using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BrowserInterop
{
    internal class JSInteropActionWrapper : IAsyncDisposable
    {
        private int listernerId;
        private readonly IJSRuntime jSRuntime;
        private readonly Func<Task> toDo;

        internal JSInteropActionWrapper(IJSRuntime jSRuntime, Func<Task> toDo)
        {
            this.jSRuntime = jSRuntime;
            this.toDo = toDo;
        }

        internal void SeListenerId(int listenerId)
        {
            this.listernerId = listenerId;
        }


        public async ValueTask DisposeAsync()
        {
            await jSRuntime.InvokeVoidAsync("browserInterop.removeEventListener", "navigator.connection", "change", listernerId);
        }

        [JSInvokable]
        public async Task Invoke()
        {
            await toDo.Invoke();
        }
    }
}