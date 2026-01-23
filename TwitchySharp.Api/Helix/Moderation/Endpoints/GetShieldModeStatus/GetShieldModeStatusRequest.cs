using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets the broadcaster’s Shield Mode activation status.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadShieldMode"/> or <see cref="Scope.ModeratorManageShieldMode"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-shield-mode-status">Get Shield Mode Status</see> for more information.
/// </remarks>
public record GetShieldModeStatusRequest
    : TwitchHelixRequest<GetShieldModeStatusResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorReadShieldMode"/> or <see cref="Scope.ModeratorManageShieldMode"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetShieldModeStatusRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetShieldModeStatusRequestParameters parameters
        ) : base(
            "/moderation/shield_mode",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("moderator_id", parameters.ModeratorId)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetShieldModeStatusRequest"/>.
/// </summary>
public record GetShieldModeStatusRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get Shield Mode status for.
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
