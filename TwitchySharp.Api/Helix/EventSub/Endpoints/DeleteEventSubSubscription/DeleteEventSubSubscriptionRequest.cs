using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub;
/// <summary>
/// Deletes an EventSub subscription.
/// </summary>
/// <remarks>
/// <para>
/// Requires an app access token if you use the <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/> as the subscription's transport.
/// <br/>
/// Requires a user access token if you use the <see cref="WebsocketSubscriptionTransport"/> as the subscription's transport. No particular <see cref="Scope"/> is required.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-eventsub-subscription">Delete EventSub Subscription</see> for more information.
/// </remarks>
public record DeleteEventSubSubscriptionRequest
    : TwitchHelixRequest<DeleteEventSubSubscriptionResponse>
{
    protected override string Path => "/eventsub/subscriptions";
    public override HttpMethod Method => HttpMethod.Delete;
    protected override TwitchApiIdentity DefaultIdentity => TwitchApiIdentity.Default;
    public override IEnumerable<Scope> ValidScopes => [];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("id", SubscriptionId);

    /// <summary>
    /// The id of the subscription to delete.
    /// </summary>
    public required EventSubSubscriptionId SubscriptionId { get; set; }
}
