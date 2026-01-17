using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Goals.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Goals.Requests;
/// <summary>
/// Gets the broadcaster’s list of active goals.
/// </summary>
/// <remarks>
/// Use this to get the current progress of each goal.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelReadGoals"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-creator-goals">Get Creator Goals</see> for more information.
/// </remarks>
public record GetCreatorGoalsRequest : TwitchHelixRequest<GetCreatorGoalsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelReadGoals"/>.</param>
    /// <param name="broadcasterId">
    /// The user id of the broadcaster to get goals for. 
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    public GetCreatorGoalsRequest(
        string clientId,
        string accessToken,
        string broadcasterId
        ) : base(
            "/goals",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
            )
    {
        Method = HttpMethod.Get;
    }
}
