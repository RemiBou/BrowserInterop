# BrowserInterop

[![Build Status](https://dev.azure.com/remibou/toss/_apis/build/status/RemiBou.BrowserInterop?branchName=master)](https://dev.azure.com/remibou/toss/_build/latest?definitionId=9&branchName=master) [![BrowserInterop](https://img.shields.io/nuget/v/BrowserInterop.svg)](https://www.nuget.org/packages/BrowserInterop/)

This library provides access to browser API in a Blazor App. 

The following criteria are taken into account for choosing if an API must be handled :
- Is it already doable with Blazor (like xhr or dom manipulation) ?
- Is that part of the standard ?
- Is that implemented by most browsers ? (> 75% in caniuse)

This library aim at providing some added value which are :
- Better typing : duration as TimeSpan, string as enum ...
- Use IAsyncDisposable for method call that must be executed around a code block (like profiling) or event subscription
- Func for event subscription
- Check for method implementation

I use the following website for discovering API description https://developer.mozilla.org/en-US/docs/Web/API and this one for finding out if it is implemented  https://caniuse.com/.

## Quick Start

First install the package 

```
dotnet add package BrowserInterop
```

Then in your template enter the API with the Window() extension method like this :

```c#
@using BrowserInterop
...
@code {
    protected override async Task OnInitializedAsync()
    {
        var window = await jsRuntime.Window();
        await window.Console.Log("this is a {0}","Log message");
      
    }
}
```

You can find more usage here : https://github.com/RemiBou/BrowserInterop/tree/master/sample/SampleApp

## API covered
Those are the first API covered, more will come, please open an issue if you think some API might be valuable.
- window
    - window.history
        - history.length
        - history.scrollRestoration
        - history.go
        - history.back
        - history.forward
        - history.state
        - history.pushState
        - history.replaceState
    - window.frames
        - window.frames[i]
        - window.frames.length
    - window.console
        - window.console.assert
        - window.console.clear
        - window.console.count
        - window.console.countReset
        - window.console.debug
        - window.console.dir
        - window.console.dirXml
        - window.console.error
        - window.console.group
        - window.console.groupEnd
        - window.console.log
        - window.console.profile
        - window.console.profileEnd
        - window.console.table
        - window.console.time
        - window.console.timeEnd
        - window.console.timeLog
        - window.console.timeStamp
        - window.console.trace
    - window.navigator
        - navigator.appCodeName
        - navigator.appName
        - navigator.appVersion
        - navigator.getBattery()
            - battery.charging
            - battery.chargingTime
            - battery.dischargingTime
            - battery.level
        - navigator.connection
            - navigator.connection.downLink
            - navigator.connection.downLinkMax
            - navigator.connection.effectiveType
            - navigator.connection.rtt
            - navigator.connection.saveData
            - navigator.connection.type
            - navigator.connection.onchange
        - navigator.cookieEnabled
        - navigator.hardwareConcurrency
        - navigator.geolocation
            - navigator.geolocation.getCurrentPosition()
            - navigator.geolocation.watchPosition()
        - navigator.javaEnabled()
        - navigator.language
        - navigator.languages
        - navigator.maxTouchPoints
        - navigator.mimeTypes
            - mimeType.type
            - mimeType.suffix
            - mimeType.description
            - mimeType.plugin
        - navigator.online
        - navigator.platform
        - navigator.plugins
            - plugin.name
            - plugin.fileName
            - plugin.description
            - plugin.version
        - navigator.storage
            - navigator.storage.estimate()
            - navigator.storage.persist()
            - navigator.storage.persisted()
        - navigator.userAgent
        - navigator.canShare()
        - navigator.registerProtocolHandler()
        - navigator.sendBeacon()
        - navigator.share()
        - navigator.vibrate()
    - window.parent
    - window.opener
    - window.innerHeight
    - window.innerWidth
    - window.locationBar
    - window.menuBar
    - window.localStorage / window.sessionStorage
        - storage.length
        - storage.key
        - storage.getItem
        - storage.setItem
        - storage.clear
        - storage.removeItem
    - window.name
    - window.outerHeight
    - window.outerWidth
    - window.performance
        - performance.timeOrigin
        - performance.clearMeasures
        - performance.clearMarks
        - performance.clearResourceTimings
        - performance.getEntries
        - performance.getEntriesByName
        - performance.getEntriesByType
        - performance.mark
        - performance.measure
        - performance.now
        - performance.setResourceTimingBufferSize
## Utility method

With the development of the library I needed a few utilities method :

```cs
IJSRuntime jsRuntime;
// this will get a reference to the js window object that you can use later, it works like ElementReference ofr DotNetRef : you can add it to any method parameter and it 
// will be changed in the corresponding js object 
var windowObjectRef = await jsRuntime.GetInstancePropertyAsync<JsRuntimeObjectRef>("window");
// get the value of window.performance.timeOrigin
var time = await jsRuntime.GetInstancePropertyAsync<decimal>(windowObjectRef, "performance.timeOrigin");
// set the value of the property window.history.scrollRestoration
await jsRuntime.SetInstancePropertyAsync(windowObjectRef, "history.scrollRestoration", "auto");
//get a reference to window.parent
var parentRef = await jsRuntime.GetInstancePropertyRefAsync(windowObjectRef, "parent");
// call the method window.console.clear with window.console as scope
await jsRuntime.InvokeInstanceMethodAsync(windowObjectRef, "console.clear");
// call the method window.history.key(1) with window.history as scope
await jsRuntime.InvokeInstanceMethodAsync<string>(windowObjectRef, "history.key",1 );
// will call the method navigator.storage.persist and will return default(bool) if there is no response after 3 secs
jsRuntime.InvokeOrDefaultAsync<bool>("navigator.storage.persist",TimeSpan.FromSeconds(3), null)
//will listen for the event until DisposeAsync is called on the result
var listener = await jSRuntime.AddEventListener(windowObjectRef, "navigator.connection", "change", () => Console.WriteLine("navigator connection change"));
//stop listening to the event, you can also use "await using()" notation
await listener.DisposeAsync();
//will return true if window.navigator.registerProtocolHandler property exists
await jsRuntime.HasProperty(windowObjectRef, "navigator.registerProtocolHandler")
```

    





