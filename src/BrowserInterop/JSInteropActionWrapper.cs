using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BrowserInterop
{
    /// <summary>
    /// Wrap a c# action into an object ibvokable by JS
    /// </summary>
    public class JSInteropActionWrapper
    {
        private readonly Func<Task> toDo;

        internal JSInteropActionWrapper(Func<Task> toDo)
        {
            this.toDo = toDo;
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
    public class JSInteropActionWrapper<T>
    {
        private readonly Func<T, Task> toDo;

        internal JSInteropActionWrapper(Func<T, Task> toDo)
        {
            this.toDo = toDo;
        }


        [JSInvokable]
        public async Task Invoke(T arg1)
        {
            await toDo.Invoke(arg1);
        }
    }
}