using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// <b>BETA</b> Mutates the channel settings for configuration of the Guest Star feature for a particular host.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-channel-guest-star-settings">Update Channel Guest Star Settings</see> for more information.
/// </remarks>
public record UpdateChannelGuestStarSettingsRequest
    : TwitchHelixRequest<UpdateChannelGuestStarSettingsResponse>
{
    protected override string Path => "/guest_star/channel_settings";
    public override HttpMethod Method => HttpMethod.Patch;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManageGuestStar)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);
    public override object? ContentObject => Settings;

    /// <summary>
    /// The user id of the broadcaster you want to update settings for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The settings to update.
    /// </summary>
    public required UpdateChannelGuestStarSettingsRequestData Settings { get; init; }

    protected override ValueTask<UpdateChannelGuestStarSettingsResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
        => ValueTask.FromResult(new UpdateChannelGuestStarSettingsResponse());
}

/// <summary>
/// Contains data used to set Guest Star settings.
/// </summary>
public record UpdateChannelGuestStarSettingsRequestData
{
    /// <summary>
    /// Determines if Guest Star moderators have access to control whether a guest is live once assigned to a slot.
    /// </summary>
    public bool? IsModeratorSendLiveEnabled { get; init; }
    /// <summary>
    /// Number of slots the Guest Star call interface will allow the host to add to a call. 
    /// Required to be between 1 and 6.
    /// </summary>
    public int? SlotCount { get; init; }
    /// <summary>
    /// Determines if Browser Sources subscribed to sessions on this channel should output audio.
    /// </summary>
    public bool? IsBrowserSourceAudioEnabled { get; init; }
    /// <summary>
    /// Determines how the guests within a session should be laid out within the browser source.
    /// </summary>
    public GuestStarGroupLayout? GroupLayout { get; init; }
    /// <summary>
    /// Determines if Guest Star should regenerate the auth token associated with the channel’s browser sources. 
    /// Providing a <see langword="true"/> value for this will immediately invalidate all browser sources previously configured in your streaming software.
    /// </summary>
    public bool? RegenerateBrowserSources { get; init; }
}
