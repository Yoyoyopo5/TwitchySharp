using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Contains information about a specific extension configuration segment.
/// </summary>
public record ExtensionConfigurationSegment
{
    /// <summary>
    /// The type of segment.
    /// </summary>
    public required ExtensionConfigurationSegmentType Segment { get; init; }
    /// <summary>
    /// The user id of the broadcaster that installed the extension. 
    /// This is <see langword="null"/> if <see cref="Segment"/> is set to <see cref="ExtensionConfigurationSegmentType.Global"/>.
    /// </summary>
    public UserId? BroadcasterId { get; init; }
    /// <summary>
    /// The contents of the segment. 
    /// This string may be a plain-text string or a string-encoded JSON object.
    /// </summary>
    public required string Content { get; init; }
    /// <summary>
    /// The version number that identifies this definition of the segment’s data.
    /// </summary>
    public required ExtensionVersion Version { get; init; }
}
