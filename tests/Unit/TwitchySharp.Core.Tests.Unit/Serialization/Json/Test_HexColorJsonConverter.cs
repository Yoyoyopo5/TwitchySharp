using System.Text.Json;
using System.Text.Json.Serialization;
using TwitchySharp.Tests.Unit;

namespace TwitchySharp.Core.Tests.Unit.Serialization;

public class HexColorJsonConverterTestDataset
    : IJsonConverterTestDataset<RgbColor>
{
    public static IEnumerable<JsonConverterTestData<RgbColor>> ValidData => [
        new() { Value = new RgbColor(0, 0, 0), Json = "#000000".AsJson() },
        new() { Value = new RgbColor(255, 255, 255), Json = "#FFFFFF".AsJson() },
        new() { Value = new RgbColor(32, 10, 25), Json = "#200A19".AsJson() },
        ];

    public static IEnumerable<string> InvalidJson => [
        "#XXXXXX".AsJson(),
        "asdf".AsJson(),
        "24",
        "[0, 0, 0]",
        "{ \"r\": 29, \"g\": 134, \"b\": 20 }"
        ];
}

public class PublicHexColorJsonConverter : JsonConverter<RgbColor>
{
    private readonly HexColorJsonConverter _converter = new();

    public override RgbColor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => _converter.Read(ref reader, typeToConvert, options);
    public override void Write(Utf8JsonWriter writer, RgbColor value, JsonSerializerOptions options)
        => _converter.Write(writer, value, options);
}

public class Test_HexColorJsonConverter
    : JsonConverterTest<RgbColor, PublicHexColorJsonConverter, HexColorJsonConverterTestDataset>
{
    protected override PublicHexColorJsonConverter Converter { get; } = new();
}
