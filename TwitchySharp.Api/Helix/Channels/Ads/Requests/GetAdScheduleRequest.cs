using System;
using System.Collections.Generic;
using System.Text;
using TwitchySharp.Api.Helix.Channels.Ads.Responses;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Channels.Ads.Requests;
/// <summary>
/// This endpoint returns ad schedule related information, including snooze, when the last ad was run, when the next ad is scheduled, and if the channel is currently in pre-roll free time. 
/// Note that a new ad cannot be run until 8 minutes after running a previous ad.
/// Requires a user access token with <see cref="Scope.ChannelReadAds"/>.
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-ad-schedule">get ad schedule</see> for more information.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token with <see cref="Scope.ChannelReadAds"/>.</param>
/// <param name="broadcasterId">The user id to get the ad schedule from. This must be the same user that provided the <paramref name="accessToken"/>.</param>
public class GetAdScheduleRequest(string clientId, string accessToken, string broadcasterId)
    : HelixApiRequest<GetAdScheduleResponse>("/channels/ads" + $"?broadcaster_id={broadcasterId}", clientId, accessToken);
