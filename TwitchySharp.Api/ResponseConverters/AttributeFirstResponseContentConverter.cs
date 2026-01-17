using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.ResponseConverters;

public class AttributeFirstResponseContentConverter(IConvertResponseContent defaultConverter)
    : IConvertResponseContent
{
    private readonly IConvertResponseContent _defaultConverter = defaultConverter;

    public ValueTask<TResponseContent> Convert<TResponseContent>(HttpResponseMessage httpResponse, CancellationToken ct = default)
        => (GetConverterByAttribute<TResponseContent>() ?? _defaultConverter).Convert<TResponseContent>(httpResponse, ct);

    private static IConvertResponseContent? GetConverterByAttribute<TResponseContent>()
        => typeof(TResponseContent).GetCustomAttribute<ApiConverterAttribute>()?.CreateConverter();
}