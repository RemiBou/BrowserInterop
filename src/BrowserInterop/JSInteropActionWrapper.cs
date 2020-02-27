using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BrowserInterop
{
    /// <summary>
    /// Wrap a c# action into an object ibvokable by JS
    /// </summary>
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
            //TODO : fix bug on unregister
            await jSRuntime.InvokeVoidAsync("browserInterop.removeEventListener", "navigator.connection", "change", listernerId);
        }

        [JSInvokable]
        public async Task Invoke()
        {
            await toDo.Invoke();
        }
    }

    /// <summary>
    /// Wrap a c# action into an object ibvokable by JS
    /// </summary>
    internal class JSInteropActionWrapper<T> : IAsyncDisposable
    {
        private int listernerId;
        private readonly IJSRuntime jSRuntime;
        private readonly Func<T, Task> toDo;

        internal JSInteropActionWrapper(IJSRuntime jSRuntime, Func<T, Task> toDo)
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
            //TODO : fix bug on unregister
            await jSRuntime.InvokeVoidAsync("browserInterop.removeEventListener", "navigator.connection", "change", listernerId);
        }

        [JSInvokable]
        public async Task Invoke(T arg1)
        {
            await toDo.Invoke(arg1);
        }
    }
}