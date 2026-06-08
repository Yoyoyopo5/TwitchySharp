using TwitchySharp.Serialization;
using TwitchySharp.Tests.Unit;

namespace TwitchySharp.Core.Tests.Unit.Serialization;

public class IanaTimeZoneJsonConverterDataset
    : IJsonConverterTestDataset<TimeZoneInfo>
{
    private static IEnumerable<(string Windows, string Iana)> WindowsIanaMapping =>
        [
            new("Eastern Standard Time", "America/New_York"),
            new("Argentina Standard Time", "America/Buenos_Aires"),
            new("China Standard Time", "Asia/Shanghai"),
            new("UTC", "Etc/UTC")
        ];

    public static IEnumerable<JsonConverterTestData<TimeZoneInfo>> ValidData
        => WindowsIanaMapping.Select(mapping =>
            new JsonConverterTestData<TimeZoneInfo>()
            {
                Value = TimeZoneInfo.FindSystemTimeZoneById(OperatingSystem.IsWindows() ? mapping.Windows : mapping.Iana),
                Json = mapping.Iana.AsJson()
            });

    public static IEnumerable<string> InvalidJson => [ "Nirn/Tamriel".AsJson(), "XXX".AsJson(), "42", "[]", "{}" ];
}

public class Test_IanaTimeZoneJsonConverter
    : JsonConverterTest<TimeZoneInfo, IanaTimeZoneJsonConverter, IanaTimeZoneJsonConverterDataset>
{
    protected override IanaTimeZoneJsonConverter Converter { get; } = new();

    protected override Func<TimeZoneInfo?, TimeZoneInfo?, bool> Equal
        => (x, y) => x?.BaseUtcOffset == y?.BaseUtcOffset;
}
