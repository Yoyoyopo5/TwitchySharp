namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Contains information about the extensions that were updated.
/// </summary>
public record UpdateUserExtensionsResponseContent
{
    /// <summary>
    /// The extensions that were updated.
    /// </summary>
    public required UserActiveExtensions Data { get; init; }
}
