using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Activates or deactivates the broadcaster’s Shield Mode.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageShieldMode"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-shield-mode-status">Update Shield Mode Status</see> for more information.
/// </remarks>
public record UpdateShieldModeStatusRequest
    : TwitchHelixRequest<UpdateShieldModeStatusResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageShieldMode"/>.</param>
    /// <param name="broadcasterId">The user id of the broadcaster (channel) to update Shield Mode status for.</param>
    /// <param name="moderatorId">
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="shieldModeStatus">The Shield Mode status to update to.</param>
    public UpdateShieldModeStatusRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId,
        UpdateShieldModeStatusRequestData shieldModeStatus
    ) : base(
        "/moderation/shield_mode",
        clientId,
        accessToken,
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId)
    )
    {
        Method = HttpMethod.Put;
        ContentObject = shieldModeStatus;
    }
}

/// <summary>
/// Data used to set the status of Shield Mode on a channel.
/// </summary>
public record UpdateShieldModeStatusRequestData
{
    /// <summary>
    /// Determines whether to activate or deactivate Shield Mode. 
    /// Set to <see langword="true"/> to activate Shield Mode; otherwise, <see langword="false"/> to deactivate Shield Mode.
    /// </summary>
    public required bool IsActive { get; set; }
}
