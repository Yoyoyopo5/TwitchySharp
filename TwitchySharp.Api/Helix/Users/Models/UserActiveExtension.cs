namespace TwitchySharp.Api.Helix.Users;

/// <summary>
/// Contains information about a specific active extension.
/// </summary>
public record UserActiveExtension
{
    /// <summary>
    /// Indicates the extension’s activation state. 
    /// If <see langword="false"/>, the user has not configured this extension.
    /// </summary>
    public required bool Active { get; init; }
    /// <summary>
    /// The id of the extension.
    /// </summary>
    public string? Id { get; init; }
    /// <summary>
    /// The version of the extension.
    /// </summary>
    public string? Version { get; init; }
    /// <summary>
    /// The name of the extension.
    /// </summary>
    public string? Name { get; init; }
}
