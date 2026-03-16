using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Updates an installed extension's information.
/// </summary>
/// <remarks>
/// You can update the extension's activation state, ID, and version number.
/// If you try to activate an extension under multiple extension types, the last write wins (and there is no guarantee of write order).
/// <br/>
/// Requires a user access token that includes <see cref="Scope.UserEditBroadcast"/>.
/// The broadcaster who created the token is the one whose extensions will be updated.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-user-extensions">Update User Extensions</see> for more information.
/// </remarks>
public record UpdateUserExtensionsRequest
    : TwitchHelixRequest<UpdateUserExtensionsResponse>
{
    protected override string Path => "/users/extensions";
    public override HttpMethod Method => HttpMethod.Put;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = User,
        ValidScopes = ImmutableHashSet.Create(Scope.UserEditBroadcast)
    };

    /// <summary>
    /// The user identity of the broadcaster whose extensions will be updated.
    /// </summary>
    public required TwitchIdentity.User User { get; init; }

    // Note: Unsure of how this function actually behaves. I'm assuming only included extensions are updated, but if all extensions are updated, this could delete extensions.
    // Class may need to be re-written during testing because of how crap the docs are for this one. Very strange models as well.
    public override object? ContentObject => new UpdateUserExtensionsRequestData(Extensions);

    /// <summary>
    /// The extensions to update.
    /// </summary>
    public required ExtensionsConfiguration Extensions { get; init; }
}

/// <summary>
/// Used to properly serialize the content in a <see cref="UpdateUserExtensionsRequest"/>.
/// </summary>
internal record UpdateUserExtensionsRequestData
{
    public UpdateUserExtensionsMaps Data { get; init; }
    public UpdateUserExtensionsRequestData(ExtensionsConfiguration extensions)
        => Data = new UpdateUserExtensionsMaps()
        {
            Panel = extensions.PanelExtensions?.Extensions
                .Select(
                (extension, index) => new KeyValuePair<string, UserExtensionUpdate>(
                    (index + 1).ToString(),
                    extension.ToExtensionUpdate()
                    )
                ).ToImmutableDictionary(),
            Overlay = extensions.OverlayExtensions?.Extensions
                .Select(
                (extension, index) => new KeyValuePair<string, UserExtensionUpdate>(
                    (index + 1).ToString(),
                    extension.ToExtensionUpdate()
                    )
                ).ToImmutableDictionary(),
            Component = extensions.ComponentExtensions?.Extensions
                .Select(
                (extension, index) => new KeyValuePair<string, UserComponentExtensionUpdate>(
                    (index + 1).ToString(),
                    extension.ToExtensionUpdate()
                    )
                ).ToImmutableDictionary()
        };
}

# region INTERNAL_USE
/// <summary>
/// Used to properly serialize a <see cref="UpdateUserExtensionsRequest"/>.
/// </summary>
internal record UpdateUserExtensionsMaps
{
    /// <summary>
    /// The panel extensions that will be updated.
    /// </summary>
    public IReadOnlyDictionary<string, UserExtensionUpdate>? Panel { get; init; }
    /// <summary>
    /// The overlay extensions that will be updated.
    /// </summary>
    public IReadOnlyDictionary<string, UserExtensionUpdate>? Overlay { get; init; }
    /// <summary>
    /// The component extensions that will be updated.
    /// </summary>
    public IReadOnlyDictionary<string, UserComponentExtensionUpdate>? Component { get; init; }
}

/// <summary>
/// Used to get a unique signature for an extension.
/// Includes both the extension id and version.
/// </summary>
/// <param name="Id">The id of the extension.</param>
/// <param name="Version">The version of the extension.</param>
internal readonly record struct ExtensionIdentifier(ExtensionId Id, ExtensionVersion Version);

/// <summary>
/// Used to serialize an extension configuration.
/// </summary>
/// /// <param name="Id">The id of the extension.</param>
/// <param name="Version">The version of the extension.</param>
public record UserExtensionUpdate(ExtensionId Id, ExtensionVersion Version)
    : UpdateExtensionParameters();

/// <summary>
/// Used to serialize an extension configuration.
/// </summary>
/// /// <param name="Id">The id of the extension.</param>
/// <param name="Version">The version of the extension.</param>
public record UserComponentExtensionUpdate(ExtensionId Id, ExtensionVersion Version)
    : UpdateComponentExtensionParameters();

internal static class InstalledExtensionExtensions
{
    /// <summary>
    /// Creates an <see cref="ExtensionIdentifier"/> for an <see cref="InstalledExtension"/> as returned by <see cref="GetUserExtensionsRequest"/>.
    /// </summary>
    /// <param name="extension">The extension to create an indentifier for.</param>
    public static ExtensionIdentifier ToIdentifier(this InstalledExtension extension)
        => new(extension.Id, extension.Version);
}

