using System.Globalization;

namespace TwitchySharp.Core.Tests.Unit;

public class Test_LanguageCode
{
    [Fact]
    public void Other_HasCorrectValue()
    {
        Assert.Equal("other", LanguageCode.Other.Value);
    }

    [Fact]
    public void TryParse_ValidCode_ReturnsTrue()
    {
        var success = LanguageCode.TryParse("en", out var languageCode);

        Assert.True(success);
        Assert.Equal("en", languageCode.Value);
    }

    [Fact]
    public void TryParse_Other_ReturnsTrue()
    {
        var success = LanguageCode.TryParse("other", out var languageCode);

        Assert.True(success);
        Assert.Equal(LanguageCode.Other, languageCode);
    }

    [Fact]
    public void TryParse_OtherCaseInsensitive_ReturnsTrue()
    {
        var success = LanguageCode.TryParse("OTHER", out var languageCode);

        Assert.True(success);
        Assert.Equal("other", languageCode.Value);
    }

    [Fact]
    public void TryParse_InvalidCode_ReturnsFalse()
    {
        var success = LanguageCode.TryParse("xx", out var languageCode);

        Assert.False(success);
        Assert.Equal(default, languageCode);
    }

    [Fact]
    public void TryParse_NullCode_ReturnsFalse()
    {
        var success = LanguageCode.TryParse(null!, out var languageCode);

        Assert.False(success);
        Assert.Equal(default, languageCode);
    }

    [Fact]
    public void Constructor_CultureInfo_ExtractsLanguageCode()
    {
        var cultureInfo = new CultureInfo("en-US");

        var languageCode = new LanguageCode(cultureInfo);

        Assert.Equal("en", languageCode.Value);
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        LanguageCode.TryParse("fr", out var languageCode);

        var result = languageCode.ToString();

        Assert.Equal("fr", result);
    }

    [Fact]
    public void ImplicitOperator_ConvertsToString()
    {
        LanguageCode.TryParse("de", out var languageCode);

        string result = languageCode;

        Assert.Equal("de", result);
    }

    [Theory]
    [InlineData("en")]
    [InlineData("es")]
    [InlineData("fr")]
    [InlineData("de")]
    [InlineData("ja")]
    [InlineData("ko")]
    [InlineData("pt")]
    [InlineData("zh")]
    public void TryParse_CommonLanguages_AllSucceed(string code)
    {
        var success = LanguageCode.TryParse(code, out var languageCode);

        Assert.True(success);
        Assert.Equal(code, languageCode.Value);
    }
}
