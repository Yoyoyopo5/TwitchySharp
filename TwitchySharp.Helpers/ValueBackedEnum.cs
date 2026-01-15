using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Helpers;
/// <summary>
/// Allows for simple creation of hardcoded sets of values that can be discovered via intellisense.
/// </summary>
public record ValueBackedEnum<T>
{
    public T Value { get; private set; }
    protected ValueBackedEnum(T value)
    {
        Value = value;
    }
    public static implicit operator T(ValueBackedEnum<T> a) => a.Value;
    public sealed override string ToString() => Value?.ToString() ?? string.Empty;
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
        JsonSerializer.Serialize(writer, value.Value, options);
    }
}

/// <summary>
/// Creates a <see cref="ValueBackedEnumJsonConverter{TValueBackedEnum, T}"/> for types inheriting from <see cref="ValueBackedEnum{T}"/>.
/// </summary>
/// <remarks>
/// Register this factory in <see cref="JsonSerializerOptions"/> to support deserialization of <see cref="ValueBackedEnum{T}"/> derived types without attributes.
/// </remarks>
public class ValueBackedEnumConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
        => GetValueBackedEnumType(typeToConvert) != null;

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        if (GetValueBackedEnumType(typeToConvert) is not Type baseType)
            return null;
        Type tValue = baseType.GetGenericArguments()[0];
        Type converterType = typeof(ValueBackedEnumJsonConverter<,>).MakeGenericType(typeToConvert, tValue);
        return (JsonConverter?)Activator.CreateInstance(converterType);
    }

    private static Type? GetValueBackedEnumType(Type type)
    {
        // Walk up the inheritance chain to find ValueBackedEnum<T>
        while (type != null && type != typeof(object))
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ValueBackedEnum<>))
            {
                return type;
            }
            type = type.BaseType!;
        }
        return null;
    }
}
