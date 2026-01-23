using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

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
    /// <param name="parameters">The request parameters.</param>
    /// <param name="shieldModeStatus">The Shield Mode status to update to.</param>
    public UpdateShieldModeStatusRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        UpdateShieldModeStatusRequestParameters parameters,
        UpdateShieldModeStatusRequestData shieldModeStatus
        ) : base(
            "/moderation/shield_mode",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("moderator_id", parameters.ModeratorId)
            )
    {
        Method = HttpMethod.Put;
        ContentObject = shieldModeStatus;
    }
}

/// <summary>
/// Request parameters for a <see cref="UpdateShieldModeStatusRequest"/>.
/// </summary>
public record UpdateShieldModeStatusRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) to update Shield Mode status for.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }
}

/// <summary>
/// Data used to set the status of Shield Mode on a channel.
/// </summary>
public record UpdateShieldModeStatusRequestData
{
    /// <summary>
    /// Determines whether to activate or deactivate Shield Mode. 
    /// </summary>
    /// <remarks>
    /// Set to <see langword="true"/> to activate Shield Mode; otherwise, <see langword="false"/> to deactivate Shield Mode.
    /// </remarks>
    public required bool IsActive { get; set; }
}
