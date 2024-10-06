using System;
using System.Collections.Generic;
using System.Text;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Bits;
/// <summary>
/// Gets the Bits leaderboard for the authenticated broadcaster.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-bits-leaderboard">get bits leaderboard</see> for more information.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.BitsRead"/>.
/// </summary>
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
public class GetBitsLeaderboardRequest(
    string clientId,
    string accessToken,
    int? count = null,
    LeaderboardPeriod? period = null,
    DateTimeOffset? StartedAt = null,
    string? userId = null
    )
    : HelixApiRequest<GetBitsLeaderboardResponse>(
        "/bits/leaderboard" + 
        new HttpQueryParameters()
            .Add("count", count?.ToString())
            .Add("period", period?.Value)
            .Add("started_at", StartedAt?.UtcDateTime.AddHours(8).ToString("yyyy-MM-dd'T'HH:mm:ssZ"))
            .Add("user_id", userId),
        clientId,
        accessToken
        );

public record LeaderboardPeriod : ValueBackedEnum<string>
{
    /// <summary>
    /// A day spans from 00:00:00 on the day specified in StartedAt and runs through 00:00:00 of the next day.
    /// </summary>
    public static LeaderboardPeriod Day { get; } = new("day");
    /// <summary>
    /// A week spans from 00:00:00 on the Monday of the week specified in StartedAt and runs through 00:00:00 of the next Monday.
    /// </summary>
    public static LeaderboardPeriod Week { get; } = new("week");
    /// <summary>
    /// A month spans from 00:00:00 on the first day of the month specified in StartedAt and runs through 00:00:00 of the first day of the next month.
    /// </summary>
    public static LeaderboardPeriod Month { get; } = new("month");
    /// <summary>
    /// A year spans from 00:00:00 on the first day of the year specified in StartedAt and runs through 00:00:00 of the first day of the next year.
    /// </summary>
    public static LeaderboardPeriod Year { get; } = new("year");
    /// <summary>
    /// Default. The lifetime of the broadcaster's channel.
    /// </summary>
    public static LeaderboardPeriod All { get; } = new("all");
    private LeaderboardPeriod(string period) : base(period) { }
}
