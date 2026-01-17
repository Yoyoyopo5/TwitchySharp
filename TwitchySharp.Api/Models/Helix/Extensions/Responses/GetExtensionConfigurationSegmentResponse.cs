using TwitchySharp.Api.Models.Helix.Extensions.Models;

namespace TwitchySharp.Api.Models.Helix.Extensions.Responses;
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