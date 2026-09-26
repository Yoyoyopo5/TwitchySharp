namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Contains a list of extensions that the broadcaster has installed.
/// </summary>
public record GetUserExtensionsResponseContent
{
    /// <summary>
    /// The list of installed extensions.
    /// </summary>
    public required InstalledExtension[] Data { get; init; }
}
