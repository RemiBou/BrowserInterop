using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BrowserInterop
{
    public class HistoryInterop
    {
        private IJSRuntime jsRuntime;
        private JsRuntimeObjectRef jsRuntimeObjectRef;

        /// <summary>
        /// Represents the number of elements in the session history, including the currently loaded page. For example, for a page loaded in a new tab this property returns 1.
        /// </summary>
        /// <value></value>
        public int Length { get; set; }

        internal void SetJSRuntime(IJSRuntime jsRuntime, JsRuntimeObjectRef jsRuntimeObjectRef)
        {
            this.jsRuntime = jsRuntime;
            this.jsRuntimeObjectRef = jsRuntimeObjectRef;
        }

        /// <summary>
        ///  allows web applications to explicitly set default scroll restoration behavior on history navigation.
        /// </summary>
        /// <value></value>
        public string ScrollRestoration { get; set; }

        /// <summary>
        /// Enum accessor for history.scrollRestoration
        /// </summary>
        /// <value></value>
        public ScrollRestorationEnum ScrollRestorationEnum
        {
            get
            {
                return Enum.Parse<ScrollRestorationEnum>(ScrollRestoration, true);
            }
        }

        /// <summary>
        /// Set the current value of history.scrollRestoration
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task SetScrollRestoration(ScrollRestorationEnum value)
        {
            await jsRuntime.SetInstancePropertyAsync(jsRuntimeObjectRef, "scrollRestoration", value.ToString().ToLower());
        }

        /// <summary>
        /// Represents the state at the top of the history stack. This is a way to look at the state without having to wait for a popstate event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> State<T>()
        {
            return await jsRuntime.GetInstancePropertyAsync<T>(jsRuntimeObjectRef, "state");
        }

        /// <summary>
        /// causes the browser to move back one page in the session history. It has the same effect as calling history.go(-1). If there is no previous page, this method call does nothing.
        /// </summary>
        /// <returns></returns>
        public async Task Back()
        {
            await jsRuntime.InvokeInstanceMethodAsync(jsRuntimeObjectRef, "back");
        }

        /// <summary>
        /// causes the browser to move forward one page in the session history. It has the same effect as calling history.go(1).
        /// </summary>
        /// <returns></returns>
        public async Task Forward()
        {
            await jsRuntime.InvokeInstanceMethodAsync(jsRuntimeObjectRef, "forward");
        }

        /// <summary>
        /// loads a specific page from the session history. You can use it to move forwards and backwards through the history depending on the value of the delta parameter.
        /// /// </summary>
        /// <param name="delta">The position in the history to which you want to move, relative to the current page. A negative value moves backwards, a positive value moves forwards. So, for example, history.go(2) moves forward two pages and history.go(-2) moves back two pages. If no value is passed or if delta equals 0, it has the same result as calling location.reload().</param>
        /// <returns></returns>
        public async Task Go(int delta = 0)
        {
            await jsRuntime.InvokeInstanceMethodAsync(jsRuntimeObjectRef, "go", delta);
        }

        /// <summary>
        ///  adds a state to the browser's session history stack.
        /// </summary>
        /// <param name="state">The state object is a JavaScript object which is associated with the new history entry created by pushState(). Whenever the user navigates to the new state, a popstate event is fired, and the state property of the event contains a copy of the history entry's state object.
        /// The state object can be anything that can be serialized.Because Firefox saves state objects to the user's disk so they can be restored after the user restarts the browser, we impose a size limit of 640k characters on the serialized representation of a state object. If you pass a state object whose serialized representation is larger than this to pushState(), the method will throw an exception. If you need more space than this, you're encouraged to use sessionStorage and/or localStorage.
        /// </param>
        /// <param name="title">Most browsers currently ignores this parameter, although they may use it in the future. Passing the empty string here should be safe against future changes to the method. Alternatively, you could pass a short title for the state to which you're moving.</param>
        /// <param name="url">The new history entry's URL is given by this parameter. Note that the browser won't attempt to load this URL after a call to pushState(), but it might attempt to load the URL later, for instance after the user restarts the browser. The new URL does not need to be absolute; if it's relative, it's resolved relative to the current URL. The new URL must be of the same origin as the current URL; otherwise, pushState() will throw an exception. If this parameter isn't specified, it's set to the document's current URL.</param>
        /// <returns></returns>
        public async Task PushState(object state, string title, Uri url = null)
        {
            await jsRuntime.InvokeInstanceMethodAsync(jsRuntimeObjectRef, "pushState", state, title, url?.ToString());
        }

        /// <summary>
        ///  adds a state to the browser's session history stack.
        /// </summary>
        /// <param name="state">The state object is a JavaScript object which is associated with the history entry passed to the replaceState method. The state object can be null. </param>
        /// <param name="title">Most browsers currently ignores this parameter, although they may use it in the future. Passing the empty string here should be safe against future changes to the method. Alternatively, you could pass a short title for the state to which you're moving.</param>
        /// <param name="url">The URL of the history entry. The new URL must be of the same origin as the current URL; otherwise replaceState throws an exception.</param>
        /// <returns></returns>
        public async Task ReplaceState(object state, string title, Uri url = null)
        {
            await jsRuntime.InvokeInstanceMethodAsync(jsRuntimeObjectRef, "history.replaceState", state, title, url.ToString());
        }
    }
    public enum ScrollRestorationEnum
    {
        ///<summary>The location on the page to which the user has scrolled will be restored.</summary>
        Auto,
        ///<summary>The location on the page is not restored. The user will have to scroll to the location manually.</summary>
        Manual
    }
}