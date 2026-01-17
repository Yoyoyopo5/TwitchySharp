using TwitchySharp.Api.Models.Helix.Extensions.Models;

namespace TwitchySharp.Api.Models.Helix.Extensions.Responses;
/// <summary>
/// Contains a list with a single <see cref="Extension"/>.
/// </summary>
public record GetExtensionsResponse
{
    /// <summary>
    /// A list that contains the requested extension as the single entry.
    /// </summary>
    public required Extension[] Data { get; init; }
}
