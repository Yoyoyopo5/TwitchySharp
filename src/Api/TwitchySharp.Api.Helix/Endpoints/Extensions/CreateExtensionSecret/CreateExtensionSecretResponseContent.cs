namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Contains a list of newly added extension secrets.
/// </summary>
public record CreateExtensionSecretResponseContent
{
    /// <summary>
    /// The list of newly created secrets, grouped by version.
    /// </summary>
    public required ExtensionSecretsVersion[] Data { get; init; }
}
