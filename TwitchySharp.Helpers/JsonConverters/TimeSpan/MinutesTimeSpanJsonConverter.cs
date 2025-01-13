using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.JsonConverters;
public class MinutesTimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType switch
        {
            JsonTokenType.Number => TimeSpan.FromMinutes(reader.GetDouble()),
            JsonTokenType.String => reader.GetString() switch
            {
                string value => TimeSpan.FromMinutes(double.Parse(value)),
                _ => default
            },
            _ => default
        };

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        => writer.WriteNumberValue(value.TotalSeconds);
}
