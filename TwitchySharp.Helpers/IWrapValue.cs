using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Helpers;

/// <summary>
/// Wraps a single value.
/// </summary>
/// <typeparam name="T">The wrapped type.</typeparam>
public interface IWrapValue<T>
{
    T Value { get; }
}

/// <summary>
/// Used to serialize basic wrapper classes and structs that implement <see cref="IWrapValue{T}"/>.
/// </summary>
public class WrapperJsonConverter<TWrapper, TWrapped> : JsonConverter<TWrapper>
    where TWrapper : IWrapValue<TWrapped>
{
    public override TWrapper? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return (TWrapper?)Activator.CreateInstance(typeToConvert, JsonSerializer.Deserialize<TWrapped>(ref reader, options));
    }

    public override void Write(Utf8JsonWriter writer, TWrapper value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Value, options);
    }
}
