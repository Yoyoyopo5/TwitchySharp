using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Ads;
/// <summary>
/// If available, pushes back the timestamp of the upcoming automatic mid-roll ad by 5 minutes.
/// </summary>
/// <remarks>
/// This endpoint duplicates the snooze functionality in the creator dashboard's Ads Manager.
/// The channel must be live and have an upcoming scheduled ad break.
/// <br/>
/// Requires a user access token with <see cref="Scope.ChannelManageAds"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#snooze-next-ad">Snooze Next Ad</see> for more information.
/// </remarks>
public record SnoozeNextAdRequest
    : TwitchHelixRequest<SnoozeNextAdResponse>
{
    protected override string Path => "/channels/ads/schedule/snooze";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(BroadcasterId);
    public override IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ChannelManageAds);
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
