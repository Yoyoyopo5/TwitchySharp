
namespace TwitchySharp.Api.Helix.Authorization;

/// <summary>
/// Contains information about a specific user's authorization with a specific app.
/// </summary>
public record UserAuthorization
{
    /// <summary>
    /// The user id of the authorized user.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the authorized user.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the authorized user.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The scopes that the user has granted to the client (app).
    /// </summary>
    public required Scope[] Scopes { get; init; }
}
