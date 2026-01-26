using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// <b>BETA</b> Allows a user to update slot settings for a particular guest within a Guest Star session, such as allowing the user to share audio or video within the call as a host.
/// </summary>
/// <remarks>
/// These settings will be broadcasted to all subscribers which control their view of the guest in that slot.
/// One or more of the optional parameters to this API can be specified at any time.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-guest-star-slot-settings">Update Guest Star Settings</see> for more information.
/// </remarks>
public record UpdateGuestStarSlotSettingsRequest
    : TwitchHelixRequest<UpdateGuestStarSlotSettingsResponse>
{
    protected override string Path => "/guest_star/slot_settings";
    public override HttpMethod Method => HttpMethod.Patch;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(ModeratorId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ChannelManageGuestStar, Scope.ModeratorManageGuestStar ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId)
            .Add("session_id", SessionId)
            .Add("slot_id", SlotId)
            .Add("is_audio_enabled", Settings?.IsAudioEnabled?.ToString())
            .Add("is_video_enabled", Settings?.IsVideoEnabled?.ToString())
            .Add("is_live", Settings?.IsLive?.ToString())
            .Add("volume", Settings?.Volume?.ToString());

    /// <summary>
    /// The user id of the broadcaster hosting the Guest Star session.
    /// </summary>
    public required UserId BroadcasterId { get; set; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }

    /// <summary>
    /// The id of the Guest Star session.
    /// </summary>
    public required GuestStarSessionId SessionId { get; set; }

    /// <summary>
    /// The id of the slot you want to update settings for.
    /// </summary>
    public required GuestStarSlotId SlotId { get; set; }

    /// <summary>
    /// The settings to update.
    /// </summary>
    public required GuestStarSlotSettings Settings { get; set; }
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
