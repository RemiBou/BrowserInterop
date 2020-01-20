browserInterop = {
    getProperty: function (propertyName) {
        var splitProperty = propertyName.split('.');
        var currentProperty = window;
        for (i = 0; i < splitProperty.length; i++) {
            currentProperty = currentProperty[splitProperty[i]];
        }
        return currentProperty;
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