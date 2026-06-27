using TwitchySharp.Serialization;
using TwitchySharp.Tests.Unit;

namespace TwitchySharp.Core.Tests.Unit.Serialization;

public class UnixSecondsDateTimeOffsetConverterTestDataset
    : IJsonConverterTestDataset<DateTimeOffset>
{
    private static IEnumerable<long> UnixTimeSeconds => [
        2783819278,
        1705312200,
        0,
        -100
        ];

    public static IEnumerable<JsonConverterTestData<DateTimeOffset>> ValidData
        => UnixTimeSeconds.Select(s => new JsonConverterTestData<DateTimeOffset>()
        {
            Value = DateTimeOffset.FromUnixTimeSeconds(s),
            Json = s.ToString()
        });

    public static IEnumerable<string> InvalidJson => [
        "true",
        "null",
        "[]",
        "{}"
        ];
}

public class Test_UnixSecondsDateTimeOffsetConverter
    : JsonConverterTest<DateTimeOffset, UnixSecondsDateTimeOffsetConverter, UnixSecondsDateTimeOffsetConverterTestDataset>
{
    protected override UnixSecondsDateTimeOffsetConverter Converter { get; } = new();
}
