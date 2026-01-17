using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.ResponseConverters;
/// <summary>
/// Converts response content by deserializing JSON content into a TResponseContent instance.
/// </summary>
/// <param name="serializerOptions">The serializer options to use when deserializing JSON content.</param>
public class JsonResponseConverter(JsonSerializerOptions? serializerOptions) : IConvertResponseContent
{
    public async ValueTask<TResponse> Convert<TResponse>(HttpResponseMessage httpResponse, CancellationToken ct = default)
        => await JsonSerializer.DeserializeAsync<TResponse>(httpResponse.Content.ReadAsStream(ct), serializerOptions, ct).ConfigureAwait(false) switch
        {
            TResponse deserialized => deserialized,
            _ => throw new NotSupportedException("This converter does not support null literal responses.")
        };
}
