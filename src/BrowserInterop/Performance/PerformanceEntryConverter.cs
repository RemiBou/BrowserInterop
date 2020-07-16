using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BrowserInterop.Performance
{
    public class PerformanceEntryConverter : JsonConverter<PerformanceEntry>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(PerformanceEntry).IsAssignableFrom(typeToConvert);
        }

        public override PerformanceEntry Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (!JsonDocument.TryParseValue(ref reader, out var jsonDocument))
                return null;
            try
            {
                var entryTypeStr = jsonDocument.RootElement.GetProperty("entryType").GetString();
                var entryType = WindowPerformance.ConvertStringToType(entryTypeStr);
                return (PerformanceEntry) JsonSerializer.Deserialize(jsonDocument.RootElement.GetRawText(), entryType,
                    options);
            }
            finally
            {
                jsonDocument?.Dispose();
            }
        }

        public override void Write(Utf8JsonWriter writer, PerformanceEntry value, JsonSerializerOptions options)
        {
            //we use the default serializer
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}