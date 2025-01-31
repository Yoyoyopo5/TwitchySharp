using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;

namespace TwitchySharp.Api.ApiResponseConverters;

internal class JsonResponseConverter(JsonSerializerOptions? serializerOptions) : IConvertApiResponse
{
    public async ValueTask<TResponse> Convert<TResponse>(HttpResponseMessage httpResponse, CancellationToken ct = default)
        => await JsonSerializer.DeserializeAsync<TResponse>(httpResponse.Content.ReadAsStream(), serializerOptions, ct).ConfigureAwait(false) switch
        {
            TResponse deserialized => deserialized,
            _ => throw new NotSupportedException("This converter does not support null literal responses.")
        };
}
