using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Extensions;
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
    public required ExtensionSecret[] Secrets { get; init; }
}

/// <summary>
/// Contains information about a specific extension secret.
/// </summary>
public record ExtensionSecret
{
    /// <summary>
    /// The raw secret that you use with JWT encoding.
    /// </summary>
    public required string Content { get; init; }
    /// <summary>
    /// The date and time that you may begin using this secret to sign a JWT.
    /// </summary>
    public required DateTimeOffset ActiveAt { get; init; }
    /// <summary>
    /// The date and time when you must stop using this secret to decode a JWT.
    /// </summary>
    public required DateTimeOffset ExpiresAt { get; init; }
}
