using Microsoft.JSInterop;

using System;
using System.Threading.Tasks;

namespace BrowserInterop
{
    /// <summary>
    /// Wrap a c# action into an object ibvokable by JS
    /// </summary>
    public class JsInteropActionWrapper
    {
        private readonly Func<ValueTask> toDo;

        internal JsInteropActionWrapper(Func<ValueTask> toDo)
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
    public class JsInteropActionWrapper<T>
    {
        private readonly Func<T, ValueTask> toDo;

        internal JsInteropActionWrapper(Func<T, ValueTask> toDo)
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