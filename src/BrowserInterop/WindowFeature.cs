using System.Collections.Generic;

namespace BrowserInterop
{
    public class WindowFeature
    {
        /// <summary>
        /// Specifies the distance the new window is placed from the left side of the work area for applications of the user's operating system to the leftmost border (resizing handle) of the browser window. The new window can not be initially positioned offscreen.
        /// </summary>
        /// <value></value>
        public int? Left { get; set; }

        /// <summary>
        /// Specifies the distance the new window is placed from the top side of the work area for applications of the user's operating system to the topmost border (resizing handle) of the browser window. The new window can not be initially positioned offscreen.
        /// </summary>
        /// <value></value>
        public int? Top { get; set; }

        /// <summary>
        /// Specifies the height of the content area, viewing area of the new secondary window in pixels. The height value includes the height of the horizontal scrollbar if present. The minimum required value is 100.
        /// </summary>
        /// <value></value>
        public int? Height { get; set; }

        /// <summary>
        /// Specifies the width of the content area, viewing area of the new secondary window in pixels. The width value includes the width of the vertical scrollbar if present. The width value does not include the sidebar if it is expanded. The minimum required value is 100.
        /// </summary>
        /// <value></value>
        public int? Width { get; set; }

        /// <summary>
        /// Centers the window in relation to its parent's size and position. Requires chrome=yes.
        /// </summary>
        /// <value></value>
        public bool? CenterScreen { get; set; }

        /// <summary>
        /// Specifies the height of the whole browser window in pixels. This outerHeight value includes any/all present toolbar, window horizontal scrollbar (if present) and top and bottom window resizing borders. Minimal required value is 100.
        /// </summary>
        /// <value></value>
        public int? OuterHeight { get; set; }

        /// <summary>
        /// Specifies the width of the whole browser window in pixels. This outerWidth value includes the window vertical scrollbar (if present) and left and right window resizing borders.
        /// </summary>
        /// <value></value>
        public int? OuterWidth { get; set; }

        /// <summary>
        /// Same as height but only supported by Netscape and Mozilla-based browsers. Specifies the height of the content area, viewing area of the new secondary window in pixels. The innerHeight value includes the height of the horizontal scrollbar if present. Minimal required value is 100.
        /// </summary>
        /// <value></value>
        public int? InnerHeight { get; set; }

        /// <summary>
        /// Same as width but only supported by Netscape and Mozilla-based browsers. Specifies the width of the content area, viewing area of the new secondary window in pixels. The innerWidth value includes the width of the vertical scrollbar if present. The innerWidth value does not include the sidebar if it is expanded. Minimal required value is 100.
        /// </summary>
        /// <value></value>
        public int? InnerWidth { get; set; }

        /// <summary>
        /// If this feature is on, then the new secondary window renders the menubar.
        /// </summary>
        /// <value></value>
        public bool? MenuBar { get; set; }

        /// <summary>
        /// If this feature is on, then the new secondary window renders the Navigation Toolbar (Back, Forward, Reload, Stop buttons). In addition to the Navigation Toolbar, Mozilla-based browsers will render the Tab Bar if it is visible, present in the parent window. (If this feature is set to no all toolbars in the window will be invisible, for example extension toolbars).
        /// </summary>
        /// <value></value>
        public bool? ToolBar { get; set; }

        /// <summary>
        /// If this feature is on, then the new secondary window renders the Location bar in Mozilla-based browsers. MSIE 5+ and Opera 7.x renders the Address Bar.
        /// </summary>
        /// <value></value>
        public bool? Location { get; set; }

        /// <summary>
        /// If this feature is on, then the new secondary window renders the Personal Toolbar in Netscape 6.x, Netscape 7.x and Mozilla browser. It renders the Bookmarks Toolbar in Firefox. In addition to the Personal Toolbar, Mozilla browser will render the Site Navigation Bar if such toolbar is visible, present in the parent window.
        /// </summary>
        /// <value></value>
        public bool? PersonalBar { get; set; }

