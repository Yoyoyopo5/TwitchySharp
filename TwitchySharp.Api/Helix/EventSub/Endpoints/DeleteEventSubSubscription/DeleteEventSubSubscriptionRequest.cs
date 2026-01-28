using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
/// <para>
/// When using the <see cref="DeleteEventSubSubscriptionRequest(EventSubSubscription)"/> constructor or setting the <see cref="Subscription"/>, 
/// the identity for the request introspected from the <see cref="Subscription"/>. It does not need to be manually configured unless you need to
/// manually configure the <see cref="ClientIdentity"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-eventsub-subscription">Delete EventSub Subscription</see> for more information.
/// </remarks>
public record DeleteEventSubSubscriptionRequest()
    : TwitchHelixRequest<DeleteEventSubSubscriptionResponse>
{
    protected override string Path => "/eventsub/subscriptions";
    public override HttpMethod Method => HttpMethod.Delete;
    protected override TwitchApiIdentity DefaultIdentity => Subscription switch
    {
        not null => Subscription.RequiresUserAccessToken() switch
        {
            true => Subscription.GetAuthorizingUser(),
            _ => null
        },
        _ => null
    } ?? TwitchApiIdentity.Default;
    public override IEnumerable<Scope> ValidScopes => [];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("id", SubscriptionId);

    /// <summary>
    /// The subscription to delete.
    /// </summary>
    /// <remarks>
    /// Auto-sets <see cref="SubscriptionId"/> to support <see langword="with"/> syntax.
    /// For first time initialization use the constructor.
    /// </remarks>
    public EventSubSubscription? Subscription
    {
        get => field;
        init
        {
            field = value;
            if (value is not null)
                SubscriptionId = value.Id;
        }
    }
    /// <summary>
    /// Automatically sets the required <see cref="SubscriptionId"/>.
    /// </summary>
    /// <remarks>
    /// You should use this in most cases, as it will set the correct <see cref="TwitchApiIdentity"/> to use with the request.
    /// If you only set <see cref="SubscriptionId"/>, the default identity is <see cref="TwitchApiIdentity.Default"/>.
    /// </remarks>
    /// <param name="subscription">The subscription to delete.</param>
    [SetsRequiredMembers]
    public DeleteEventSubSubscriptionRequest(EventSubSubscription subscription)
        : this()
        => (Subscription, SubscriptionId) = (subscription, subscription.Id);

    /// <summary>
    /// The id of the subscription to delete.
    /// </summary>
    public required EventSubSubscriptionId SubscriptionId { get; set; }
}
