using System.Text.Json;
using TwitchySharp.Serialization;
using TwitchySharp.Tests.Unit;

namespace TwitchySharp.Core.Tests.Unit.Serialization;

public class EmptyDateTimeOffsetConverterDataset
    : IJsonConverterTestDataset<DateTimeOffset?>
{
    public static IEnumerable<JsonConverterTestData<DateTimeOffset?>> ValidData
        => [
            new() { Value = null, Json = string.Empty.AsJson() },
            new() { Value = null, Json = "null" },
            new() { Value = new(2024, 1, 15, 10, 30, 0, TimeSpan.Zero), Json = "2024-01-15T10:30:00Z".AsJson() },
            new() { Value = new(2018, 2, 1, 0, 0, 0, TimeSpan.Zero), Json = "2018-02-01T00:00:00Z".AsJson() },
            new() { Value = new(2000, 1, 1, 0, 0, 0, TimeSpan.FromHours(5)), Json = "2000-01-01T00:00:00+05:00".AsJson() }
            ];

    public static IEnumerable<string> InvalidJson => [ "23", "1.2", "true", "[]", "{}" ];
}

public sealed class Test_EmptyDateTimeOffsetConverter
    : JsonConverterTest<DateTimeOffset?, EmptyDateTimeOffsetConverter, EmptyDateTimeOffsetConverterDataset>
{
    protected override EmptyDateTimeOffsetConverter Converter { get; } = new();
    protected override JsonSerializerOptions? SerializerOptions { get; } = new()
    {
        // This hopefully won't be an issue on the live API.
        // Strict changes the "+" for the timezone offset to "\u002B", which hopefully is accepted by Twitch.
        // Although the test harness expect the exact "+" string value.
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public override void Write_ValidValue_ReturnsExpectedJson(JsonConverterTestData<DateTimeOffset?> valid)
    {
        string expected = (valid.Value switch
        {
            DateTimeOffset value => value.ToString(value.Offset == TimeSpan.Zero
                ? "yyyy-MM-ddTHH:mm:ss.fffffffZ"
                : "yyyy-MM-ddTHH:mm:ss.fffffffzzz"),
            _ => string.Empty
        }).AsJson();

        string json = Converter.Write(valid.Value, SerializerOptions);

        Assert.Equal(expected, json);
    }
}
