using System.Globalization;

namespace TwitchySharp.Core.Tests.Unit.Primitives;

public class Test_LanguageCode
{
    [Theory]
    [InlineData("en")]
    [InlineData("es")]
    [InlineData("fr")]
    [InlineData("de")]
    [InlineData("ja")]
    [InlineData("ko")]
    [InlineData("pt")]
    [InlineData("zh")]
    [InlineData("en-gb")]
    public void ToCultureInfo_ValidLanguageCode_ReturnsCultureInfo(string validCode)
    {
        CultureInfo culture = new LanguageCode(validCode).ToCultureInfo();
    }

    [Fact]
    public void ToCultureInfo_InvalidLanguageCode_ThrowsCultureNotFoundException()
    {
        const string INVALID_CODE = "---";

        Assert.Throws<CultureNotFoundException>(() => new LanguageCode(INVALID_CODE).ToCultureInfo());
    }
}
