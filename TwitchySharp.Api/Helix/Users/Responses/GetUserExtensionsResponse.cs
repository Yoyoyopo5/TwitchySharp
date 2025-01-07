using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Contains a list of extensions that the broadcaster has installed.
/// </summary>
public record GetUserExtensionsResponse
{
    /// <summary>
    /// The list of installed extensions.
    /// </summary>
    public required InstalledExtension[] Data { get; init; }
}

/// <summary>
/// Contains information about a specific installed extension.
/// </summary>
public record InstalledExtension
{
    /// <summary>
    /// The id of the extension.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The version of the extension.
    /// </summary>
    public required string Version { get; init; }
    /// <summary>
    /// The name of the extension.
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// Indicates whether the extension is configured and can be activated.
    /// </summary>
    public required bool CanActivate { get; init; }
    /// <summary>
    /// The extension types that you can activate for this extension.
    /// </summary>
    public required UserExtensionType[] Type { get; init; }
}
