using TwitchySharp.Api.Models.Helix.CCLs.Models;

namespace TwitchySharp.Api.Models.Helix.CCLs.Responses;
/// <summary>
/// Contains a list of content classification labels.
/// </summary>
public record GetContentClassificationLabelsResponse
{
    /// <summary>
    /// The list of content classification labels.
    /// </summary>
    public required ContentClassificationLabel[] Data { get; init; }
}