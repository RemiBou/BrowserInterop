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
        private readonly Func<ValueTask> toDo;

        internal JSInteropActionWrapper(Func<ValueTask> toDo)
        {
            this.toDo = toDo;
        }


        [JSInvokable]
        public async ValueTask Invoke()
        {
            await toDo.Invoke();
        }
    }

    /// <summary>
    /// Wrap a c# action into an object ibvokable by JS
    /// </summary>
    public class JSInteropActionWrapper<T>
    {
        private readonly Func<T, ValueTask> toDo;

        internal JSInteropActionWrapper(Func<T, ValueTask> toDo)
        {
            this.toDo = toDo;
        }


        [JSInvokable]
        public async ValueTask Invoke(T arg1)
        {
            await toDo.Invoke(arg1);
        }
    }
}