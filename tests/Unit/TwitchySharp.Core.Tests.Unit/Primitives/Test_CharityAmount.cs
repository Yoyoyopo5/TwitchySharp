using Xunit.Sdk;

namespace TwitchySharp.Core.Tests.Unit.Primitives;

public class Test_CharityAmount
{
    public record TestCharityAmount
        : IXunitSerializable
    {
        public required int Value { get; set; }
        public required int DecimalPlaces { get; set; }
        public required double Expected { get; set; }

        public CharityAmount CharityAmount => new()
        {
            Currency = GetTestCurrencyCode(),
            DecimalPlaces = DecimalPlaces,
            Value = Value
        };

        public void Deserialize(IXunitSerializationInfo info)
        {
            Value = info.GetValue<int>(nameof(Value));
            DecimalPlaces = info.GetValue<int>(nameof(DecimalPlaces));
            Expected = info.GetValue<double>(nameof(Expected));
        }
        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(Value), Value);
            info.AddValue(nameof(DecimalPlaces), DecimalPlaces);
            info.AddValue(nameof(Expected), Expected);
        }
    }

    private static CurrencyCode GetTestCurrencyCode()
        => new("USD");

    public static IEnumerable<TheoryDataRow<TestCharityAmount>> MonetaryValueData =>
        [
            new(new() { Value = 100, DecimalPlaces = 2, Expected = 1.00 }),
            new(new() { Value = 250, DecimalPlaces = 0, Expected = 250.0 }),
            new(new() { Value = 15, DecimalPlaces = 1, Expected = 1.5 }),
            new(new() { Value = 2382, DecimalPlaces = 3, Expected = 2.382 }),
            new(new() { Value = 5000000, DecimalPlaces = 6, Expected = 5.0 }),
        ];

    [Theory]
    [MemberData(nameof(MonetaryValueData))]
    public void GetMonetaryValue_ReturnsCorrectResult(TestCharityAmount testAmount)
        => Assert.Equal(testAmount.Expected, testAmount.CharityAmount.MonetaryValue);
}
