using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Ads;
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
    /// <param name="parameters">The request parameters.</param>
    public SnoozeNextAdRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        SnoozeNextAdRequestParameters parameters
        )
        : base(
            "/channels/ads/schedule/snooze",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
            )
    {
        Method = HttpMethod.Post;
    }
}

/// <summary>
/// Request parameters for a <see cref="SnoozeNextAdRequest"/>.
/// </summary>
public record SnoozeNextAdRequestParameters
{
    /// <summary>
    /// The user id of the channel to snooze an ad on. 
    /// This must be the same user that provided the access token for the request.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
}
