using TwitchySharp.Api.Models.Helix.Extensions.Models;

namespace TwitchySharp.Api.Models.Helix.Extensions.Responses;
/// <summary>
/// Contains a list of an extension's shared secrets, grouped by version.
/// </summary>
public record GetExtensionSecretsResponse
{
    /// <summary>
    /// The list of shared extension secrets, grouped by version.
    /// </summary>
    public required ExtensionSecretsVersion[] Data { get; init; }
}