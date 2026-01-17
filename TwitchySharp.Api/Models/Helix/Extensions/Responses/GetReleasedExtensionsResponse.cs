using TwitchySharp.Api.Models.Helix.Extensions.Models;

namespace TwitchySharp.Api.Models.Helix.Extensions.Responses;
/// <summary>
/// Contains a list of requested released extensions.
/// </summary>
public record GetReleasedExtensionsResponse
{
    /// <summary>
    /// A list that contains the specified extension as its single entry.
    /// </summary>
    public required Extension[] Data { get; init; }
}
