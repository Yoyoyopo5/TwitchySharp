using TwitchySharp.Api.Models.Helix.Users.Models;

namespace TwitchySharp.Api.Models.Helix.Users.Responses;
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