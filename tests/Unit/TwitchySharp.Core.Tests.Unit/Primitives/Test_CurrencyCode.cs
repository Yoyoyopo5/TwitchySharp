using System.Globalization;

namespace TwitchySharp.Core.Tests.Unit.Primitives;

public class Test_CurrencyCode
{
    public static IEnumerable<TheoryDataRow<RegionInfo>> RegionTestData =>
        [
            new(new("US")),
            new(new("CA")),
            new(new("JP")),
            new(new("FR")),
            new(new("GB"))
        ];

    [Theory]
    [MemberData(nameof(RegionTestData))]
    public void Constructor_RegionInfo_ExtractsCurrencySymbol(RegionInfo region)
    {
        CurrencyCode currencyCode = new(region);

        Assert.Equal(region.ISOCurrencySymbol, currencyCode.Value);
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        RegionInfo region = new("US");
        CurrencyCode currencyCode = new(region);

        string result = currencyCode.ToString();

        Assert.Equal("USD", result);
    }
}
