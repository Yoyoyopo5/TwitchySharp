using System.Text;
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
            byte[] content = await response.Content.ReadAsByteArrayAsync(ct);
            ByteArrayContent replace = new(content);
            foreach ((string key, IEnumerable<string> value) in response.Content.Headers)
            {
                replace.Headers.TryAddWithoutValidation(
                    key, value);
            }
            response.Content = replace;
            responseContent = Encoding.UTF8.GetString(content);
        }

        TestContext.Current.AddAttachment($"http-{request.Method}-{request.RequestUri?.AbsolutePath.Replace('/', '_')}-{Guid.CreateVersion7()}", $$"""
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
