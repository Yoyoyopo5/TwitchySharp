namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Contains information about an updated user.
/// </summary>
public record UpdateUserResponseContent
{
    /// <summary>
    /// A list containing the single user that was updated.
    /// </summary>
    public required TwitchUser[] Data { get; init; }
}
