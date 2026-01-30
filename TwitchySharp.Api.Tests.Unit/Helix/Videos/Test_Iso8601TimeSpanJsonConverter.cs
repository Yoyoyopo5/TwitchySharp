using TwitchySharp.Api.Helix.Videos;
using TwitchySharp.Tests;

namespace TwitchySharp.Api.Tests.Unit.Helix.Videos;

public class Test_Iso8601TimeSpanJsonConverter
{
    private readonly Iso8601TimeSpanJsonConverter _converter = new();

    [Fact]
    public void Read_MinutesAndSeconds_ReturnsTimeSpan()
    {
        const string json = "\"3m21s\"";

        var result = _converter.Read(json);

        Assert.Equal(TimeSpan.FromMinutes(3) + TimeSpan.FromSeconds(21), result);
    }

    [Fact]
    public void Read_HoursMinutesSeconds_ReturnsTimeSpan()
    {
        const string json = "\"1h30m45s\"";

        var result = _converter.Read(json);

        Assert.Equal(new TimeSpan(1, 30, 45), result);
    }

    [Fact]
    public void Read_SecondsOnly_ReturnsTimeSpan()
    {
        const string json = "\"45s\"";

        var result = _converter.Read(json);

        Assert.Equal(TimeSpan.FromSeconds(45), result);
    }

    [Fact]
    public void Read_MinutesOnly_ReturnsTimeSpan()
    {
        const string json = "\"5m\"";

        var result = _converter.Read(json);

        Assert.Equal(TimeSpan.FromMinutes(5), result);
    }

    [Fact]
    public void Read_HoursOnly_ReturnsTimeSpan()
    {
        const string json = "\"2h\"";

        var result = _converter.Read(json);

        Assert.Equal(TimeSpan.FromHours(2), result);
    }

    [Fact]
    public void Read_LowercaseInput_ReturnsTimeSpan()
    {
        const string json = "\"1h30m45s\"";

        var result = _converter.Read(json);

        Assert.Equal(new TimeSpan(1, 30, 45), result);
    }

    [Fact]
    public void Read_Null_ReturnsDefault()
    {
        const string json = "null";

        var result = _converter.Read(json);

        Assert.Equal(TimeSpan.Zero, result);
    }

    [Fact]
    public void Read_LongDuration_ReturnsTimeSpan()
    {
        const string json = "\"10h5m30s\"";

        var result = _converter.Read(json);

        Assert.Equal(new TimeSpan(10, 5, 30), result);
    }
}
