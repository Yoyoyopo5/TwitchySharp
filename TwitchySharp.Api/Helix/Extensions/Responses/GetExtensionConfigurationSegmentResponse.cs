using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Contains a list of extension configuration segment data.
/// </summary>
public record GetExtensionConfigurationSegmentResponse
{
    /// <summary>
    /// The list of requested configuration segments. 
    /// The list is returned in the same order that you specified the list of segments in the request.
    /// </summary>
    public required ExtensionConfigurationSegment[] Data { get; init; }
}

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
    public string? BroadcasterId { get; init; }
    /// <summary>
    /// The contents of the segment. 
    /// This string may be a plain-text string or a string-encoded JSON object.
    /// </summary>
    public required string Content { get; init; }
    /// <summary>
    /// The version number that identifies this definition of the segment’s data.
    /// </summary>
    public required string Version { get; init; }
}

/// <summary>
/// Contains static definitions for types of extension configuration segments.
/// </summary>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ExtensionConfigurationSegmentType, string>))]
public record ExtensionConfigurationSegmentType(string Value)
    : ValueBackedEnum<string>(Value)
{
    public static ExtensionConfigurationSegmentType Broadcaster { get; } = new("broadcaster");
    public static ExtensionConfigurationSegmentType Developer { get; } = new("developer");
    public static ExtensionConfigurationSegmentType Global { get; } = new("global");
}
