using System.Text.Json;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Helix.Videos;
using TwitchySharp.Tests.Unit;

namespace TwitchySharp.Api.Tests.Unit.Helix.Videos;

public class Iso8601TimeSpanJsonConverterTestDataset
    : IJsonConverterTestDataset<TimeSpan>
{
    public static IEnumerable<JsonConverterTestData<TimeSpan>> ValidData
        => [
            new() { Value = TimeSpan.Zero, Json = "0H0M0S".AsJson() },
            new() { Value = TimeSpan.FromSeconds(30), Json = "0H0M30S".AsJson() },
            new() { Value = TimeSpan.FromMinutes(5), Json = "0H5M0S".AsJson() },
            new() { Value = TimeSpan.FromHours(1), Json = "1H0M0S".AsJson() }
            ];

    public static IEnumerable<string> InvalidJson
        => [
            "null",
            "{}",
            "[]",
            "5".AsJson(),
            "5"
            ];
}

public class PublicIso8601TimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    private static readonly Iso8601TimeSpanJsonConverter Converter = new();

    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => Converter.Read(ref reader, typeToConvert, options);

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        => Converter.Write(writer, value, options);
}

public class Test_Iso8601TimeSpanJsonConverter
    : JsonConverterTest<TimeSpan, PublicIso8601TimeSpanJsonConverter, Iso8601TimeSpanJsonConverterTestDataset>
{
    protected override PublicIso8601TimeSpanJsonConverter Converter { get; } = new();
}
