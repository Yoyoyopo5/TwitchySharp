using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Helpers.Tests.Unit.JsonConverters;

public class Test_WrapperJsonConverter
{
    [Fact]
    public void Read_WrappedStringValue_ReturnsWrapper()
    {
        const string json = "\"test-value\"";
        var options = new JsonSerializerOptions();
        options.Converters.Add(new WrapperJsonConverter<TestStringWrapper, string>());

        var result = JsonSerializer.Deserialize<TestStringWrapper>(json, options);

        Assert.NotNull(result);
        Assert.Equal("test-value", result.Value);
    }

    [Fact]
    public void Read_WrappedIntValue_ReturnsWrapper()
    {
        const string json = "42";
        var options = new JsonSerializerOptions();
        options.Converters.Add(new WrapperJsonConverter<TestIntWrapper, int>());

        var result = JsonSerializer.Deserialize<TestIntWrapper>(json, options);

        Assert.NotNull(result);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void Write_StringWrapper_WritesWrappedValue()
    {
        var wrapper = new TestStringWrapper("test-value");
        var options = new JsonSerializerOptions();
        options.Converters.Add(new WrapperJsonConverter<TestStringWrapper, string>());

        var result = JsonSerializer.Serialize(wrapper, options);

        Assert.Equal("\"test-value\"", result);
    }

    [Fact]
    public void Write_IntWrapper_WritesWrappedValue()
    {
        var wrapper = new TestIntWrapper(42);
        var options = new JsonSerializerOptions();
        options.Converters.Add(new WrapperJsonConverter<TestIntWrapper, int>());

        var result = JsonSerializer.Serialize(wrapper, options);

        Assert.Equal("42", result);
    }

    [Fact]
    public void RoundTrip_StringWrapper_PreservesValue()
    {
        var original = new TestStringWrapper("round-trip-test");
        var options = new JsonSerializerOptions();
        options.Converters.Add(new WrapperJsonConverter<TestStringWrapper, string>());

        var json = JsonSerializer.Serialize(original, options);
        var result = JsonSerializer.Deserialize<TestStringWrapper>(json, options);

        Assert.NotNull(result);
        Assert.Equal(original.Value, result.Value);
    }

    private record TestStringWrapper(string Value) : IWrapValue<string>;
    private record TestIntWrapper(int Value) : IWrapValue<int>;
}
