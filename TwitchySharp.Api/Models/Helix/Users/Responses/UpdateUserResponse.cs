using TwitchySharp.Api.Models.Helix.Users.Models;

namespace TwitchySharp.Api.Models.Helix.Users.Responses;
/// <summary>
/// Contains information about an updated user.
/// </summary>
public record UpdateUserResponse
{
    /// <summary>
    /// A list containing the single user that was updated.
    /// </summary>
    public required TwitchUser[] Data { get; init; }
}
