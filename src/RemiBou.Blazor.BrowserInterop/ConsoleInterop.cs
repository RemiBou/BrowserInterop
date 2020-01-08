using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace RemiBou.Blazor.BrowserInterop
{
    /// <summary>
    /// Give access to window.console API https://developer.mozilla.org/en-US/docs/Web/API/Console/
    /// </summary>
    public class ConsoleInterop
    {
        private IJSRuntime jsRuntime;

        internal ConsoleInterop(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Will print an error with the given objectsin the output if the assertion is not verified
        /// </summary>
        /// <param name="assertion">true or false</param>
        /// <param name="printedObjects">object to print in the console</param>
        /// <returns></returns>
        public async Task Assert(bool assertion, params object[] printedObjects)
        {
            await jsRuntime.InvokeVoidAsync("console.assert", assertion, printedObjects);
        }

        /// <summary>
        /// Will print an error with the given message in the output if the assertion is not verified
        /// Note : in the browser API, the format is a C format but here it seems better to use .net string.Format
        /// </summary>
        /// <param name="assertion">true or false</param>
        /// <param name="message">message format</param>
        /// <param name="formatParameters">string.Format parameters</param>
        /// <returns></returns>
        public async Task Assert(bool assertion, string message, params object[] formatParameters)
        {
            await jsRuntime.InvokeVoidAsync("console.assert", assertion, string.Format(message, formatParameters));

        }

        /// <summary>
        /// Clear the console
        /// </summary>
        /// <returns></returns>
        public async Task Clear()
        {
            await jsRuntime.InvokeVoidAsync("console.clear");
        }

        /// <summary>
        /// Logs the number of times that this particular call to Count() has been called
        /// </summary>
        /// <param name="label">If supplied, Count() outputs the number of times it has been called with that label. If omitted, Count() behaves as though it was called with the "default" label.</param>
        /// <returns></returns>
        public async Task Count(string label = null)
        {
            await jsRuntime.InvokeVoidAsync("console.count", label ?? "default");
        }

        /// <summary>
        /// Reset a counter
        /// </summary>
        /// <param name="label">If supplied,resets this label counter else, resets the default label.</param>
        /// <returns></returns>
        public async Task CountReset(string label = null)
        {
            await jsRuntime.InvokeVoidAsync("console.countReset", label ?? "default");
        }

        /// <summary>
        /// Will print the given object at the debug level in the console
        /// </summary>
        /// <param name="printedObjects">object to print in the console</param>
        /// <returns></returns>
        public async Task Debug(params object[] printedObjects)
        {
            await jsRuntime.InvokeVoidAsync("console.debug", printedObjects);
        }

        /// <summary>
        /// Will print the given message at the debug level in the console
        /// Note : in the browser API, the format is a C format but here it seems better to use .net string.Format
        /// </summary>$
        /// <param name="message">message format</param>
        /// <param name="formatParameters">string.Format parameters</param>
        /// <returns></returns>
        public async Task Debug(string message, params object[] formatParameters)
        {
            await jsRuntime.InvokeVoidAsync("console.debug", string.Format(message, formatParameters));

        }

        /// <summary>
        /// Display the object in the console as an interactive list
        /// </summary>
        /// <param name="data">the object to display</param>
        /// <returns></returns>
        public async Task Dir(object data)
        {
            await jsRuntime.InvokeVoidAsync("console.dir", data);
        }

        /// <summary>
        /// Display the HTML element as interactive in the console
        /// </summary>
        /// <param name="data">the HTML element ref to display</param>
        /// <returns></returns>
        public async Task DirXml(ElementReference element)
        {
            await jsRuntime.InvokeVoidAsync("console.dirxml", element);
        }

        /// <summary>
        /// Will print the given object at the error level in the console
        /// </summary>
        /// <param name="printedObjects">object to print in the console</param>
        /// <returns></returns>
        public async Task Error(params object[] printedObjects)
        {
            await jsRuntime.InvokeVoidAsync("console.error", printedObjects);
        }

        /// <summary>
        /// Will print the given message at the error level in the console
        /// Note : in the browser API, the format is a C format but here it seems better to use .net string.Format
        /// </summary>$
        /// <param name="message">message format</param>
        /// <param name="formatParameters">string.Format parameters</param>
        /// <returns></returns>
        public async Task Error(string message, params object[] formatParameters)
        {
            await jsRuntime.InvokeVoidAsync("console.error", string.Format(message, formatParameters));
        }

        /// <summary>
        /// Creates a new group for the next logs in the console
        /// </summary>
        /// <param name="label">The group label</param>
        /// <returns></returns>
        public async Task GroupStart(string label)
        {
            await jsRuntime.InvokeVoidAsync("console.group", label);
        }

        /// <summary>
        /// End the current console group
        /// </summary>
        /// <returns></returns>
        public async Task GroupEnd()
        {
            await jsRuntime.InvokeVoidAsync("console.groupEnd");
        }

        /// <summary>
        /// Conveniant method for creating console group with the using syntax : the group will be closed when Dispose is called
        /// </summary>
        /// <param name="label">group label</param>
        /// <returns></returns>
        public async Task<IAsyncDisposable> Group(string label)
        {
            await GroupStart(label);
            return new ActionAsyncDisposable(() => GroupEnd());
        }

        /// <summary>
        /// Will print the given object at the log level in the console
        /// </summary>
        /// <param name="printedObjects">object to print in the console</param>
        /// <returns></returns>
        public async Task Log(params object[] printedObjects)
        {
            await jsRuntime.InvokeVoidAsync("console.log", printedObjects);
        }

        /// <summary>
        /// Will print the given message at the log level in the console
        /// Note : in the browser API, the format is a C format but here it seems better to use .net string.Format
        /// </summary>$
        /// <param name="message">message format</param>
        /// <param name="formatParameters">string.Format parameters</param>
        /// <returns></returns>
        public async Task Log(string message, params object[] formatParameters)
        {
            await jsRuntime.InvokeVoidAsync("console.log", string.Format(message, formatParameters));
        }

        /// <summary>
        /// Start a profiling session with the given name
        /// </summary>
        /// <param name="name">The profiling session name</param>
        /// <returns></returns>
        public async Task ProfileStart(string name = null)
        {
            await jsRuntime.InvokeVoidAsync("console.profile", name);
        }

        /// <summary>
        /// End the profiling session withe the given name
        /// </summary>
        /// <returns></returns>
        public async Task ProfileEnd(string name = null)
        {
            await jsRuntime.InvokeVoidAsync("console.profileEnd", name);
        }

        /// <summary>
        /// Conveniant method for profiling with the using syntax :
        ///  the profiling session will be closed when Dispose is called.
        /// You can see a profiling session by going in dev tool => 3 dots menu => more tools => Javascript Profiler
        /// </summary>
        /// <param name="label">group label</param>
        /// <returns></returns>
        public async Task<IAsyncDisposable> Profile(string name = null)
        {
            await ProfileStart(name);
            return new ActionAsyncDisposable(() => ProfileEnd(name));
        }

        /// <summary>
        /// DIsplay object list as a table
        /// </summary>
        /// <param name="objectToDisplay">Objects to display</param>
        /// <param name="columns">Columns to display, if no parameters then all the columns are displayed</param>
        /// <returns></returns>
        public async Task Tab<T>(IEnumerable<T> objectToDisplay, params string[] columns)
        {
            await jsRuntime.InvokeVoidAsync("console.table", objectToDisplay, columns);
        }

        private class ActionAsyncDisposable : IAsyncDisposable
        {
            private Func<Task> todoOnDispose;
            public ActionAsyncDisposable(Func<Task> todoOnDispose)
            {
                this.todoOnDispose = todoOnDispose;
            }
            public async ValueTask DisposeAsync()
            {
                await todoOnDispose();
            }
        }
    }
}