using System;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Bits.Enums;
using TwitchySharp.Api.Models.Helix.Bits.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Bits.Requests;
/// <summary>
/// Gets the Bits leaderboard for the authenticated broadcaster.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.BitsRead"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-bits-leaderboard">Get Bits Leaderboard</see> for more information.
/// </remarks>
public record GetBitsLeaderboardRequest
    : TwitchHelixRequest<GetBitsLeaderboardResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.BitsRead"/>.</param>
    /// <param name="count">The number of results (leaderboard entries) to return. The minimum count is 1 and the maximum is 100. The default is 10.</param>
    /// <param name="period">The time period over which data is aggregated.</param>
    /// <param name="StartedAt">
    /// The start date used for determining the aggregation period. 
    /// Specify this parameter only if you specify <paramref name="period"/>. The start date is ignored if <paramref name="period"/> is <see cref="LeaderboardPeriod.All"/>.
    /// </param>
    /// <param name="userId">
    /// The user ID of a user that has cheered bits in the channel. 
    /// If count is greater than 1, the response may include users ranked above and below the specified user. 
    /// To get the leaderboard’s top leaders, set this to <see langword="null"/>.
    /// </param>
    public GetBitsLeaderboardRequest(
        string clientId,
        string accessToken,
        int? count = null,
        LeaderboardPeriod? period = null,
        DateTimeOffset? StartedAt = null,
        string? userId = null
        )
        : base(
            "/bits/leaderboard",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("count", count?.ToString())
                .Add("period", period?.Value)
                .Add("started_at", StartedAt?.UtcDateTime.AddHours(8).ToString("yyyy-MM-dd'T'HH:mm:ssZ"))
                .Add("user_id", userId)
            )
    {
        Method = HttpMethod.Get;
    }
}