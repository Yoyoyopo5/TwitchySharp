namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Contains a group of extension secrets that have a specific version.
/// </summary>
public record ExtensionSecretsVersion
{
    /// <summary>
    /// The version number that identifies this definition of the secret’s data.
    /// </summary>
    public required int FormatVersion { get; init; }
    /// <summary>
    /// The list of secrets.
    /// </summary>
    public required ExtensionSecretDetails[] Secrets { get; init; }
}
