using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.JsonConverters;
public class SecondsTimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType switch
        {
            JsonTokenType.Number => TimeSpan.FromSeconds(reader.GetDouble()),
            JsonTokenType.String => reader.GetString() switch
            {
                string value => TimeSpan.FromSeconds(double.Parse(value)),
                _ => default
            },
            _ => default
        };

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        => writer.WriteNumberValue(value.TotalSeconds);
}
