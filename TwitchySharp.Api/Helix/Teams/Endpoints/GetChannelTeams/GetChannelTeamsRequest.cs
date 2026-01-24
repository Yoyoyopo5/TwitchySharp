using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Teams;
/// <summary>
/// Gets the list of Twitch teams that the broadcaster is a member of.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-teams">Get Channel Teams</see> for more information.
/// </remarks>
public record GetChannelTeamsRequest
    : TwitchHelixRequest<GetChannelTeamsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetChannelTeamsRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetChannelTeamsRequestParameters parameters
        ) : base(
            "/teams/channel",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
        )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetChannelTeamsRequest"/>.
/// </summary>
public record GetChannelTeamsRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster to get teams for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
}
