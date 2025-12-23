using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Webhooks.MessageVerifiers;
using TwitchySharp.EventSub.Webhooks.Responses;
using TwitchySharp.EventSub.Webhooks.WebhookMessageProcessors;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

public static class TwitchWebhooksRouteExtensions
{
    public static IEndpointConventionBuilder MapTwitchWebhooks(this IEndpointRouteBuilder endpoints, string pattern)
    {
        return endpoints.MapPost(pattern, async (HttpContext context, ILoggerFactory? loggerFactory = null, CancellationToken ct = default) =>
        {
            context.Request.EnableBuffering();
            context.Request.Body.Position = 0;

            // Header Conversion
            ITwitchWebhooksHeaderConverter headerConverter = context.RequestServices.GetRequiredService<ITwitchWebhooksHeaderConverter>();
            var headerConversionResult = headerConverter.Convert(context.Request.Headers);
            if (headerConversionResult.MissingHeaders.Any())
                return Results.BadRequest($"Missing required headers: {string.Join(", ", headerConversionResult.MissingHeaders)}");

            // Validation
            if (context.RequestServices.GetService<ITwitchWebhookMessageVerifier>() is ITwitchWebhookMessageVerifier verifier)
                if (!await verifier.IsValid(headerConversionResult.ConvertedHeader, context.Request.Body, ct))
                    return Results.Unauthorized();

            // Process
            IEventSubWebhookMessageProcessor processor = context.RequestServices.GetRequiredService<IEventSubWebhookMessageProcessor>();
            var responseData = await processor.HandleRequest(headerConversionResult.ConvertedHeader, context.Request.Body, ct);

            // Respond
            return responseData switch
            {
                CallbackVerificationResponseData { StatusCode: 200 } callbackVerificationResponse => Results.Content(callbackVerificationResponse.Challenge, "text/plain", Encoding.UTF8, callbackVerificationResponse.StatusCode),
                _ => Results.StatusCode(responseData.StatusCode)
            };
        });
    }
}
