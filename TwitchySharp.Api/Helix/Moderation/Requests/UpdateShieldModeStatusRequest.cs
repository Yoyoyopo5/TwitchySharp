using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Activates or deactivates the broadcaster’s Shield Mode.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-shield-mode-status">update Shield Mode status</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageShieldMode"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageShieldMode"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster (channel) to update Shield Mode status for.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator of the broadcaster's channel.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="shieldModeStatus">The Shield Mode status to update to.</param>
public class UpdateShieldModeStatusRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string moderatorId,
    UpdateShieldModeStatusRequestData shieldModeStatus
    )
    : HelixApiRequest<UpdateShieldModeStatusResponse, UpdateShieldModeStatusRequestData>(
        "/moderation/shield_mode" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId),
        clientId,
        accessToken,
        shieldModeStatus
        )
{
    public override HttpMethod Method => HttpMethod.Put;
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
