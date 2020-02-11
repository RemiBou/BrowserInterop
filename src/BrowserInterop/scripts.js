
browserInterop = new (function () {
    var weakMap = new WeakMap();
    var weakMapKeys = {};
    var jsObjectRefId = 0;

    const jsRefKey = '__jsObjectRefId'; // Keep in sync with ElementRef.cs

    //reviver will help me store js object ref on .net like the .net do with elementreference or dotnetobjectreference
    DotNet.attachReviver((key, value) => {
        if (value &&
            typeof value === 'object' &&
            value.hasOwnProperty(jsRefKey) &&
            typeof value[jsRefKey] === 'number') {

            var id = value[jsRefKey];
            if (!(id in weakMapKeys) && !weakMap.has(weakMapKeys[id])) {
                throw new Error("This JS object reference does not exists : " + id);
            }
            const instance = weakMap.get(weakMapKeys[id]);
            return instance;
        } else {
            return value;
        }
    });
    var me = this;
    var eventListenersIdCurrent = 0;
    this.eventListeners = {};
    this.getPropertyRef = function (propertyPath) {
        return me.getInstancePropertyRef(window, propertyPath);
    };
    this.getInstancePropertyRef = function (instance, propertyPath) {
        var res = me.getInstanceProperty(instance, propertyPath);
        var id = jsObjectRefId++;
        weakMapKeys[id] = { id: id };
        weakMap.set(weakMapKeys[id], res);
        var jsRef = {};
        jsRef[jsRefKey] = id;

        return jsRef;
    };
    this.getProperty = function (propertyPath) {
        return me.getInstanceProperty(window, propertyPath);
    };
    this.callInstanceMethod = function (instance, methodPath, ...args) {
        var method = me.getInstanceProperty(instance, methodPath);
        return method.apply(instance, args);
    }
    this.getInstanceProperty = function (instance, propertyPath) {
        var currentProperty = instance;
        var splitProperty = propertyPath.replace('[', '.').replace(']', '').split('.');

        for (i = 0; i < splitProperty.length; i++) {
            if (splitProperty[i] in currentProperty) {
                currentProperty = currentProperty[splitProperty[i]];
            } else {
                return null;
            }
        }

        return currentProperty;
    }
    this.addEventListener = function (propertyPath, eventName, dotnetAction) {
        var target = me.getProperty(propertyPath);
        var methodRef = function () {
            return dotnetAction.invokeMethodAsync('Invoke');
        }
        target.addEventListener(eventName, methodRef);
        var eventId = eventListenersIdCurrent++;
        me.eventListeners[eventId] = methodRef;
        return eventId;
    };
    this.removeEventListener = function (propertyPath, eventName, eventListenersId) {
        var target = me.getProperty(propertyPath);
        target.removeEventListener(eventName, me.eventListeners[eventListenersId]);
        delete me.eventListeners[eventListenersId];
    };
    this.hasProperty = function (propertyPath) {
        return me.getProperty(propertyPath) !== null;
    };
    this.getSerializableObject = function (data, alreadySerialized) {
        if (!alreadySerialized) {
            alreadySerialized = [];
        }
        var res = {};
        for (var i in data) {
            var currentMember = data[i];

            if (typeof currentMember === 'function' || currentMember === null) {
                continue;
            } else if (typeof currentMember === 'object') {
                if (alreadySerialized.indexOf(currentMember) < 0) {
                    alreadySerialized.push(currentMember);
                    if (Array.isArray(currentMember) || currentMember.length) {
                        res[i] = [];
                        for (var j = 0; j < currentMember.length; j++) {
                            const arrayItem = currentMember[j];
                            if (typeof arrayItem === 'object') {
                                res[i].push(me.getSerializableObject(arrayItem, alreadySerialized));
                            } else {
                                res[i].push(arrayItem);
                            }
                        }
                    } else {
                        //the browser provides some member (like plugins) as hash with index as key, if length == 0 we shall not convert it
                        if (currentMember.length === 0) {
                            res[i] = [];
                        } else {
                            res[i] = me.getSerializableObject(currentMember, alreadySerialized);
                        }
                    }
                }

            } else {
                // string, number or boolean
                if (currentMember === Infinity) { //inifity is not serialized by JSON.stringify
                    currentMember = "Infinity";
                }
                if (currentMember !== null) { //needed because the default json serializer in jsinterop serialize null values
                    res[i] = currentMember;
                }
            }
        }
        return res;
    };
    this.setInstanceProperty = function (instance, propertyPath, value) {
        var currentProperty = instance;
        var splitProperty = propertyPath.replace('[', '.').replace(']', '').split('.');
        for (i = 0; i < splitProperty.length; i++) {
            if (splitProperty[i] in currentProperty) {
                if (i === splitProperty.length - 1) {
                    currentProperty[splitProperty[i]] = value;
                    return;
                } else {
                    currentProperty = currentProperty[splitProperty[i]];
                }
            } else {
                return;
            }
        }
    };
    this.getInstancePropertySerializable = function (instance, propertyName) {

        var data = me.getInstanceProperty(instance, propertyName);
        var res = me.getSerializableObject(data);
        return res;
    };
    this.navigator = new (function () {
        this.geolocation = new (function () {
            this.getCurrentPosition = function (options) {
                return new Promise(function (resolve) {
                    navigator.geolocation.getCurrentPosition(
                        position => resolve({ location: me.getSerializableObject(position) }),
                        error => resolve({ error: me.getSerializableObject(error) }),
                        options)
                });
            };
            this.watchPosition = function (options, wrapper) {
                return navigator.geolocation.watchPosition(
                    position => {
                        const result = { location: me.getSerializableObject(position) };
                        return wrapper.invokeMethodAsync('Invoke', result);
                    },
                    error => wrapper.invokeMethodAsync('Invoke', { error: me.getSerializableObject(error) }),
                    options
                );
            };
        })();
        this.getBattery = function () {
            return new Promise(function (resolve, reject) {
                if (navigator.battery) {//some browser does not implement getBattery but battery instead see https://developer.mozilla.org/en-US/docs/Web/API/Navigator/battery
                    var res = me.getSerializableObject(navigator.battery);
                    resolve(res);
                }
                else if ('getBattery' in navigator) {
                    navigator.getBattery().then(
                        function (battery) {
                            var res = me.getSerializableObject(battery);
                            resolve(res);
                        }
                    );
                }
                else {
                    resolve(null);
                }
            });
        }
    })();
})();