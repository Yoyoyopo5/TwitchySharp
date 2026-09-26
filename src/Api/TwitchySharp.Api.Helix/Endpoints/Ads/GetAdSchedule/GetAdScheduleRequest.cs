using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Ads;
/// <summary>
/// This endpoint returns ad schedule related information, including snooze, when the last ad was run, when the next ad is scheduled, and if the channel is currently in pre-roll free time. 
/// </summary>
/// <remarks>
/// A new ad cannot be run until 8 minutes after running a previous ad.
/// <para>
/// Requires a user access token with <see cref="Scope.ChannelReadAds"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ChannelReadAds"/> for the <see cref="BroadcasterId"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-ad-schedule">Get Ad Schedule</see> for more information.
/// </remarks>
public record GetAdScheduleRequest
    : TwitchHelixRequest<GetAdScheduleResponseContent>,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    protected override string Path => "/channels/ads";
    public override HttpMethod Method => HttpMethod.Get;
    private readonly static ImmutableHashSet<Scope> Scopes = [Scope.ChannelReadAds];
    private UserSupportingPriorAuthorizationAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = Scopes
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);

    public UserSupportingPriorAuthorizationAuthenticationContext AuthenticationContext { get => field ?? DefaultAuthenticationContext; init; }

    /// <summary>
    /// The user id of the broadcaster (channel) to get the ad schedule from. 
    /// </summary>
    /// <remarks>
    /// The request will be made on behalf of this user and requires <see cref="Scope.ChannelReadAds"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }
}
