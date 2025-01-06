using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Teams;
/// <summary>
/// Gets the list of Twitch teams that the broadcaster is a member of.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-teams">get channel teams</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="broadcasterId">The user id of the broadcaster to get teams for.</param>
public class GetChannelTeamsRequest(
    string clientId,
    string accessToken,
    string broadcasterId
    )
    : HelixApiRequest<GetChannelTeamsResponse>(
        "/teams/channel" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId),
        clientId,
        accessToken
        );
