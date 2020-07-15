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
            if (!JsonDocument.TryParseValue(ref reader, out JsonDocument jsonDocument))
                return null;

            string entryTypeStr = jsonDocument.RootElement.GetProperty("entryType").GetString();
            Type entryType = PerformanceInterop.ConvertStringToType(entryTypeStr);
            return (PerformanceEntry)JsonSerializer.Deserialize(jsonDocument.RootElement.GetRawText(), entryType, options);
        }

        public override void Write(Utf8JsonWriter writer, PerformanceEntry value, JsonSerializerOptions options)
        {
            //we use the default serializer
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}