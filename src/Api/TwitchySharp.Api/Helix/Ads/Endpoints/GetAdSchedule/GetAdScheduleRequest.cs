using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Ads;
/// <summary>
/// This endpoint returns ad schedule related information, including snooze, when the last ad was run, when the next ad is scheduled, and if the channel is currently in pre-roll free time. 
/// </summary>
/// <remarks>
/// A new ad cannot be run until 8 minutes after running a previous ad.
/// <br/>
/// Requires a user access token with <see cref="Scope.ChannelReadAds"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-ad-schedule">Get Ad Schedule</see> for more information.
/// </remarks>
public record GetAdScheduleRequest
    : TwitchHelixRequest<GetAdScheduleResponse>
{
    protected override string Path => "/channels/ads";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelReadAds)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);

    /// <summary>
    /// The user id of the broadcaster (channel) to get the ad schedule from. 
    /// </summary>
    /// <remarks>
    /// The request will be made on behalf of this user and requires <see cref="Scope.ChannelReadAds"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }
}
