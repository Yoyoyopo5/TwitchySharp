using System.Text.Json;

namespace TwitchySharp.Api.Tests.E2E;

internal class ResponseRecorder : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        string? requestContent = null;
        if (request.Content is not null)
        {
            using StreamReader sr = new(request.Content.ReadAsStream(ct));
            requestContent = await sr.ReadToEndAsync(ct);
        }

        HttpResponseMessage response = await base.SendAsync(request, ct);

        string? responseContent = null;
        if (response.Content is not null)
        {
            await response.Content.LoadIntoBufferAsync(ct);
            using StreamReader sr = new(await response.Content.ReadAsStreamAsync(ct));
            responseContent = await sr.ReadToEndAsync(ct);
        }

        TestContext.Current.AddAttachment($"http-{request.Method}-{request.RequestUri?.AbsolutePath.Replace('/', '_')}", $$"""
            {
                "request": {
                    "url": {{request.RequestUri?.AbsoluteUri}},
                    "method": {{request.Method}},
                    "headers": {{JsonSerializer.Serialize(request.Headers.ToDictionary(h => h.Key, h => string.Equals(h.Key, "authorization", StringComparison.OrdinalIgnoreCase) ? ["REDACTED"] : h.Value.ToArray()))}},
                    "content": {{requestContent}}
                },
                "response": {
                    "status": {{response.StatusCode}},
                    "headers": {{JsonSerializer.Serialize(response.Headers.ToDictionary(h => h.Key, h => h.Value.ToArray()))}},
                    "content": {{responseContent}}
                }
            }
            """
            );

        return response;
    }
}
