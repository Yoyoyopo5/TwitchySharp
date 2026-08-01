using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

public static class TwitchWebhooksRouteExtensions
{
    /// <summary>
    /// Map an endpoint for Twitch EventSub Webhooks requests.
    /// </summary>
    /// <param name="endpoints">The endpoint route builder.</param>
    /// <param name="pattern">The endpoint path.</param>
    /// <returns>The endpoint route builder.</returns>
    public static IEndpointConventionBuilder MapTwitchWebhooks(this IEndpointRouteBuilder endpoints, string pattern)
        => endpoints.MapPost(pattern, (HandleAspNetWebhookRequest process, HttpContext context, CancellationToken ct = default) => process(context, ct));
}
