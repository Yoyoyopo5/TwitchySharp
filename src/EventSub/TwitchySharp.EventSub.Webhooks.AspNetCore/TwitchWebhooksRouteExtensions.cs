using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using TwitchySharp.EventSub.Webhooks.WebhookMessageProcessors;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

public static class TwitchWebhooksRouteExtensions
{
    private const string LOGGER_CATEGORY = LoggingConfig.LOGGER_CATEGORY + ".EventSub.Webhooks.AspNetCore";
    public static IEndpointConventionBuilder MapTwitchWebhooks(this IEndpointRouteBuilder endpoints, string pattern)
        => endpoints.MapPost(
            pattern,
            async (ReadWebhookHeader readHeader, ProcessWebhookRequest process, HttpContext context, ILoggerFactory? loggerFactory = null, CancellationToken ct = default) =>
                readHeader(context.Request.Headers).Match<ValueTask<IResult>>(
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
                    onValid: async header => (await process(new()
                    {
                        Header = header,
                        Content = new(context.Request.Body)
                    }, ct)).ToResult()
                    ));
}