        /// <summary>
        /// If this feature is on, then the new secondary window has a status bar. Users can force the rendering of status bar in all Mozilla-based browsers, in MSIE 6 SP2 (Note on status bar in XP SP2) and in Opera 6+. The default preference setting in recent Mozilla-based browser releases and in Firefox 1.0 is to force the presence of the status bar.
        /// </summary>
        /// <value></value>
        public bool? Status { get; set; }

        /// <summary>
        /// This setting can only apply to dialog windows; "minimizable" requires dialog=yes. If minimizable is on, the new dialog window will have a minimize system command icon in the titlebar and it will be minimizable. Any non-dialog window is always minimizable and minimizable=no will be ignored.
        /// </summary>
        /// <value></value>
        public bool? Minimizable { get; set; }

        /// <summary>
        /// If this feature is set, the newly-opened window will open as normal, except that it will not have access back to the originating window (via Window.opener — it returns null). In addition, the window.open() call will also return null, so the originating window will not have access to the new one either.  This is useful for preventing untrusted sites opened via window.open() from tampering with the originating window, and vice versa.
        /// </summary>
        /// <value></value>
        public bool? NoOpener { get; set; }

        /// <summary>
        /// If this feature is set, the request to load the content located at the specified URL will be loaded with the request's referrer set to noreferrer; this prevents the request from sending the URL of the page that initiated the request to the server where the request is sent. In addition, setting this feature also automatically sets noopener. See rel="noreferrer" for additional details and compatibility information. Firefox int?roduced support for noreferrer in Firefox 68.
        /// </summary>
        /// <value></value>
        public bool? NoReferrer { get; set; }

        /// <summary>
        /// If this feature is on, the new secondary window will be resizable.
        /// </summary>
        /// <value></value>
        public bool? Resizable { get; set; }

        /// <summary>
        /// If this feature is on, the new secondary window will show horizontal and/or vertical scrollbar(s) if the document doesn't fit int?o the window's viewport.
        /// </summary>
        /// <value></value>
        public bool? ScrollBars { get; set; }

        /// <summary>
        /// Note: Starting with Mozilla 1.7/Firefox 0.9, this feature requires the UniversalBrowserWrite privilege (bug 244965). Without this privilege, it is ignored.
        /// If on, the page is loaded as window's only content, without any of the browser's int?erface elements. There will be no context menu defined by default and none of the standard keyboard shortcuts will work.The page is supposed to provide a user int?erface of its own, usually this feature is used to open XUL documents(standard dialogs like the JavaScript Console are opened this way).
        /// </summary>
        /// <value></value>
        public bool? Chrome { get; set; }

        /// <summary>
        /// Note: Starting with Firefox 44, this feature can only be used with chrome privileges. If content attempts to toggle this feature, it will be ignored.
        /// The dialog feature removes all icons(restore, minimize, maximize) from the window's titlebar, leaving only the close button. Mozilla 1.2+ and Netscape 7.1 will render the other menu system commands (in FF 1.0 and in NS 7.0x, the command system menu is not identified with the Firefox/NS 7.0x icon on the left end of the titlebar: that's probably a bug.You can access the command system menu with a right-click on the titlebar). Dialog windows are windows which have no minimize system command icon and no maximize/restore down system command icon on the titlebar nor in correspondent menu item in the command system menu.They are said to be dialog because their normal, usual purpose is to only notify info and to be dismissed, closed.On Mac systems, dialog windows have a different window border and they may get turned int?o a sheet.
        /// </summary>
        /// <value></value>
        public bool? Dialog { get; set; }

        /// <summary>
        /// Note: Starting with Mozilla 1.2.1, this feature requires the UniversalBrowserWrite privilege (bug 180048). Without this privilege, it is ignored.
        /// If on, the new window is said to be modal.The user cannot return to the main window until the modal window is closed.A typical modal window is created by the alert() function.
        /// The exact behavior of modal windows depends on the platform and on the Mozilla release version.
        /// </summary>
        /// <value></value>
        public bool? Modal { get; set; }

        /// <summary>
        /// By default, all new secondary windows have a titlebar. If set to no or 0, this feature removes the titlebar from the new secondary window.
        /// </summary>
        /// <value></value>
        public bool? TitleBar { get; set; }

        /// <summary>
        /// If on, the new window will always be displayed on top of other browser windows, regardless of whether it is active or not.
        /// </summary>
        /// <value></value>
        public bool? AlwaysRaised { get; set; }

