using System.Globalization;

namespace TwitchySharp.Core.Tests.Unit.Primitives;

public class Test_LanguageCode
{
    [Fact]
    public void Constructor_CultureInfo_ExtractsLanguageCode()
    {
        var cultureInfo = new CultureInfo("en-US");

        var languageCode = new LanguageCode(cultureInfo);

        Assert.Equal("en", languageCode.Value);
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
    [InlineData("other")]
    public void TryParse_ValidLanguageCode_ReturnTrue(string code)
    {
        bool result = LanguageCode.TryParse(code, out LanguageCode languageCode);

        Assert.True(result);
        Assert.Equal(code, languageCode.Value);
    }

    [Theory]
    [InlineData("acb")]
    [InlineData("---")]
    [InlineData(".")]
    public void TryParse_InvalidLanguageCode_ReturnFalse(string invalidCode)
    {
        bool result = LanguageCode.TryParse(invalidCode, out LanguageCode languageCode);

        Assert.False(result);
        Assert.Equal(default, languageCode);
    }
}
