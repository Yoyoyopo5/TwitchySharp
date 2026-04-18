using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Helpers.Tests.Unit.JsonConverters;

public partial class Test_WrapperJsonConverter
{
    [Fact]
    public void Read_WrappedStringValue_ReturnsWrapper()
    {
        const string json = "\"test-value\"";

        var result = JsonSerializer.Deserialize<TestStringWrapper>(json);

        Assert.NotNull(result);
        Assert.Equal("test-value", result.Value);
    }

    [Fact]
    public void Read_WrappedIntValue_ReturnsWrapper()
    {
        const string json = "42";

        var result = JsonSerializer.Deserialize<TestIntWrapper>(json);

        Assert.NotNull(result);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void Write_StringWrapper_WritesWrappedValue()
    {
        var wrapper = new TestStringWrapper("test-value");

        var result = JsonSerializer.Serialize(wrapper);

        Assert.Equal("\"test-value\"", result);
    }

    [Fact]
    public void Write_IntWrapper_WritesWrappedValue()
    {
        var wrapper = new TestIntWrapper(42);

        var result = JsonSerializer.Serialize(wrapper);

        Assert.Equal("42", result);
    }

    [Fact]
    public void RoundTrip_StringWrapper_PreservesValue()
    {
        var original = new TestStringWrapper("round-trip-test");

        var json = JsonSerializer.Serialize(original);
        var result = JsonSerializer.Deserialize<TestStringWrapper>(json);

        Assert.NotNull(result);
        Assert.Equal(original.Value, result.Value);
    }

    [Wrapper<string>]
    private partial record TestStringWrapper(string Value);
    [Wrapper<int>]
    private partial record TestIntWrapper(int Value);
}
