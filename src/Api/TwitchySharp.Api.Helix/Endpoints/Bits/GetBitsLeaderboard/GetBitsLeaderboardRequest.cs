using System.Collections.Immutable;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Helix.Bits;
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
    protected override string Path => "/bits/leaderboard";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.BitsRead)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("count", Count?.ToString())
            .Add("period", Period?.Value)
            .Add("started_at", StartedAt?.UtcDateTime.AddHours(8).ToRfc3339())
            .Add("user_id", UserId);

    /// <summary>
    /// The user id of the broadcaster to get the bits leaderboard for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The number of results (leaderboard entries) to return.
    /// </summary>
    /// <remarks>
    /// The minimum count is 1 and the maximum is 100. The default is 10.
    /// </remarks>
    public int? Count { get; init; }

    /// <summary>
    /// The time period over which data is aggregated.
    /// </summary>
    public LeaderboardPeriod? Period { get; init; }

    /// <summary>
    /// The start date used for determining the aggregation period.
    /// </summary>
    /// <remarks>
    /// The start date is ignored if <see cref="Period"/> is <see cref="LeaderboardPeriod.All"/> or <see langword="null"/>.
    /// </remarks>
    public DateTimeOffset? StartedAt { get; init; }

    /// <summary>
    /// The id of a user that has cheered bits in the channel.
    /// </summary>
    /// <remarks>
    /// If <see cref="Count"/> is greater than <c>1</c>, the response may include users ranked above and below the specified user.
    /// To get the leaderboard's top leaders, set this to <see langword="null"/>.
    /// </remarks>
    public UserId? UserId { get; init; }
}
