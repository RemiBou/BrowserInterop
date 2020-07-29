using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BrowserInterop.Extensions
{
    /// <summary>
    /// Wrap a c# action into an object invokable by JS
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
            await toDo.Invoke().ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Wrap a c# action into an object invokable by JS
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
            await toDo.Invoke(arg1).ConfigureAwait(false);
        }
    }
    
    /// <summary>
    /// Wrap a c# action into an object invokable by JS
    /// </summary>
    public class JsInteropActionWrapperWithResult<T, TResult>
    {
        private readonly Func<T, ValueTask<TResult>> toDo;

        internal JsInteropActionWrapperWithResult(Func<T, ValueTask<TResult>> toDo)
        {
            this.toDo = toDo;
        }


        [JSInvokable]
        public async Task<TResult> Invoke(T arg1)
        {
            return await toDo.Invoke(arg1).ConfigureAwait(false);
        }
    }
}