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
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token with <see cref="Scope.ChannelReadAds"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetAdScheduleRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetAdScheduleRequestParameters parameters
        )
        : base(
            "/channels/ads",
            clientId,
            accessToken,
            new HttpQueryParameters()
              .Add("broadcaster_id", parameters.BroadcasterId)
            )
    {
        Method = HttpMethod.Get;
    }
}

public record GetAdScheduleRequestParameters
{
    /// <summary>
    /// The user id to get the ad schedule from. 
    /// This must be the same user that provided the user access token.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
}
