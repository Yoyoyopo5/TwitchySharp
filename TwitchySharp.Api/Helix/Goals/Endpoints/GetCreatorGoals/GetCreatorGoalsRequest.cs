using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Goals;
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
    /// <param name="parameters">The request parameters.</param>
    public GetCreatorGoalsRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetCreatorGoalsRequestParameters parameters
        ) : base(
            "/goals",
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
/// Request parameters for a <see cref="GetCreatorGoalsRequest"/>.
/// </summary>
public record GetCreatorGoalsRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get goals for. 
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
}
