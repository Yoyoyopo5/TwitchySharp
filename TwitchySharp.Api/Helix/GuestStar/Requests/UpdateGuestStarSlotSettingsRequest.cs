using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// BETA
/// <br/>
/// Allows a user to update slot settings for a particular guest within a Guest Star session, such as allowing the user to share audio or video within the call as a host. 
/// These settings will be broadcasted to all subscribers which control their view of the guest in that slot. 
/// One or more of the optional parameters to this API can be specified at any time.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-guest-star-slot-settings">update Guest Star settings</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster hosting the Guest Star session.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator of the broadcaster's channel.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="sessionId">The id of the Guest Star session.</param>
/// <param name="slotId">The id of the slot you want to update settings for.</param>
/// <param name="settings">The settings to update.</param>
public class UpdateGuestStarSlotSettingsRequest(
    string clientId, 
    string accessToken,
    string broadcasterId,
    string moderatorId,
    string sessionId,
    string slotId,
    GuestStarSlotSettings settings
    )
    : HelixApiRequest<UpdateGuestStarSettingsRequest>(
        "/guest_star/slot_settings" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId)
            .Add("session_id", sessionId)
            .Add("slot_id", slotId)
            .Add("is_audio_enabled", settings.IsAudioEnabled?.ToString())
            .Add("is_video_enabled", settings.IsVideoEnabled?.ToString())
            .Add("is_live", settings.IsLive?.ToString())
            .Add("volume", settings.Volume?.ToString()),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Patch;
}

/// <summary>
/// Contains various settings for a specific Guest Star session slot.
/// </summary>
public record GuestStarSlotSettings
{
    /// <summary>
    /// Determines whether the slot is allowed to share their audio with the rest of the session. 
    /// If <see langword="false"/>, the slot will be muted in any views containing the slot.
    /// </summary>
    public bool? IsAudioEnabled { get; set; }
    /// <summary>
    /// Determines whether the slot is allowed to share their video with the rest of the session. 
    /// If <see langword="false"/>, the slot will have no video shared in any views containing the slot.
    /// </summary>
    public bool? IsVideoEnabled { get; set; }
    /// <summary>
    /// Determines whether the user assigned to this slot is visible/can be heard from any public subscriptions. 
    /// Generally, this determines whether or not the slot is enabled in any broadcasting software integrations.
    /// </summary>
    public bool? IsLive { get; set; }
    /// <summary>
    /// Value from 0-100 that controls the audio volume for shared views containing the slot.
    /// </summary>
    public int? Volume { get; set; }
}
