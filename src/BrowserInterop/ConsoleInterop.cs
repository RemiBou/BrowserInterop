using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrowserInterop
{
    /// <summary>
    /// Give access to window.console API https://developer.mozilla.org/en-US/docs/Web/API/Console/
    /// </summary>
    public class ConsoleInterop
    {
        /// <summary>
        /// Set to false if you want to disable all the calls to a console function
        /// </summary>
        public static bool IsEnabled = true;
        private readonly IJSRuntime jsRuntime;
        private readonly JsRuntimeObjectRef windowObject;

        internal ConsoleInterop(IJSRuntime jsRuntime, JsRuntimeObjectRef windowObject)
        {
            this.jsRuntime = jsRuntime;
            this.windowObject = windowObject;
        }

        /// <summary>
        /// Will print an error with the given objectsin the output if the assertion is not verified
        /// </summary>
        /// <param name="assertion">true or false</param>
        /// <param name="printedObjects">object to print in the console</param>
        /// <returns></returns>
        public async ValueTask Assert(bool assertion, params object[] printedObjects)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "browserInterop.callInstanceMethod", windowObject, "console.assert", assertion, printedObjects);
        }

        /// <summary>
        /// Will print an error with the given message in the output if the assertion is not verified
        /// Note : in the browser API, the format is a C format but here it seems better to use .net string.Format
        /// </summary>
        /// <param name="assertion">true or false</param>
        /// <param name="message">message format</param>
        /// <param name="formatParameters">string.Format parameters</param>
        /// <returns></returns>
        public async ValueTask Assert(bool assertion, string message, params object[] formatParameters)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.assert", assertion, string.Format(message, formatParameters));

        }

        /// <summary>
        /// Clear the console
        /// </summary>
        /// <returns></returns>
        public async ValueTask Clear()
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.clear");
        }

        /// <summary>
        /// Logs the number of times that this particular call to Count() has been called
        /// </summary>
        /// <param name="label">If supplied, Count() outputs the number of times it has been called with that label. If omitted, Count() behaves as though it was called with the "default" label.</param>
        /// <returns></returns>
        public async ValueTask Count(string label = null)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.count", label ?? "default");
        }

        /// <summary>
        /// Reset a counter
        /// </summary>
        /// <param name="label">If supplied,resets this label counter else, resets the default label.</param>
        /// <returns></returns>
        public async ValueTask CountReset(string label = null)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.countReset", label ?? "default");
        }

        /// <summary>
        /// Will print the given object at the debug level in the console
        /// </summary>
        /// <param name="printedObjects">object to print in the console</param>
        /// <returns></returns>
        public async ValueTask Debug(params object[] printedObjects)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.debug", printedObjects);
        }

        /// <summary>
        /// Will print the given message at the debug level in the console
        /// Note : in the browser API, the format is a C format but here it seems better to use .net string.Format
        /// </summary>$
        /// <param name="message">message format</param>
        /// <param name="formatParameters">string.Format parameters</param>
        /// <returns></returns>
        public async ValueTask Debug(string message, params object[] formatParameters)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.debug", string.Format(message, formatParameters));

        }

        /// <summary>
        /// Display the object in the console as an interactive list
        /// </summary>
        /// <param name="data">the object to display</param>
        /// <returns></returns>
        public async ValueTask Dir(object data)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.dir", data);
        }

        /// <summary>
        /// Display the HTML element as interactive in the console
        /// </summary>
        /// <param name="data">the HTML element ref to display</param>
        /// <returns></returns>
        public async ValueTask DirXml(ElementReference element)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.dirxml", element);
        }

        /// <summary>
        /// Will print the given object at the error level in the console
        /// </summary>
        /// <param name="printedObjects">object to print in the console</param>
        /// <returns></returns>
        public async ValueTask Error(params object[] printedObjects)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.error", printedObjects);
        }

        /// <summary>
        /// Will print the given message at the error level in the console
        /// Note : in the browser API, the format is a C format but here it seems better to use .net string.Format
        /// </summary>$
        /// <param name="message">message format</param>
        /// <param name="formatParameters">string.Format parameters</param>
        /// <returns></returns>
        public async ValueTask Error(string message, params object[] formatParameters)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.error", string.Format(message, formatParameters));
        }

        /// <summary>
        /// Creates a new group for the next logs in the console
        /// </summary>
        /// <param name="label">The group label</param>
        /// <returns></returns>
        public async ValueTask GroupStart(string label)
        {
            if (IsEnabled)
            {
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.group", label);
            }
        }


        /// <summary>
        /// End the current console group
        /// </summary>
        /// <returns></returns>
        public async ValueTask GroupEnd()
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.groupEnd");
        }

        /// <summary>
        /// Conveniant method for creating console group with the using syntax : the group will be closed when Dispose is called
        /// </summary>
        /// <param name="label">group label</param>
        /// <returns></returns>
        public async ValueTask<IAsyncDisposable> Group(string label)
        {

            if (!IsEnabled) return EmptyAsyncDisposable.Instance;
            await GroupStart(label);
            return new ActionAsyncDisposable(() => GroupEnd());
        }

        /// <summary>
        /// Will print the given object at the log level in the console
        /// </summary>
        /// <param name="printedObjects">object to print in the console</param>
        /// <returns></returns>
        public async ValueTask Log(params object[] printedObjects)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.log", printedObjects);
        }

        /// <summary>
        /// Will print the given message at the log level in the console
        /// Note : in the browser API, the format is a C format but here it seems better to use .net string.Format
        /// </summary>$
        /// <param name="message">message format</param>
        /// <param name="formatParameters">string.Format parameters</param>
        /// <returns></returns>
        public async ValueTask Log(string message, params object[] formatParameters)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.log", string.Format(message, formatParameters));
        }

        /// <summary>
        /// Start a profiling session with the given name
        /// </summary>
        /// <param name="name">The profiling session name</param>
        /// <returns></returns>
        public async ValueTask ProfileStart(string name = null)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.profile", name);
        }

        /// <summary>
        /// End the profiling session withe the given name
        /// </summary>
        /// <returns></returns>
        public async ValueTask ProfileEnd(string name = null)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.profileEnd", name);
        }

        /// <summary>
        /// Conveniant method for profiling with the using syntax :
        ///  the profiling session will be closed when Dispose is called.
        /// You can see a profiling session by going in dev tool => 3 dots menu => more tools => Javascript Profiler
        /// </summary>
        /// <param name="label">profiler name</param>
        /// <returns></returns>
        public async ValueTask<IAsyncDisposable> Profile(string name = null)
        {
            if (!IsEnabled) return EmptyAsyncDisposable.Instance;
            await ProfileStart(name);
            return new ActionAsyncDisposable(() => ProfileEnd(name));
        }

        /// <summary>
        /// DIsplay object list as a table
        /// </summary>
        /// <param name="objectToDisplay">Objects to display</param>
        /// <param name="columns">Columns to display, if no parameters then all the columns are displayed</param>
        /// <returns></returns>
        public async ValueTask Table<T>(IEnumerable<T> objectToDisplay, params string[] columns)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.table", objectToDisplay, columns);
        }

        /// <summary>
        /// Start a timer, it's label and execution time will be shown on the profiling session (only the ones in the Performance tab, not the ones started with the Profile lethod) and on the console
        /// </summary>
        /// <param name="name">The label displayed</param>
        /// <returns></returns>
        public async ValueTask TimeStart(string label)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.time", label);
        }

        /// <summary>
        /// End a timer, it's label and execution time will be shown on the profiling (not the one started) session and on the console
        /// </summary>
        /// <param name="name">The label displayed</param>
        /// <returns></returns>
        public async ValueTask TimeEnd(string label)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.timeEnd", label);
        }

        /// <summary>
        /// Conveniant method for starting and ending a timer around a piece of code with the using syntax :
        ///  the timer session will be ended when Dispose is called.
        /// </summary>
        /// <param name="label">The label displayed</param>
        /// <returns></returns>
        public async ValueTask<IAsyncDisposable> Time(string label)
        {
            if (!IsEnabled) return EmptyAsyncDisposable.Instance;
            await TimeStart(label);
            return new ActionAsyncDisposable(() => TimeEnd(label));
        }

        /// <summary>
        /// Display the given timer time in the console, it must be done when the timer is running
        /// </summary>
        /// <param name="name">The label displayed</param>
        /// <returns></returns>
        public async ValueTask TimeLog(string label)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.timeLog", label);
        }

        /// <summary>
        /// Display a marker on the profiling sessions
        /// </summary>
        /// <param name="name">The label displayed</param>
        /// <returns></returns>
        public async ValueTask TimeStamp(string label)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.timeStamp", label);
        }

        /// <summary>
        /// Will print the current browser stack trace (not the .net runtime stacktrace, for this use Environment.StackTrace) with this objects 
        /// </summary>
        /// <param name="printedObjects">objects to print in the console</param>
        /// <returns></returns>
        public async ValueTask Trace(params object[] printedObjects)
        {
            if (IsEnabled)
                await jsRuntime.InvokeInstanceMethodAsync(windowObject, "console.trace", printedObjects);
        }

    }
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