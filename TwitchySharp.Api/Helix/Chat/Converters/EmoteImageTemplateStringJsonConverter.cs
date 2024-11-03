using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Chat;
internal class EmoteImageTemplateStringJsonConverter : JsonConverter<EmoteImageTemplateString>
{
    public override EmoteImageTemplateString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(typeToConvert == typeof(EmoteImageTemplateString));
        if (reader.TokenType != JsonTokenType.String)
            throw new NotSupportedException("Value must be a string.");
        if (reader.GetString() is not string value)
            throw new NotSupportedException("Value must be a non-null string.");
        return new EmoteImageTemplateString() { TemplateString = value };
    }

    public override void Write(Utf8JsonWriter writer, EmoteImageTemplateString value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.TemplateString);
}
