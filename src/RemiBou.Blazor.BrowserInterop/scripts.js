
browserInterop = {
    getProperty: function (propertyName) {
        var splitProperty = propertyName.split('.');
        var currentProperty = window;
        for (i = 0; i < splitProperty.length; i++) {
            currentProperty = currentProperty[splitProperty[i]];
        }
        return currentProperty;
    },
    credential: {
        create: function (param) {
            console.log(param);
            return new Promise(function (resolve, reject) {
                navigator.credentials.create(param).then(
                    credential => {
                        console.log(credential);
                        return resolve(browserInterop.getSerializableObject(credential, []));
                    }
                );
            })
        },
    },
    getBattery: function () {
        return new Promise(function (resolve, reject) {
            if (navigator.battery) {//some browser does not implement getBattery but battery instead see https://developer.mozilla.org/en-US/docs/Web/API/Navigator/battery
                var res = browserInterop.getSerializableObject(navigator.battery, []);
                resolve(res);
                return;
            }
            navigator.getBattery().then(
                function (battery) {
                    var res = browserInterop.getSerializableObject(battery, []);
                    resolve(res);
                }
            );
        });
    },
    getSerializableObject: function (data, alreadySerialized) {
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
                                res[i].push(browserInterop.getSerializableObject(arrayItem, alreadySerialized));
                            } else {
                                res[i].push(arrayItem);
                            }
                        }
                    } else {
                        res[i] = browserInterop.getSerializableObject(currentMember, alreadySerialized);
                    }
                }

            } else {
                // string, number or boolean
                if (currentMember === Infinity) { //inifity is not serialized by JSON.stringify
                    currentMember = "Infinity";
                }
                res[i] = currentMember;
            }
        }
        return res;
    },
    getAsJson: function (propertyName) {

        var data = browserInterop.getProperty(propertyName);
        var res = browserInterop.getSerializableObject(data, []);
        return res;
    },
    navigator: {
        mimeTypes: function () {
            var res = [];
            for (i = 0; i <= navigator.mimeTypes.length; i++) {
                var mimeType = navigator.mimeTypes[i];
                var current = {
                    type: mimeType.type,
                    suffix: mimeType.suffix,
                    description: mimeType.description
                };
                if (mimeType.enabledPlugin) {
                    current.enabledPlugin = {
                        name: mimeType.enabledPlugin.name,
                        filename: mimeType.enabledPlugin.filename,
                        description: mimeType.enabledPlugin.description,
                        version: mimeType.enabledPlugin.version
                    }
                }
                res.push(current);
            }
            return res;
        },
        plugins: function () {
            var res = [];
            for (i = 0; i <= navigator.plugins.length; i++) {
                var plugin = navigator.plugins[i];
                var current = {
                    name: plugin.name,
                    filename: plugin.filename,
                    description: plugin.description,
                    version: plugin.version
                };
                res.push(current);
            }
            return res;
        }

    }
}