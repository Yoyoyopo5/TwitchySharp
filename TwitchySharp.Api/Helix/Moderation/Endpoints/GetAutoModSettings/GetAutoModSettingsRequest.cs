using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets the broadcaster’s AutoMod settings. 
/// </summary>
/// <remarks>
/// The settings are used to automatically block inappropriate or harassing messages from appearing in the broadcaster’s chat room.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadAutomodSettings"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-automod-settings">Get AutoMod Settings</see> for more information.
/// </remarks>
public record GetAutoModSettingsRequest
    : TwitchHelixRequest<GetAutoModSettingsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorReadAutomodSettings"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetAutoModSettingsRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetAutoModSettingsRequestParameters parameters
        ) : base(
            "/moderation/automod/settings",
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
/// Request parameters for a <see cref="GetAutoModSettingsRequest"/>.
/// </summary>
public record GetAutoModSettingsRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get AutoMod settings for.
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
