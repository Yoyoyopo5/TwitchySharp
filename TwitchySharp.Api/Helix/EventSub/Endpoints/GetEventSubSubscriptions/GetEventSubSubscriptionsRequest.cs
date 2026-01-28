using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub;
/// <summary>
/// Gets a list of EventSub subscriptions that an app created.
/// </summary>
/// <remarks>
/// <para>
/// By default, this uses the <see cref="TwitchApiIdentity.Default"/> and will only get subscriptions using <see cref="WebhookSubscriptionTransport"/> and <see cref="ConduitSubscriptionTransport"/>.
/// To get subscriptions using <see cref="WebsocketSubscriptionTransport"/>, call the <see cref="ForWebsocketSubscriptions(UserIdentity)"/> method with an explicit <see cref="UserIdentity"/>.
/// </para>
/// If using <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/>, requires an app access token.
/// If using <see cref="WebsocketSubscriptionTransport"/>, requires a user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-eventsub-subscriptions">Get EventSub Subscriptions</see> for more information.
/// </remarks>
public record GetEventSubSubscriptionsRequest
    : TwitchHelixRequest<GetEventSubSubscriptionsResponse>, IPageableRequest
{
    protected override string Path => "/eventsub/subscriptions";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => TwitchApiIdentity.Default;
    public override IEnumerable<Scope> ValidScopes => [];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("status", Status?.Value)
            .Add("type", Type?.ToString())
            .Add("user_id", UserId)
            .Add("subscription_id", SubscriptionId)
            .Add("after", After?.ToString());

    /// <summary>
    /// Configures the request to query for subscriptions using the <see cref="EventSubTransportMethod.Websocket"/>.
    /// </summary>
    /// <remarks>
    /// This requires a user access token, so you must pass a <see cref="UserIdentity"/> to use for the request.
    /// </remarks>
    /// <param name="user">The <see cref="UserIdentity"/> to make the request as.</param>
    /// <returns>A new <see cref="GetEventSubSubscriptionsRequest"/> configured to get subscriptions using <see cref="EventSubTransportMethod.Websocket"/>.</returns>
    public GetEventSubSubscriptionsRequest ForWebsocketSubscriptions(UserIdentity user)
        => this with { Identity = user };

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

    /// <inheritdoc/>
    public PaginationCursor? After { get; set; }
}