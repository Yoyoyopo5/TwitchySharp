using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

public static class TwitchWebhooksRouteExtensions
{
    public static IEndpointConventionBuilder MapTwitchWebhooks(this IEndpointRouteBuilder endpoints, string pattern)
        => endpoints.MapPost(pattern, (HandleAspNetWebhookRequest process, HttpContext context, CancellationToken ct = default) => process(context, ct));
}
