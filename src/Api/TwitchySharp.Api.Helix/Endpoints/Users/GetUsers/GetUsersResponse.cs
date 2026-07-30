namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Contains a list of Twitch users.
/// </summary>
public record GetUsersResponse
{
    /// <summary>
    /// The list of users.
    /// </summary>
    public required TwitchUser[] Data { get; init; }
}
