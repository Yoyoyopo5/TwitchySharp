using TwitchySharp.Api.Models.Helix.Users.Models;

namespace TwitchySharp.Api.Models.Helix.Users.Responses;
/// <summary>
/// Contains information about the extensions that were updated.
/// </summary>
public record UpdateUserExtensionsResponse
{
    /// <summary>
    /// The extensions that were updated.
    /// </summary>
    public required UserActiveExtensions Data { get; init; }
}
