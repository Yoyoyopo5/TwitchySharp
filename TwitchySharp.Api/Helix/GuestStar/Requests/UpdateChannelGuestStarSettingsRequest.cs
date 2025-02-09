using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// BETA
/// <br/>
/// Mutates the channel settings for configuration of the Guest Star feature for a particular host.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-channel-guest-star-settings">update channel Guest Star settings</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageGuestStar"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster you want to update settings for. 
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="settings">The settings to update.</param>
public class UpdateChannelGuestStarSettingsRequest(
    string clientId, 
    string accessToken, 
    string broadcasterId, 
    UpdateChannelGuestStarSettingsRequestData settings
    )
    : HelixApiRequest<UpdateChannelGuestStarSettingsResponse, UpdateChannelGuestStarSettingsRequestData>(
        "/guest_star/channel_settings" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId),
        clientId,
        accessToken,
        settings
        )
{
    // Dev Note: Examples for this request show fields in the request data as query parameters.
    // The docs also show these as body fields.
    public override HttpMethod Method => HttpMethod.Patch;
}

/// <summary>
/// Contains data used to set Guest Star settings.
/// </summary>
public record UpdateChannelGuestStarSettingsRequestData
{
    /// <summary>
    /// Determines if Guest Star moderators have access to control whether a guest is live once assigned to a slot.
    /// </summary>
    public bool? IsModeratorSendLiveEnabled { get; set; }
    /// <summary>
    /// Number of slots the Guest Star call interface will allow the host to add to a call. 
    /// Required to be between 1 and 6.
    /// </summary>
    public int? SlotCount { get; set; }
    /// <summary>
    /// Determines if Browser Sources subscribed to sessions on this channel should output audio.
    /// </summary>
    public bool? IsBrowserSourceAudioEnabled { get; set; }
    /// <summary>
    /// Determines how the guests within a session should be laid out within the browser source.
    /// </summary>
    [JsonConverter(typeof(SnakeCaseUpperJsonStringEnumConverter<GuestStarGroupLayout>))]
    public GuestStarGroupLayout? GroupLayout { get; set; }
    /// <summary>
    /// Determines if Guest Star should regenerate the auth token associated with the channel’s browser sources. 
    /// Providing a <see langword="true"/> value for this will immediately invalidate all browser sources previously configured in your streaming software.
    /// </summary>
    public bool? RegenerateBrowserSources { get; set; }
}
