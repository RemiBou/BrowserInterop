using Microsoft.JSInterop;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace RemiBou.Blazor.BrowserProfiling
{
    public static class BlazorProfilingJsRuntimeExtension
    {
        private static int CallCount= 0;
        public static async Task<IAsyncDisposable> ProfileStep(this IJSRuntime jsRuntime, string stepName)
        {
            var stepId = Interlocked.Increment(ref CallCount);
            stepName = stepName+"#"+stepId;
            await jsRuntime.InvokeAsync<string>("console.time", stepName);
            return new BlazorProfilingStep(jsRuntime, stepName);

        }
        public static IDisposable ProfileStepSync(this IJSRuntime jsRuntime, string stepName)
        {
            var stepId = Interlocked.Increment(ref CallCount);
            stepName = stepName+"#"+stepId;
            Task.Run(() => jsRuntime.InvokeAsync<string>("console.time", stepName));
            return new BlazorProfilingStep(jsRuntime, stepName);

        }

        private class BlazorProfilingStep : IAsyncDisposable, IDisposable
        {
            private string stepName;
            private IJSRuntime jsRuntime;
            public BlazorProfilingStep(IJSRuntime jsRuntime, string stepName)
            {
                this.stepName = stepName;
                this.jsRuntime = jsRuntime;
            }
            public async ValueTask DisposeAsync ()
            {
                await jsRuntime.InvokeAsync<string>("console.timeEnd", stepName);

            }
            public void Dispose ()
            {
                Task.Run(() => jsRuntime.InvokeAsync<string>("console.timeEnd", stepName));

            }
            
        }
    }
}
