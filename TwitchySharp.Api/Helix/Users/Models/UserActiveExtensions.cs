using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Users;

/// <summary>
/// Contains information about a broadcaster's active extensions, grouped by extension type.
/// </summary>
public record UserActiveExtensions
{
    /// <summary>
    /// A dictionary that contains active panel extensions. 
    /// The dictionary keys are sequential numbers beginning with 1.
    /// </summary>
    public required ImmutableDictionary<string, UserActiveExtension> Panel { get; init; }
    /// <summary>
    /// A dictionary that contains active overlay extension.
    /// The dictionary keys are sequential numbers beginning with 1.
    /// </summary>
    public required ImmutableDictionary<string, UserActiveExtension> Overlay { get; init; }
    /// <summary>
    /// A dictionary that contains active component extensions.
    /// The dictionary keys are sequential numbers beginning with 1.
    /// </summary>
    public required ImmutableDictionary<string, UserActiveComponentExtension> Component { get; init; }
}
