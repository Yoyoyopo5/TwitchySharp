namespace TwitchySharp.Api.Helix.Channels;

public record GetChannelFollowersResponseContent
    : IPageableResponse
{
    /// <summary>
    /// The list of users that follow the specified broadcaster. 
    /// The list is in descending order by <see cref="ChannelFollower.FollowedAt"/> (with the most recent follower first). 
    /// The list is empty if nobody follows the broadcaster, the requested user id isn’t in the follower list, the user access token is missing <see cref="Scope.ModeratorReadFollowers"/>, or the user isn’t the broadcaster or moderator for the channel.
    /// </summary>
    public required ChannelFollower[] Data { get; init; }
    /// <summary>
    /// Contains the information used to page through the list of results. 
    /// The <see cref="Pagination.Cursor"/> is null if there are no more pages left to page through.
    /// </summary>
    public required Pagination Pagination { get; init; }
    /// <summary>
    /// The total number of users that follow this broadcaster. 
    /// As someone pages through the list, the number of users may change as users follow or unfollow the broadcaster.
    /// </summary>
    public required int Total { get; init; }
}
