using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TwitchySharp.EventSub.Webhooks.MessageVerifiers;
using TwitchySharp.EventSub.Webhooks.Responses;
using TwitchySharp.EventSub.Webhooks.WebhookMessageProcessors;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

public static class TwitchWebhooksRouteExtensions
{
    private const string LOGGER_CATEGORY = LoggingConfig.LOGGER_CATEGORY + ".MapTwitchWebhooks";
    public static IEndpointConventionBuilder MapTwitchWebhooks(this IEndpointRouteBuilder endpoints, string pattern)
        => endpoints.MapPost(pattern, async (HttpContext context, ILoggerFactory? loggerFactory = null, CancellationToken ct = default) =>
            {
                ILogger? logger = loggerFactory?.CreateLogger(LOGGER_CATEGORY);

                context.Request.EnableBuffering();
                context.Request.Body.Position = 0;

                // Header Conversion
                ITwitchWebhooksHeaderConverter headerConverter = context.RequestServices.GetRequiredService<ITwitchWebhooksHeaderConverter>();
                var headerConversionResult = headerConverter.Convert(context.Request.Headers);
                logger?.LogTrace("Converted headers: {HeaderConversionResult}", headerConversionResult);
                if (headerConversionResult.MissingHeaders.Any())
                {
                    string missingHeadersList = string.Join(", ", headerConversionResult.MissingHeaders);
                    logger?.LogDebug("Missing required headers: {MissingHeaders}", missingHeadersList);
                    return Results.BadRequest($"Missing required headers: {missingHeadersList}");
                }

                using IDisposable? loggingScope = logger?.BeginScope(new Dictionary<string, object?>
                {
                    ["WebhookHeaders"] = headerConversionResult.ConvertedHeader,
                    ["X-Forwarded-For"] = context.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                });
                logger?.LogTrace("Received webhook request.");

                // Validation
                try
                {
                    if (context.RequestServices.GetService<ITwitchWebhookMessageVerifier>() is ITwitchWebhookMessageVerifier verifier)
                    {
                        if (!await verifier.IsValid(headerConversionResult.ConvertedHeader, context.Request.Body, ct))
                        {
                            logger?.LogWarning("Verification failed for webhook message.");
                            return Results.Unauthorized();
                        }
                        logger?.LogTrace("Verification success for webhook message.");
                    }
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex, "An error occurred during message verification.");
#if DEBUG
                    return Results.StatusCode(500);
#else
                    return Results.Ok(); // We return 200 in release mode to avoid Twitch revoking the webhook subscription due to internal errors.
#endif
                }
                finally
                {
                    context.Request.Body.Position = 0;
                }

                // Process
                WebhookResponseData responseData = default!;
                try
                {
                    IEventSubWebhookMessageProcessor processor = context.RequestServices.GetRequiredService<IEventSubWebhookMessageProcessor>();
                    responseData = await processor.HandleRequest(headerConversionResult.ConvertedHeader, context.Request.Body, ct);
                    logger?.LogTrace("Processed webhook message.");
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex, "An error occurred during message processing.");
#if DEBUG
                    return Results.StatusCode(500);
#else
                    return Results.Ok();
#endif
                }

                // Respond
                return responseData switch
                {
                    CallbackVerificationResponseData { StatusCode: 200 } callbackVerificationResponse => Results.Content(callbackVerificationResponse.Challenge, "text/plain", Encoding.UTF8, callbackVerificationResponse.StatusCode),
                    _ => Results.StatusCode(responseData.StatusCode)
                };
            });
}
