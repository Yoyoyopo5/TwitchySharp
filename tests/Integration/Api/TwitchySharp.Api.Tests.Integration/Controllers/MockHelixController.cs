using System.Collections.Immutable;
using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Tests.Integration.Controllers;

public readonly record struct BearerToken(string Value);

public class HelixControllerOptions
{
    public required ClientId ValidClientId { get; set; }
    public required BearerToken ValidBearerToken { get; set; }
    public required TwitchRateLimitDetails RateLimitDetails { get; set; }
}

[ApiController]
[Route("helix")]
public class MockHelixController(HelixControllerOptions options) : ControllerBase
{
    private readonly HelixControllerOptions _options = options;

    /// <summary>
    /// Validates that required Twitch authorization headers are present.
    /// </summary>
    /// <returns>An error result if validation fails, null if validation passes.</returns>
    private Validation ValidateAuthHeaders()
        => new Validation<IHeaderDictionary>(Request.Headers)
            .HasClientId(_options.ValidClientId)
            .HasAuthorization(_options.ValidBearerToken)
            .Bind(h => new Validation());

    /// <summary>
    /// Adds Twitch rate limit headers to the response.
    /// </summary>
    private void AddRateLimitHeaders()
    {
        Response.Headers["Ratelimit-Limit"] = _options.RateLimitDetails.Limit.ToString();
        Response.Headers["Ratelimit-Remaining"] = _options.RateLimitDetails.Remaining.ToString();
        Response.Headers["Ratelimit-Reset"] = _options.RateLimitDetails.Reset?.ToUnixTimeSeconds().ToString();
    }

    [HttpPost("eventsub/subscriptions")]
    public IActionResult CreateEventSubSubscription([FromBody] CreateEventSubSubscriptionData body)
        => ValidateAuthHeaders().Match(
            e => (e as HttpError)!.ToResult(),
            () =>
            {
                AddRateLimitHeaders();
                return Accepted(new
                {
                    data = new[]
                    {
                        new
                        {
                            id = "12345",
                            status = "enabled",
                            type = body.Type.ToString(),
                            version = body.Version.ToString(),
                            condition = body.Condition,
                            created_at = DateTimeOffset.UtcNow,
                            transport = new
                            {
                                method = body.Transport.Method.ToString(),
                                callback = body.Transport.Callback?.ToString(),
                                session_id = body.Transport.SessionId?.ToString(),
                                connected_at = (DateTimeOffset?)(body.Transport.SessionId is not null ? DateTimeOffset.UtcNow : null),
                                conduit_id = body.Transport.ConduitId?.ToString()
                            },
                            cost = 1,
                        }
                    },
                    total = 1,
                    total_cost = 1,
                    max_total_cost = 10000
                });
            }
            );

    [HttpDelete("eventsub/subscriptions")]
    public IActionResult DeleteEventSubSubscription(
        [FromQuery(Name = "id"), BindRequired] string id
        )
        => ValidateAuthHeaders().Match<IActionResult>(
            e => (e as HttpError)!.ToResult(),
            () =>
            {
                AddRateLimitHeaders();
                return NoContent();
            });

    [HttpGet("eventsub/subscriptions")]
    public IActionResult GetEventSubSubscriptions(
        [FromQuery] string? status,
        [FromQuery] string? type,
        [FromQuery(Name = "user_id")] string? userId,
        [FromQuery(Name = "subscription_id")] string? subscriptionId,
        [FromQuery(Name = "conduit_id")] string? conduitId,
        [FromQuery(Name = "after")] string? after
        )
        => ValidateAuthHeaders().Match(
            e => (e as HttpError)!.ToResult(),
            () =>
            {
                AddRateLimitHeaders();
                return Ok(new
                {
                    data = new[]
                    {
                        new
                        {
                            id = "12345",
                            status = "enabled",
                            type = "fake.subscription",
                            version = "1",
                            condition = new
                            {
                                user_id = "82901",
                                boradcaster_user_id = "12372"
                            },
                            created_at = DateTime.UtcNow.ToRfc3339(),
                            transport = new
                            {
                                method = "webhook",
                                callback = "https://fake-callback.com"
                            },
                            cost = 1
                        }
                    },
                    total_cost = 1,
                    max_total_cost = 10000,
                    pagination = new
                    {
                        cursor = "1382"
                    }
                });
            }
            );

