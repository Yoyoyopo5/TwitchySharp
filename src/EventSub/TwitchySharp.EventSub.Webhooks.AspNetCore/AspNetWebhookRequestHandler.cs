using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TwitchySharp.EventSub.Webhooks.Functional;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

internal delegate ValueTask<IResult> HandleAspNetWebhookRequest(HttpContext context, CancellationToken ct);

internal static class AspNetWebhookRequestHandler
{
    private const string LOGGER_CATEGORY = "TwitchySharp.EventSub.Webhooks.AspNetCore";
    public static HandleAspNetWebhookRequest Create(ReadWebhookHeader readHeader, ProcessWebhookRequest processRequest, ILoggerFactory? loggerFactory = null)
        => (context, ct) => readHeader(context.Request.Headers).Match<ValueTask<IResult>>(
                onError: e =>
                {
                    ILogger? logger = loggerFactory?.CreateLogger(LOGGER_CATEGORY);
                    if (e is EventSubWebhookHeaderReader.MissingHeadersError missingHeadersError
                        && (logger?.IsEnabled(LogLevel.Debug) ?? false))
                        logger.LogDebug("Request was missing required headers: {MissingHeaders}", string.Join(", ", missingHeadersError.Headers));
#if DEBUG
                    return ValueTask.FromResult(Results.BadRequest());
#else
                    return ValueTask.FromResult(Results.Ok());
#endif
                },
                onValid: async header => (await processRequest(new()
                {
                    Header = header,
                    Content = new(context.Request.Body)
                }, ct)).ToResult()
                );
}
