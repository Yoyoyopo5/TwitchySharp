
namespace TwitchySharp.Api.Helix.Users;

/// <summary>
/// Contains information about a specific blocked user.
/// </summary>
public record BlockedUser
{
    /// <summary>
    /// The user id of the blocked user.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the blocked user.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the blocked user.
    /// </summary>
    public required UserName DisplayName { get; init; }
}
