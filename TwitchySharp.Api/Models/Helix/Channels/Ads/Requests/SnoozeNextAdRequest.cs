using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Channels.Ads.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Channels.Ads.Requests;
/// <summary>
/// If available, pushes back the timestamp of the upcoming automatic mid-roll ad by 5 minutes. 
/// </summary>
/// <remarks>
/// This endpoint duplicates the snooze functionality in the creator dashboard’s Ads Manager.
/// The channel must be live and have an upcoming scheduled ad break.
/// <br/>
/// Requires a user access token with <see cref="Scope.ChannelManageAds"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#snooze-next-ad">Snooze Next Ad</see> for more information.
/// </remarks>
public record SnoozeNextAdRequest
    : TwitchHelixRequest<SnoozeNextAdResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token with <see cref="Scope.ChannelManageAds"/>.</param>
    /// <param name="broadcasterId">The user id of the channel to snooze an ad on. This must be the same user that provided the <paramref name="accessToken"/></param>
    public SnoozeNextAdRequest(
        string clientId,
        string accessToken,
        string broadcasterId
        )
        : base(
            "/channels/ads/schedule/snooze",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
            )
    {
        Method = HttpMethod.Post;
    }
}
