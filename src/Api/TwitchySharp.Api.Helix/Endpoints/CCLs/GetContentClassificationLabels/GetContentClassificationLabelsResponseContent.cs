namespace TwitchySharp.Api.Helix.CCLs;
/// <summary>
/// Contains a list of content classification labels.
/// </summary>
public record GetContentClassificationLabelsResponseContent
{
    /// <summary>
    /// The list of content classification labels.
    /// </summary>
    public required ContentClassificationLabel[] Data { get; init; }
}