    /// <summary>
    /// POST /helix/moderation/warnings - Warn Chat User
    /// </summary>
    [HttpPost("moderation/warnings")]
    public IActionResult WarnChatUser(
        [FromQuery(Name = "broadcaster_id"), BindRequired] string broadcasterId,
        [FromQuery(Name = "moderator_id"), BindRequired] string moderatorId,
        [FromBody] WarnChatUserRequestBody body)
        => ValidateAuthHeaders().Match(
            e => (e as HttpError)!.ToResult(),
            () =>
            {
                AddRateLimitHeaders();
                return Ok(new
                {
                    data = new[]
                    {
                        new
                        {
                            broadcaster_id = broadcasterId,
                            moderator_id = moderatorId,
                            user_id = body.Data.UserId,
                            reason = body.Data.Reason
                        }
                    }
                });
            }
            );

    /// <summary>
    /// POST /helix/channels/vips - Add Channel VIP
    /// </summary>
    [HttpPost("channels/vips")]
    public IActionResult AddChannelVip(
        [FromQuery(Name = "broadcaster_id"), BindRequired] string broadcasterId,
        [FromQuery(Name = "user_id"), BindRequired] string userId)
        => ValidateAuthHeaders()
            .Match<IActionResult>(
            e => (e as HttpError)!.ToResult(),
            () =>
            {
                AddRateLimitHeaders();
                return NoContent();
            });
}

public record WarnChatUserRequestBody
{
    [JsonPropertyName("data")]
    public required WarnChatUserData Data { get; init; }
}

public record WarnChatUserData
{
    [JsonPropertyName("user_id")]
    public required string UserId { get; init; }

    [JsonPropertyName("reason")]
    public required string Reason { get; init; }
}

public record CreateEventSubSubscriptionData
{
    [JsonPropertyName("type")]
    public required EventSubSubscriptionTypeName Type { get; init; }
    [JsonPropertyName("version")]
    public required EventSubSubscriptionTypeVersion Version { get; init; }
    [JsonPropertyName("condition")]
    public required ImmutableDictionary<string, object> Condition { get; init; }
    [JsonPropertyName("transport")]
    public required EventSubSubscriptionTransport Transport { get; init; }
}

public record EventSubSubscriptionTransport
{
    [JsonPropertyName("method")]
    public EventSubTransportMethod Method { get; protected init; } = new(string.Empty);
    [JsonPropertyName("callback")]
    public EventSubCallbackUrl? Callback { get; protected init; }
    [JsonPropertyName("secret")]
    public WebhookSecret? Secret { get; protected init; }
    [JsonPropertyName("session_id")]
    public EventSubWebsocketSessionId? SessionId { get; protected init; }
    [JsonPropertyName("conduit_id")]
    public ConduitId? ConduitId { get; protected init; }
}

public static class HeaderDictionaryExtensions
{
    public static Validation<ClientId> GetClientId(this Validation<IHeaderDictionary> headers)
        => headers.Bind<ClientId>(h => h.TryGetValue("Client-Id", out StringValues value) && value.FirstOrDefault() is string clientId
            ? new ClientId(clientId)
            : new HttpError("Missing Client-Id header", HttpStatusCode.BadRequest));

    public static Validation<BearerToken> GetBearerToken(this Validation<IHeaderDictionary> headers)
        => headers.Bind<BearerToken>(h => h.TryGetValue("Authorization", out StringValues value) && value.FirstOrDefault() is string token
            ? new BearerToken(token.Replace("Bearer", string.Empty).TrimStart())
            : new HttpError("Missing Authorization header", HttpStatusCode.BadRequest));

    public static Validation<IHeaderDictionary> HasClientId(this Validation<IHeaderDictionary> headers, ClientId expected)
        => headers.GetClientId().Bind(id => id == expected
            ? headers
            : new HttpError("Unauthorized Client-Id", HttpStatusCode.Unauthorized));

    public static Validation<IHeaderDictionary> HasAuthorization(this Validation<IHeaderDictionary> headers, BearerToken expected)
        => headers.GetBearerToken().Bind(token => token == expected
            ? headers
            : new HttpError("Invalid Authorization", HttpStatusCode.Unauthorized));
}

public static class HttpErrorExtensions
{
    public static ObjectResult ToResult(this HttpError error)
        => error switch
        {
            { Status: HttpStatusCode.BadRequest } badRequest => new BadRequestObjectResult(badRequest.Message),
            { Status: HttpStatusCode.Unauthorized } unauthorized => new UnauthorizedObjectResult(unauthorized.Message),
            _ => new ObjectResult(null) { StatusCode = 500 }
        };
}
