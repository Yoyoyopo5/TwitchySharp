using System.Net.Http;
using TwitchySharp.Helpers;

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
    /// <param name="broadcasterId">The user id of the broadcaster to get teams for.</param>
    public GetChannelTeamsRequest(
        string clientId,
        string accessToken,
        string broadcasterId
    ) : base(
        "/teams/channel",
        clientId,
        accessToken,
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
    )
    {
        Method = HttpMethod.Get;
    }
}
