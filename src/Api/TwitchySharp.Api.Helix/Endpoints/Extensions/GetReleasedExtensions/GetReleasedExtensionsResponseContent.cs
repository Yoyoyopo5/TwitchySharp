namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Contains a list of requested released extensions.
/// </summary>
public record GetReleasedExtensionsResponseContent
{
    /// <summary>
    /// A list that contains the specified extension as its single entry.
    /// </summary>
    public required Extension[] Data { get; init; }
}
