using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Helpers;

/// <summary>
/// Implemented automatically by source generator when using <see cref="WrapperAttribute{T}"/>.
/// </summary>
public interface IWrapValue<T, out TWrapper>
{
    T Value { get; }
    static abstract TWrapper Create(T value);
}

/// <summary>
/// Indicates the type is a wrapper around another type.
/// </summary>
/// <remarks>
/// This informs the source generator to generate wrapper members for the type and add a JsonConverter.
/// Mark the type with <see langword="partial"/> to use.
/// </remarks>
/// <typeparam name="T">The type to wrap.</typeparam>
[AttributeUsage(validOn: AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class WrapperAttribute<T> : Attribute;

/// <summary>
/// Used to serialize basic wrapper classes and structs that implement <see cref="IWrapValue{T, TWrapper}"/>.
/// </summary>
public class WrapperJsonConverter<TWrapper, TWrapped> : JsonConverter<TWrapper>
    where TWrapper : IWrapValue<TWrapped, TWrapper>
{
    public override TWrapper? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => JsonSerializer.Deserialize<TWrapped>(ref reader, options) switch
        {
            { } value => TWrapper.Create(value),
            _ => default
        };

    public override void Write(Utf8JsonWriter writer, TWrapper value, JsonSerializerOptions options)
        => JsonSerializer.Serialize(writer, value.Value, options);
}
