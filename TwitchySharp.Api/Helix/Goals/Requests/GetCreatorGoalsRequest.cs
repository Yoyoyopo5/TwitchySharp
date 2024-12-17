using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Goals;
/// <summary>
/// Gets the broadcaster’s list of active goals. 
/// Use this to get the current progress of each goal.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-creator-goals">get creator goals</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadGoals"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelReadGoals"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster to get goals for. 
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
public class GetCreatorGoalsRequest(string clientId, string accessToken, string broadcasterId)
    : HelixApiRequest<GetCreatorGoalsResponse>(
        "/goals" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId),
        clientId,
        accessToken
        );
