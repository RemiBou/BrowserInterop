
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
            if (reader.TokenType == JsonTokenType.String)
            {
                string specialDouble = reader.GetString();
                if (specialDouble == "Infinity")
                {
                    return double.PositiveInfinity;
                }
                else if (specialDouble == "-Infinity")
                {
                    return double.NegativeInfinity;
                }
                else
                {
                    return double.NaN;
                }
            }
            return reader.GetDouble();
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            if (double.IsFinite(value))
            {
                writer.WriteNumberValue(value);
            }
            else
            {
                writer.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
            }
        }
    }

}
