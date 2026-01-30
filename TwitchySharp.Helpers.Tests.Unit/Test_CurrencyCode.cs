using System.Globalization;

namespace TwitchySharp.Helpers.Tests.Unit;

public class Test_CurrencyCode
{
    [Fact]
    public void TryParse_ValidCode_ReturnsTrue()
    {
        var success = CurrencyCode.TryParse("USD", out var currencyCode);

        Assert.True(success);
        Assert.Equal("USD", currencyCode.Value);
    }

    [Fact]
    public void TryParse_InvalidCode_ReturnsFalse()
    {
        var success = CurrencyCode.TryParse("XXX", out var currencyCode);

        Assert.False(success);
        Assert.Equal(default, currencyCode);
    }

    [Fact]
    public void TryParse_IsCaseSensitive()
    {
        // CurrencyCode.TryParse is case-sensitive - only uppercase codes are valid
        var successLower = CurrencyCode.TryParse("usd", out _);
        var successUpper = CurrencyCode.TryParse("USD", out var upperCode);

        Assert.False(successLower);
        Assert.True(successUpper);
        Assert.Equal("USD", upperCode.Value);
    }

    [Fact]
    public void Constructor_RegionInfo_ExtractsCurrencySymbol()
    {
        var regionInfo = new RegionInfo("US");

        var currencyCode = new CurrencyCode(regionInfo);

        Assert.Equal("USD", currencyCode.Value);
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        CurrencyCode.TryParse("EUR", out var currencyCode);

        var result = currencyCode.ToString();

        Assert.Equal("EUR", result);
    }

    [Fact]
    public void ImplicitOperator_ConvertsToString()
    {
        CurrencyCode.TryParse("GBP", out var currencyCode);

        string result = currencyCode;

        Assert.Equal("GBP", result);
    }

    [Theory]
    [InlineData("USD")]
    [InlineData("EUR")]
    [InlineData("GBP")]
    [InlineData("JPY")]
    [InlineData("CAD")]
    public void TryParse_CommonCurrencies_AllSucceed(string code)
    {
        var success = CurrencyCode.TryParse(code, out var currencyCode);

        Assert.True(success);
        Assert.Equal(code, currencyCode.Value);
    }
}
