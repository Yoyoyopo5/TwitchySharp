using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Authorization;

/// <summary>
/// Contains information about a specific user's authorization with a specific app.
/// </summary>
public record UserAuthorization
{
    /// <summary>
    /// The user id of the authorized user.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the authorized user.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the authorized user.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The scopes that the user has granted to the client (app).
    /// </summary>
    public required Scope[] Scopes { get; init; }
}
