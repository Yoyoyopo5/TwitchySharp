using TwitchySharp.Api.Models.Helix.Users.Models;

namespace TwitchySharp.Api.Models.Helix.Users.Responses;
/// <summary>
/// Contains a list of extensions that the broadcaster has installed.
/// </summary>
public record GetUserExtensionsResponse
{
    /// <summary>
    /// The list of installed extensions.
    /// </summary>
    public required InstalledExtension[] Data { get; init; }
}
