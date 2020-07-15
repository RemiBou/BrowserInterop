
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BrowserInterop
{
    //from https://github.com/dotnet/corefx/issues/41442#issuecomment-553196880
    internal class HandleSpecialDoublesAsStrings : JsonConverter<double>
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
                writer.WriteStringValue(value.ToString());
            }
        }
    }

}
