using TwitchySharp.Serialization;
using TwitchySharp.Tests.Unit;

namespace TwitchySharp.Core.Tests.Unit.Serialization;

public class MinutesTimeSpanJsonConverterTestDataset
    : IJsonConverterTestDataset<TimeSpan>
{
    public static IEnumerable<JsonConverterTestData<TimeSpan>> ValidData => [
        new() { Value = TimeSpan.FromMinutes(1), Json = "1" },
        new() { Value = TimeSpan.FromHours(1), Json = "60" },
        new() { Value = TimeSpan.FromSeconds(30), Json = "0.5" },
        new() { Value = TimeSpan.Zero, Json = "0" },
        new() { Value = TimeSpan.FromMinutes(-1), Json = "-1" },
        ];

    public static IEnumerable<string> InvalidJson => [
        "true",
        "null",
        "[]",
        "{}"
        ];
}

public class Test_MinutesTimeSpanJsonConverter
    : JsonConverterTest<TimeSpan, MinutesTimeSpanJsonConverter, MinutesTimeSpanJsonConverterTestDataset>
{
    protected override MinutesTimeSpanJsonConverter Converter { get; } = new();
}
