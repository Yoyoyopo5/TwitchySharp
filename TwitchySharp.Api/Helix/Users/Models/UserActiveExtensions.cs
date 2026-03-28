using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Users;

/// <summary>
/// Contains information about a broadcaster's active extension slots, grouped by extension type.
/// </summary>
/// <remarks>
/// Each extension type has a specific number of slots that can be active or inactive.
/// The response includes every slot. If there is an active extension in a slot, its
/// id, version, and name are included. Inactive slots have no extension information.
/// </remarks>
public record UserActiveExtensions
{
    /// <summary>
    /// A dictionary containing the channel's panel extension slots. 
    /// </summary>
    /// <remarks>
    /// The dictionary keys are sequential numbers beginning with 1.
    /// There are 3 panel extension slots.
    /// </remarks>
    public required ImmutableDictionary<string, UserActiveExtension> Panel { get; init; }
    /// <summary>
    /// A dictionary containing the channel's overlay extension slots.
    /// </summary>
    /// <remarks>
    /// The dictionary keys are sequential numbers beginning with 1.
    /// There is 1 overlay extension slot.
    /// </remarks>
    public required ImmutableDictionary<string, UserActiveExtension> Overlay { get; init; }
    /// <summary>
    /// A dictionary containing the channel's component extension slots.
    /// </summary>
    /// <remarks>
    /// The dictionary keys are sequential numbers beginning with 1.
    /// There are 2 component extension slots.
    /// </remarks>
    public required ImmutableDictionary<string, UserActiveComponentExtension> Component { get; init; }
}
