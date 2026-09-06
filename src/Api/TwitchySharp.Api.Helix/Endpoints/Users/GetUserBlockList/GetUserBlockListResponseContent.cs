namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Contains the list of users that the broadcaster has blocked.
/// </summary>
public record GetUserBlockListResponseContent
{
    /// <summary>
    /// The list of blocked users, in decending order by when the user was blocked.
    /// </summary>
    public required BlockedUser[] Data { get; init; }
}