        /// <summary>
        /// If on, the new created window floats below, under its own parent when the parent window is not minimized. alwaysLowered windows are often referred as pop-under windows. The alwaysLowered window can not be on top of the parent but the parent window can be minimized. In NS 6.x, the alwaysLowered window has no minimize system command icon and no restore/maximize system command.
        /// </summary>
        /// <value></value>
        public bool? AlwaysLowered { get; set; }

        /// <summary>
        /// If on, the new window will always be displayed on top of all other windows (browser windows and otherwise), regardless of whether it is active or not. This was added in Firefox 66 (see bug 1519893).
        /// </summary>
        /// <value></value>
        public bool? AlwaysOnTop { get; set; }

        /// <summary>
        /// When set to no or 0, this feature removes the system close command icon and system close menu item. It will only work for dialog windows (dialog feature set). close=no will override minimizable=yes.
        /// </summary>
        /// <value></value>
        public bool? Close { get; set; }

        internal string GetOpenString()
        {
            var strs = new List<string>();
            if (this.Left.HasValue) strs.Add($"left={Left}");
            if (this.Top.HasValue) strs.Add($"top={Top}");
            if (this.Height.HasValue) strs.Add($"height={Height}");
            if (this.Width.HasValue) strs.Add($"width={Width}");
            if (this.OuterHeight.HasValue) strs.Add($"outerHeight={OuterHeight}");
            if (this.OuterWidth.HasValue) strs.Add($"outerWidth={OuterWidth}");
            if (this.InnerHeight.HasValue) strs.Add($"innerHeight={InnerHeight}");
            if (this.InnerWidth.HasValue) strs.Add($"innerWidth={InnerWidth}");
            if (this.MenuBar.HasValue) strs.Add("menubar=" + (MenuBar.Value ? "yes" : "no"));
            if (this.ToolBar.HasValue) strs.Add("toolbar=" + (ToolBar.Value ? "yes" : "no"));
            if (this.Location.HasValue) strs.Add("location=" + (Location.Value ? "yes" : "no"));
            if (this.PersonalBar.HasValue) strs.Add("personalbar=" + (PersonalBar.Value ? "yes" : "no"));
            if (this.Status.HasValue) strs.Add("status=" + (Status.Value ? "yes" : "no"));
            if (this.Minimizable.HasValue) strs.Add("minimizable=" + (Minimizable.Value ? "yes" : "no"));
            if (this.NoOpener.HasValue) strs.Add("noopener=" + (NoOpener.Value ? "yes" : "no"));
            if (this.NoReferrer.HasValue) strs.Add("noreferrer=" + (NoReferrer.Value ? "yes" : "no"));
            if (this.Resizable.HasValue) strs.Add("resizable=" + (Resizable.Value ? "yes" : "no"));
            if (this.ScrollBars.HasValue) strs.Add("scrollbars=" + (ScrollBars.Value ? "yes" : "no"));
            if (this.Chrome.HasValue) strs.Add("chrome=" + (Chrome.Value ? "yes" : "no"));
            if (this.Dialog.HasValue) strs.Add("dialog=" + (Dialog.Value ? "yes" : "no"));
            if (this.Modal.HasValue) strs.Add("modal=" + (Modal.Value ? "yes" : "no"));
            if (this.TitleBar.HasValue) strs.Add("titlebar=" + (TitleBar.Value ? "yes" : "no"));
            if (this.AlwaysRaised.HasValue) strs.Add("alwaysRaised=" + (AlwaysRaised.Value ? "yes" : "no"));
            if (this.AlwaysLowered.HasValue) strs.Add("alwaysLowered=" + (AlwaysLowered.Value ? "yes" : "no"));
            if (this.AlwaysOnTop.HasValue) strs.Add("alwaysOnTop=" + (AlwaysOnTop.Value ? "yes" : "no"));
            if (this.Close.HasValue) strs.Add("close=" + (Close.Value ? "yes" : "no"));
            if (this.CenterScreen.HasValue) strs.Add("centerscreen=" + (CenterScreen.Value ? "yes" : "no"));
            return string.Join(",", strs);
        }
    }
}