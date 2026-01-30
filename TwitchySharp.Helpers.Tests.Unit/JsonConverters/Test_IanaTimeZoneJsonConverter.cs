using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.Tests;

namespace TwitchySharp.Helpers.Tests.Unit.JsonConverters;

public class Test_IanaTimeZoneJsonConverter
{
    private readonly IanaTimeZoneJsonConverter _converter = new();

    [Fact]
    public void Read_ValidIanaString_ReturnsTimeZoneInfo()
    {
        const string json = "\"America/New_York\"";

        var result = _converter.Read(json);

        Assert.NotNull(result);
        // On Windows, FindSystemTimeZoneById converts IANA to Windows timezone IDs
        // On Linux/macOS, IANA IDs are used directly
        // We need a way to test this that is cross-platform.
        Assert.Contains("America/New_York", result.DisplayName, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Read_UtcTimezone_ReturnsUtcTimeZoneInfo()
    {
        const string json = "\"UTC\"";

        var result = _converter.Read(json);

        Assert.NotNull(result);
        Assert.Equal(TimeSpan.Zero, result.BaseUtcOffset);
    }

    [Fact]
    public void Write_TimeZoneInfo_WritesTimezoneId()
    {
        var timeZone = TimeZoneInfo.Utc;

        var result = _converter.Write(timeZone);

        Assert.Equal("\"UTC\"", result);
    }

    [Fact]
    public void Read_InvalidToken_ThrowsJsonException()
    {
        const string json = "123";

        var (isSuccessful, _) = _converter.TryRead(json);

        Assert.False(isSuccessful);
    }

    [Fact]
    public void Read_InvalidTimezone_ThrowsException()
    {
        const string json = "\"Invalid/Timezone\"";

        var (isSuccessful, _) = _converter.TryRead(json);

        Assert.False(isSuccessful);
    }

    [Fact]
    public void RoundTrip_UtcTimeZone_PreservesValue()
    {
        var original = TimeZoneInfo.Utc;

        var json = _converter.Write(original);
        var result = _converter.Read(json);

        Assert.Equal(original, result);
    }
}
