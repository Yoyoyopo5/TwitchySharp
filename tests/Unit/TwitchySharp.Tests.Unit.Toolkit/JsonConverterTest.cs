using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;
using Xunit.Sdk;

namespace TwitchySharp.Tests.Unit;

public record JsonConverterTestData<T>
    : IXunitSerializable
{
    public required T Value { get; set; }
    public required string Json { get; set; }

    public void Deserialize(IXunitSerializationInfo info)
    {
        Value = info.GetValue<T>(nameof(Value));
        Json = info.GetValue<string>(nameof(Json));
    }
    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(Value), Value);
        info.AddValue(nameof(Json), Json);
    }
}

public interface IJsonConverterTestDataset<T>
{
    static abstract IEnumerable<JsonConverterTestData<T>> ValidData { get; }
    static abstract IEnumerable<string> InvalidJson { get; }
}

internal delegate bool CompareEquality<in T>(T? left, T? right);

public abstract class JsonConverterTest<T, TConverter, TDataset>
    where TConverter : JsonConverter<T>
    where TDataset : IJsonConverterTestDataset<T>
{
    protected abstract TConverter Converter { get; }
    protected virtual JsonSerializerOptions? SerializerOptions { get; }
    protected virtual Func<T?, T?, bool> Equal { get; } = (l, r) => Equals(l, r);

    private IEqualityComparer<T> Comparer => new FunctionalComparer((x, y) => Equal(x, y));

    private record FunctionalComparer(CompareEquality<T> Compare) : IEqualityComparer<T>
    {
        public bool Equals(T? x, T? y) => Compare(x, y);
        public int GetHashCode([DisallowNull] T obj) => obj.GetHashCode();
    }

    public static IEnumerable<TheoryDataRow<JsonConverterTestData<T>>> ValidDataset
        => TDataset.ValidData.Select(d => new TheoryDataRow<JsonConverterTestData<T>>(d));

    public static IEnumerable<TheoryDataRow<string>> InvalidJsonDataset
        => TDataset.InvalidJson.Select(d => new TheoryDataRow<string>(d));

    [Theory]
    [MemberData(nameof(ValidDataset))]
    public virtual void Read_ValidJson_ReturnsExpectedValue(JsonConverterTestData<T> valid)
    {
        T? value = Converter.Read(valid.Json, SerializerOptions);
        Assert.Equal(valid.Value, value, comparer: Comparer);
    }

    [Theory]
    [MemberData(nameof(ValidDataset))]
    public virtual void Write_ValidValue_ReturnsExpectedJson(JsonConverterTestData<T> valid)
    {
        string json = Converter.Write(valid.Value, SerializerOptions);
        Assert.Equal(valid.Json, json);
    }

    [Theory]
    [MemberData(nameof(InvalidJsonDataset))]
    public virtual void Read_InvalidJson_ThrowsException(string invalidJson)
        => Assert.ThrowsAny<Exception>(() => Converter.Read(invalidJson, SerializerOptions));

    [Theory]
    [MemberData(nameof(ValidDataset))]
    public virtual void RoundTrip_ValidValue_ReturnsExpectedValue(JsonConverterTestData<T> valid)
    {
        string json = Converter.Write(valid.Value, SerializerOptions);
        T? value = Converter.Read(json, SerializerOptions);
        Assert.Equal(valid.Value, value, comparer: Comparer);
    }
}
