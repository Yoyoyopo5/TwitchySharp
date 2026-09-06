using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Ads;
/// <summary>
/// If available, pushes back the timestamp of the upcoming automatic mid-roll ad by 5 minutes.
/// </summary>
/// <remarks>
/// This endpoint duplicates the snooze functionality in the creator dashboard's Ads Manager.
/// The channel must be live and have an upcoming scheduled ad break.
/// <para>
/// Requires a user access token with <see cref="Scope.ChannelManageAds"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ChannelManageAds"/> for the <see cref="BroadcasterId"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#snooze-next-ad">Snooze Next Ad</see> for more information.
/// </remarks>
public record SnoozeNextAdRequest
    : TwitchHelixRequest<SnoozeNextAdResponseContent>,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    protected override string Path => "/channels/ads/schedule/snooze";
    public override HttpMethod Method => HttpMethod.Post;

    private UserSupportingPriorAuthorizationAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManageAds)
    };
    public UserSupportingPriorAuthorizationAuthenticationContext AuthenticationContext { get => field ?? DefaultAuthenticationContext; init; }

    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);

    /// <summary>
    /// The user id of the channel to snooze an ad on.
    /// </summary>
    /// <remarks>
    /// This must be the same user that provided the access token for the request.
    /// Requires <see cref="Scope.ChannelManageAds"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }
}
