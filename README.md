# BrowserInterop

[![Build Status](https://dev.azure.com/remibou/toss/_apis/build/status/RemiBou.BrowserInterop?branchName=master)](https://dev.azure.com/remibou/toss/_build/latest?definitionId=9&branchName=master)

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

I use the following website for discoering API description https://developer.mozilla.org/en-US/docs/Web/API and this one for finding out if it is implemented  https://caniuse.com/.

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

- console
    - console.assert
    - console.clear
    - console.count
    - console.countReset
    - console.debug
    - console.dir
    - console.dirXml
    - console.error
    - console.group
    - console.groupEnd
    - console.log
    - console.profile
    - console.profileEnd
    - console.table
    - console.time
    - console.timeEnd
    - console.timeLog
    - console.timeStamp
    - console.trace
- navigator
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

    





