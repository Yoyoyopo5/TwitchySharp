using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Users;
/// <inheritdoc cref="UserActiveExtensions"/>
public record GetUserActiveExtensionsResponse
{
    /// <summary>
    /// The active extensions that the broadcaster has installed.
    /// </summary>
    public required UserActiveExtensions Data { get; init; }
}

/// <summary>
/// Contains information about a broadcaster's active extensions, grouped by extension type.
/// </summary>
public record UserActiveExtensions
{
    /// <summary>
    /// A dictionary that contains active panel extensions. 
    /// The dictionary keys are sequential numbers beginning with 1.
    /// </summary>
    public required ImmutableDictionary<string, UserActiveExtension> Panel { get; init; }
    /// <summary>
    /// A dictionary that contains active overlay extension.
    /// The dictionary keys are sequential numbers beginning with 1.
    /// </summary>
    public required ImmutableDictionary<string, UserActiveExtension> Overlay { get; init; }
    /// <summary>
    /// A dictionary that contains active component extensions.
    /// The dictionary keys are sequential numbers beginning with 1.
    /// </summary>
    public required ImmutableDictionary<string, UserActiveComponentExtension> Component { get; init; }
}

/// <summary>
/// Contains information about a specific active extension.
/// </summary>
public record UserActiveExtension
{
    /// <summary>
    /// Indicates the extension’s activation state. 
    /// If <see langword="false"/>, the user has not configured this extension.
    /// </summary>
    public required bool Active { get; init; }
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
}

/// <summary>
/// Contains information about a specific active component extension.
/// </summary>
public record UserActiveComponentExtension
    : UserActiveExtension
{
    /// <summary>
    /// The x-coordinate where the extension is placed.
    /// </summary>
    public required int X { get; init; }
    /// <summary>
    /// The y-coordinate where the extension is placed.
    /// </summary>
    public required int Y { get; init; }
}
