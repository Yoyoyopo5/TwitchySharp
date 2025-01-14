using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Tests.Unit.JsonConverters;
/// <summary>
/// <see href="https://khalidabuhakmeh.com/systemtextjson-jsonconverter-test-helpers">Source</see>
/// </summary>
public static class JsonConverterTestExtensions
{
    public static TResult? Read<TResult>(
        this JsonConverter<TResult> converter,
        string token,
        JsonSerializerOptions? options = null)
    {
        options ??= JsonSerializerOptions.Default;
        var bytes = Encoding.UTF8.GetBytes(token);
        var reader = new Utf8JsonReader(bytes);
        // advance to token
        reader.Read();
        var result = converter.Read(ref reader, typeof(TResult), options);
        // did we get the result?
        return result;
    }

    public static (bool IsSuccessful, TResult? Result) TryRead<TResult>(
        this JsonConverter<TResult> converter,
        string token,
        JsonSerializerOptions? options = null)
    {
        try
        {
            var result = Read(converter, token, options);
            return (true, result);
        }
        catch (Exception)
        {
            return (IsSuccessful: false, Result: default);
        }
    }

    public static string Write<T>(
        this JsonConverter<T> converter,
        T value,
        JsonSerializerOptions? options = null)
    {
        options ??= JsonSerializerOptions.Default;
        using var ms = new MemoryStream();
        using var writer = new Utf8JsonWriter(ms);
        converter.Write(writer, value, options);
        writer.Flush();
        var result = Encoding.UTF8.GetString(ms.ToArray());
        return result;
    }

    public static (bool IsSuccessful, string? Result) TryWrite<T>(
        this JsonConverter<T> converter,
        T value,
        JsonSerializerOptions? options = null)
    {
        try
        {
            var result = Write(converter, value, options);
            return (true, result);
        }
        catch
        {
            return (false, null);
        }
    }
}
