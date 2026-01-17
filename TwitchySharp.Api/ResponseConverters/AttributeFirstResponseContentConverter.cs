using System;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.ResponseConverters;
/// <summary>
/// Looks for the <see cref="ApiConverterAttribute"/> on a response content type to find a specific response content converter,
/// or defaults to the <paramref name="defaultConverter"/> if the attribute is not present.
/// </summary>
/// <param name="defaultConverter">The default response converter to use if an <see cref="ApiConverterAttribute"/> is not present on the type.</param>
public class AttributeFirstResponseContentConverter(IConvertResponseContent defaultConverter)
    : IConvertResponseContent
{
    private readonly IConvertResponseContent _defaultConverter = defaultConverter;

    public ValueTask<TResponseContent> Convert<TResponseContent>(HttpResponseMessage httpResponse, CancellationToken ct = default)
        => (GetConverterByAttribute<TResponseContent>() ?? _defaultConverter).Convert<TResponseContent>(httpResponse, ct);

    private static IConvertResponseContent? GetConverterByAttribute<TResponseContent>()
        => typeof(TResponseContent).GetCustomAttribute<ApiConverterAttribute>()?.CreateConverter();
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public class ApiConverterAttribute(Type converterType, object[]? args = null) : Attribute
{
    public IConvertResponseContent? CreateConverter()
        => Activator.CreateInstance(converterType, args) as IConvertResponseContent;
}