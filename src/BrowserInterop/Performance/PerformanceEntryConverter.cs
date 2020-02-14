using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BrowserInterop.Performance
{
    public class PerformanceEntryConverter : JsonConverter<PerformanceEntry>
    {
        public override bool CanConvert(Type typeToConvert) => typeof(PerformanceEntry).IsAssignableFrom(typeToConvert);

        public override PerformanceEntry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!JsonDocument.TryParseValue(ref reader, out var jsonDocument))
                return null;
            var entryTypeStr = jsonDocument.RootElement.GetProperty("entryType").GetRawText();
            Type entryType = entryTypeStr switch
            {
                "mark" => typeof(PerformanceMark),
                "measure" => typeof(PerformanceMeasure),
                "frame" => typeof(PerformanceMark),
                "navigation" => typeof(PerformanceNavigationTiming),
                "resource" => typeof(PerformanceMark),
                "paint" => typeof(PerformanceMark),
                _ => typeof(PerformanceMark)
            };
            return (PerformanceEntry)JsonSerializer.Deserialize(jsonDocument.RootElement.GetRawText(), entryType, options);
        }

        public override void Write(Utf8JsonWriter writer, PerformanceEntry value, JsonSerializerOptions options)
        {
            //we use the default serializer
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}