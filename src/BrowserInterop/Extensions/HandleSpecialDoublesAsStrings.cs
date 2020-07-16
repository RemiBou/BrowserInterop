using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BrowserInterop.Extensions
{
    //from https://github.com/dotnet/corefx/issues/41442#issuecomment-553196880
#pragma warning disable CA1812 // This is used as a converter 
    internal class HandleSpecialDoublesAsStrings : JsonConverter<double>
#pragma warning restore CA1812
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String) return reader.GetDouble();
            var specialDouble = reader.GetString();
            return specialDouble switch
            {
                "Infinity" => double.PositiveInfinity,
                "-Infinity" => double.NegativeInfinity,
                _ => double.NaN
            };

        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            if (double.IsFinite(value))
                writer.WriteNumberValue(value);
            else
                writer.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
        }
    }
}