internal static class KeyValuePairExtensions
{
    /// <summary>
    /// Converts a consumer-created extension configuration into a the format Twitch API expects.
    /// </summary>
    public static UserExtensionUpdate ToExtensionUpdate(this KeyValuePair<ExtensionIdentifier, UpdateExtensionParameters> extensionConfig)
        => new(extensionConfig.Key.Id, extensionConfig.Key.Version)
        {
            Active = extensionConfig.Value.Active
        };

    /// <summary>
    /// Converts a consumer-created extension configuration into a the format Twitch API expects.
    /// </summary>
    public static UserComponentExtensionUpdate ToExtensionUpdate(this KeyValuePair<ExtensionIdentifier, UpdateComponentExtensionParameters> extensionConfig)
        => new(extensionConfig.Key.Id, extensionConfig.Key.Version)
        {
            Active = extensionConfig.Value.Active,
            X = extensionConfig.Value.X,
            Y = extensionConfig.Value.Y
        };
}

# endregion

/// <summary>
/// Contains information used to update a broadcaster's extensions.
/// </summary>
public record ExtensionsConfiguration
{
    /// <summary>
    /// The panel extensions to update.
    /// </summary>
    public ExtensionsConfigurationType<UpdateExtensionParameters>? PanelExtensions { get; init; }
    /// <summary>
    /// The overlay extensions to update.
    /// </summary>
    public ExtensionsConfigurationType<UpdateExtensionParameters>? OverlayExtensions { get; init; }
    /// <summary>
    /// The component extensions to update.
    /// </summary>
    public ExtensionsConfigurationType<UpdateComponentExtensionParameters>? ComponentExtensions { get; init; }
}

/// <summary>
/// Immutable container for configurations of a specific type of extension.
/// </summary>
/// <typeparam name="T">The type of extension configuration.</typeparam>
public record ExtensionsConfigurationType<T>
    where T : UpdateExtensionParameters
{
    private readonly ImmutableDictionary<ExtensionIdentifier, T> _extensions;
    /// <summary>
    /// The extension identifiers and their associated configurations.
    /// </summary>
    internal IReadOnlyDictionary<ExtensionIdentifier, T> Extensions => _extensions;
    /// <summary>
    /// Creates an empty set of configurations.
    /// </summary>
    public ExtensionsConfigurationType()
        => _extensions = ImmutableDictionary<ExtensionIdentifier, T>.Empty;
    /// <summary>
    /// For immutability.
    /// </summary>
    /// <param name="extensions">The new set of configurations to pass to the next instance.</param>
    private ExtensionsConfigurationType(ImmutableDictionary<ExtensionIdentifier, T> extensions)
        => _extensions = extensions;

    /// <summary>
    /// Sets the configuration for a specific extension.
    /// </summary>
    /// <param name="extensionId">The id of the extension.</param>
    /// <param name="extensionVersion">The version of the extension.</param>
    /// <param name="config">The configuration to update the extension to.</param>
    /// <returns>A new instance that includes the updated configuration.</returns>
    public ExtensionsConfigurationType<T> ConfigureExtension(ExtensionId extensionId, ExtensionVersion extensionVersion, T config)
        => new(_extensions.SetItem(new ExtensionIdentifier(extensionId, extensionVersion), config));

    /// <summary>
    /// <inheritdoc cref="ConfigureExtension(ExtensionId, ExtensionVersion, T)"/>
    /// </summary>
    /// <param name="installedExtension">
    /// The extension to configure. 
    /// This is returned from a <see cref="GetUserExtensionsRequest"/>.
    /// </param>
    /// <param name="config"><inheritdoc cref="ConfigureExtension(ExtensionId, ExtensionVersion, T)" path="/param[@name='config']"/></param>
    /// <returns><inheritdoc cref="ConfigureExtension(ExtensionId, ExtensionVersion, T)"/></returns>
    public ExtensionsConfigurationType<T> ConfigureExtension(InstalledExtension installedExtension, T config)
        => new(_extensions.SetItem(installedExtension.ToIdentifier(), config));
}

/// <summary>
/// Extension configuration for panel and overlay extensions.
/// </summary>
public record UpdateExtensionParameters
{
    /// <summary>
    /// Determines the extensionÅfs activation state
    /// </summary>
    public required bool Active { get; init; }
}

/// <summary>
/// Extension configuration for component extensions.
/// </summary>
public record UpdateComponentExtensionParameters
    : UpdateExtensionParameters
{
    /// <summary>
    /// The x-coordinate of the extension.
    /// </summary>
    public int? X { get; init; }
    /// <summary>
    /// The y- coordinate of the extension.
    /// </summary>
    public int? Y { get; init; }
}
