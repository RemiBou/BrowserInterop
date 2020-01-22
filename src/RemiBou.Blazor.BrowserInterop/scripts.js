
browserInterop = {
    getProperty: function (propertyName) {
        var splitProperty = propertyName.split('.');
        var currentProperty = window;
        for (i = 0; i < splitProperty.length; i++) {
            currentProperty = currentProperty[splitProperty[i]];
        }
        return currentProperty;
    },
    getAsJson: function (propertyName) {
        var alreadySerialized = [];//this is for avoiding infinite loop
        function getSerializableObject(data) {
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
                                    res[i].push(getSerializableObject(arrayItem));
                                } else {
                                    res[i].push(arrayItem);
                                }
                            }
                        } else {
                            res[i] = getSerializableObject(currentMember);

                        }
                    }
                
                } else {
                    // string, number or boolean
                    res[i] = currentMember;
                }
            }
            return res;
        }
        var data = browserInterop.getProperty(propertyName);
        var res = getSerializableObject(data);
        console.log(res, JSON.stringify(res));
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