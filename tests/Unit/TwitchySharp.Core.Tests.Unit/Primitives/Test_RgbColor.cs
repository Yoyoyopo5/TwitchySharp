namespace TwitchySharp.Core.Tests.Unit.Primitives;

public class Test_RgbColor
{
    [Fact]
    public void Constructor_SetsRgbValues()
    {
        RgbColor color = new(255, 87, 51);

        Assert.Equal(255, color.R);
        Assert.Equal(87, color.G);
        Assert.Equal(51, color.B);
    }

    [Fact]
    public void FromHex_WithHash_ParsesCorrectly()
    {
        RgbColor color = RgbColor.FromHex("#FF5733");

        Assert.Equal(255, color.R);
        Assert.Equal(87, color.G);
        Assert.Equal(51, color.B);
    }

    [Fact]
    public void FromHex_WithoutHash_ParsesCorrectly()
    {
        RgbColor color = RgbColor.FromHex("FF5733");

        Assert.Equal(255, color.R);
        Assert.Equal(87, color.G);
        Assert.Equal(51, color.B);
    }

    [Fact]
    public void FromHex_EmptyString_ReturnsBlack()
    {
        RgbColor color = RgbColor.FromHex("");

        Assert.Equal(0, color.R);
        Assert.Equal(0, color.G);
        Assert.Equal(0, color.B);
    }

    [Fact]
    public void FromHex_NullString_ReturnsBlack()
    {
        RgbColor color = RgbColor.FromHex(null);

        Assert.Equal(0, color.R);
        Assert.Equal(0, color.G);
        Assert.Equal(0, color.B);
    }

    [Fact]
    public void FromHex_WhitespaceString_ReturnsBlack()
    {
        RgbColor color = RgbColor.FromHex("   ");

        Assert.Equal(0, color.R);
        Assert.Equal(0, color.G);
        Assert.Equal(0, color.B);
    }

    [Fact]
    public void FromHex_InvalidLength_ThrowsFormatException()
        => Assert.Throws<FormatException>(() => RgbColor.FromHex("FFF"));

    [Fact]
    public void FromHex_8CharHex_ParsesRgbPortion()
    {
        RgbColor color = RgbColor.FromHex("FF5733AA");

        Assert.Equal(255, color.R);
        Assert.Equal(87, color.G);
        Assert.Equal(51, color.B);
    }

    [Fact]
    public void FromHex_LowercaseHex_ParsesCorrectly()
    {
        RgbColor color = RgbColor.FromHex("#ff5733");

        Assert.Equal(255, color.R);
        Assert.Equal(87, color.G);
        Assert.Equal(51, color.B);
    }

    [Fact]
    public void ToString_ReturnsHexWithHash()
    {
        RgbColor color = new(255, 87, 51);

        string result = color.ToString();

        Assert.Equal("#FF5733", result);
    }

    [Fact]
    public void ToString_Black_ReturnsCorrectHex()
    {
        var color = new RgbColor(0, 0, 0);

        var result = color.ToString();

        Assert.Equal("#000000", result);
    }

    [Fact]
    public void ToString_White_ReturnsCorrectHex()
    {
        var color = new RgbColor(255, 255, 255);

        var result = color.ToString();

        Assert.Equal("#FFFFFF", result);
    }

    [Fact]
    public void RoundTrip_FromHexToString_PreservesValue()
    {
        const string originalHex = "#AABBCC";

        RgbColor color = RgbColor.FromHex(originalHex);
        string result = color.ToString();

        Assert.Equal(originalHex, result);
    }

    [Fact]
    public void RoundTrip_FromRgbColor_PreservesValue()
    {
        RgbColor color = new(100, 100, 100);

        string hex = color.ToString();
        RgbColor result = RgbColor.FromHex(hex);

        Assert.Equal(color, result);
    }
}
