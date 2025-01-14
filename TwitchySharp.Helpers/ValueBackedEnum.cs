using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Helpers;
/// <summary>
/// Allows for simple creation of hardcoded sets of values that can be discovered via intellisense.
/// </summary>
public class ValueBackedEnum<T>
{
    public T Value { get; private set; }
    protected ValueBackedEnum(T value)
    {
        Value = value;
    }
    public static implicit operator T(ValueBackedEnum<T> a) => a.Value;
}

/// <summary>
/// Used to serialize classes based on <see cref="ValueBackedEnum{T}"/>
/// </summary>
/// <typeparam name="TValueBackedEnum"></typeparam>
/// <typeparam name="T"></typeparam>
public class ValueBackedEnumJsonConverter<TValueBackedEnum, T> : JsonConverter<TValueBackedEnum>
    where TValueBackedEnum : ValueBackedEnum<T>
{
    public override TValueBackedEnum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return (TValueBackedEnum?)Activator.CreateInstance(typeToConvert, JsonSerializer.Deserialize<T>(ref reader, options));
    }

    public override void Write(Utf8JsonWriter writer, TValueBackedEnum value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize<T>(writer, value.Value, options);
    }
}
