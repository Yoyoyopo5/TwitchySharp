using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub;
/// <summary>
/// Gets a list of EventSub subscriptions that an app created.
/// </summary>
/// <remarks>
/// If using <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/>, requires an app access token.
/// If using <see cref="WebsocketSubscriptionTransport"/>, requires a user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-eventsub-subscriptions">Get EventSub Subscriptions</see> for more information.
/// </remarks>
public record GetEventSubSubscriptionsRequest
    : TwitchHelixRequest<GetEventSubSubscriptionsResponse>
{
    /// <param name="clientId">The client id of the application to get EventSub subscriptions for.</param>
    /// <param name="accessToken">
    /// If using <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/>, an app access token.
    /// If using <see cref="WebsocketSubscriptionTransport"/>, a user access token.
    /// </param>
    /// <param name="parameters">The request parameters.</param>
    public GetEventSubSubscriptionsRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetEventSubSubscriptionsRequestParameters? parameters = null
        )
        : base(
            "/eventsub/subscriptions",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("status", parameters?.Status?.Value)
                .Add("type", parameters?.Type)
                .Add("user_id", parameters?.UserId)
                .Add("subscription_id", parameters?.SubscriptionId)
                .Add("after", parameters?.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetEventSubSubscriptionsRequest"/>.
/// </summary>
public record GetEventSubSubscriptionsRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// Specify this parameter to filter the returned list by subscription status.
    /// </summary>
    public EventSubSubscriptionStatus? Status { get; set; }
    /// <summary>
    /// Specify this parameter to filter the returned list by subscription type.
    /// </summary>
    /// <remarks>
    /// Note that this only filters by subscription type <b>name</b>, not version.
    /// </remarks>
    public EventSubSubscriptionTypeName? Type { get; set; }
    /// <summary>
    /// Specify this parameter to filter the returned list by a specific user. 
    /// </summary>
    /// <remarks>
    /// Only subscriptions that were created for this user are returned.
    /// </remarks>
    public UserId? UserId { get; set; }
    /// <summary>
    /// Specify this parameter to get a specific subscription by its id, as long as the subscription is owned by the client making the request.
    /// </summary>
    /// <remarks>
    /// If a matching subscription does not exist, an empty array is returned.
    /// </remarks>
    public EventSubSubscriptionId? SubscriptionId { get; set; }
    /// <summary>
    /// Unused for this request type.
    /// </summary>
    public PaginationAmount? First { get; set; }
    public PaginationCursor? After { get; set; }
}