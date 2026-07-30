namespace TwitchySharp.Api.Helix.Users;

/// <summary>
/// Contains information about a specific active component extension.
/// </summary>
public record UserActiveComponentExtension
    : UserActiveExtension
{
    /// <summary>
    /// The x-coordinate where the extension is placed.
    /// </summary>
    public int? X { get; init; }
    /// <summary>
    /// The y-coordinate where the extension is placed.
    /// </summary>
    public int? Y { get; init; }
}
