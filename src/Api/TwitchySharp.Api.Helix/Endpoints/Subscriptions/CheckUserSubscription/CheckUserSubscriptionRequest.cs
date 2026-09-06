using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Subscriptions;
/// <summary>
/// Checks whether the user subscribes to the broadcaster's channel.
/// </summary>
/// <remarks>
/// <para>
/// Prefer using <see cref="GetBroadcasterSubscriptionsRequest"/> as this endpoint throws <see cref="TwitchApiException"/>
/// with HTTP status code <c>404</c> if the user is not subscribed.
/// </para>
/// <para>
/// Requires a user access token that includes <see cref="Scope.UserReadSubscriptions"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#check-user-subscription">Check User Subscription</see> for more information.
/// </remarks>
public record CheckUserSubscriptionRequest
    : TwitchHelixRequest<CheckUserSubscriptionResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/subscriptions/user";
    public override HttpMethod Method => HttpMethod.Get;
    private UserWithScopesAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(UserId),
        ValidScopes = ImmutableHashSet.Create(Scope.UserReadSubscriptions)
    };
    public UserWithScopesAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("user_id", UserId);

    /// <summary>
    /// The user id of the broadcaster that the subscription is to.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The id of the user to get the subscription for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token.
    /// </remarks>
    public required UserId UserId { get; init; }
}
