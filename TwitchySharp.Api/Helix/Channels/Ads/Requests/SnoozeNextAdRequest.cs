using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Channels.Ads;
/// <summary>
/// If available, pushes back the timestamp of the upcoming automatic mid-roll ad by 5 minutes. 
/// This endpoint duplicates the snooze functionality in the creator dashboard’s Ads Manager.
/// The channel must be live and have an upcoming scheduled ad break.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#snooze-next-ad">snooze next ad</see> for more information.
/// <br/>
/// Requires a user access token with <see cref="Scope.ChannelManageAds"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token with <see cref="Scope.ChannelManageAds"/>.</param>
/// <param name="broadcasterId">The user id of the channel to snooze an ad on. This must be the same user that provided the <paramref name="accessToken"/></param>
public class SnoozeNextAdRequest(string clientId, string accessToken, string broadcasterId)
    : HelixApiRequest<SnoozeNextAdResponse>(
        "/channels/ads/schedule/snooze" + $"broadcaster_id={broadcasterId}", 
        clientId, 
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Post;
}
