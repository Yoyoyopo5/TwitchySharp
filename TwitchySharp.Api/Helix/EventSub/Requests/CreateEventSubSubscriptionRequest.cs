using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub;
/// <summary>
/// Creates an EventSub subscription.
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-eventsub-subscription">create eventsub subscription</see> for more information.
/// </summary>
/// <remarks>
/// If you use the <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/>, the <paramref name="accessToken"/> must be an app access token. 
/// If the subscription type requires a user access token authorization, the user must have created a user access token with the required <see cref="Scope"/> for this application (i.e., for this application's client id). 
/// It is not required to send this user access token in the request.
/// <br/>
/// If you use the <see cref="WebsocketSubscriptionTransport"/>, the <paramref name="accessToken"/> must be a user access token with the required <see cref="Scope"/> for the subscription type.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">
/// If using <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/>, an app access token.
/// If using <see cref="WebsocketSubscriptionTransport"/>, a user access token with the subscription type's required <see cref="Scope"/>.
/// </param>
/// <param name="subscription">The subscription to create.</param>
public class CreateEventSubSubscriptionRequest(string clientId, string accessToken, CreateEventSubSubscriptionRequestData subscription)
    : HelixApiRequest<CreateEventSubSubscriptionResponse, CreateEventSubSubscriptionRequestData>(
        "/eventsub/subscriptions",
        clientId,
        accessToken,
        subscription
        );

public record CreateEventSubSubscriptionRequestData
{
    /// <summary>
    /// The type of subscription to create. 
    /// See the <see cref="TwitchySharp.Api.Helix.EventSub.Types"/> namespace for built-in subscription types.
    /// </summary>
    [JsonIgnore]
    public required IEventSubSubscriptionType Type { get; set; }
    [JsonInclude, JsonPropertyName("type")]
    private string _type => Type.Type;
    [JsonInclude, JsonPropertyName("version")]
    private string _version => Type.Version;
    [JsonInclude, JsonPropertyName("condition")]
    private IReadOnlyDictionary<string, object> _condition => Type.Condition;
    /// <summary>
    /// The transport type that you want Twitch to use when sending you notifications.
    /// Possible transport types are <see cref="WebhookSubscriptionTransport"/>, <see cref="WebsocketSubscriptionTransport"/>, and <see cref="ConduitSubscriptionTransport"/>.
    /// </summary>
    public required NewEventSubSubscriptionTransport Transport { get; set; }
    [JsonInclude, JsonPropertyName("is_batching_enabled")]
    private bool? _isBatchingEnabled => _type switch // Kind of jank but this is the only type that requires this. Adding to IEventSubSubscription type will be more mantainable.
    {
        EventSubSubscriptionTypeNames.DROP_ENTITLEMENT_GRANT => true,
        _ => null
    };
}
