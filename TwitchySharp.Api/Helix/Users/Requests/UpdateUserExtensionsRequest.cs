using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Updates an installed extension’s information. 
/// You can update the extension’s activation state, ID, and version number.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-user-extensions">update user extensions</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserEditBroadcast"/>.
/// The broadcaster who created the token is the one whose extensions will be updated.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">
/// A user access token that includes <see cref="Scope.UserEditBroadcast"/>.
/// The broadcaster who created the token is the one whose extensions will be updated.
/// </param>
/// <param name="extensions">The extensions to update.</param>
public class UpdateUserExtensionsRequest(
    string clientId,
    string accessToken,
    ExtensionsConfiguration extensions
    )
    : HelixApiRequest<UpdateUserExtensionsResponse, UpdateUserExtensionsRequestData>(
        "/users/extensions",
        clientId,
        accessToken,
        (UpdateUserExtensionsRequestData)extensions
        )
{
    // Unsure of how this function actually behaves. I'm assuming only included extensions are updated, but if all extensions are updated, this could delete extensions.
    // Class may need to be re-written during testing because of how crap the docs are for this one. Very strange models as well.
    // I tried to reconfigure things as best as possible to make using this endpoint a little easier, but I may have made some assumptions that prove false.
    public override HttpMethod Method => HttpMethod.Put; 
}

# region INTERNAL_USE
/// <summary>
/// Used to properly serialize a <see cref="UpdateUserExtensionsRequest"/>.
/// </summary>
public record UpdateUserExtensionsRequestData
{
    /// <summary>
    /// The panel extensions that will be updated.
    /// </summary>
    public IReadOnlyDictionary<string, UserExtensionUpdate>? Panel { get; private set; }
    /// <summary>
    /// The overlay extensions that will be updated.
    /// </summary>
    public IReadOnlyDictionary<string, UserExtensionUpdate>? Overlay { get; private set; }
    /// <summary>
    /// The component extensions that will be updated.
    /// </summary>
    public IReadOnlyDictionary<string, UserComponentExtensionUpdate>? Component { get; private set; }
    /// <summary>
    /// <inheritdoc cref="UpdateUserExtensionsRequestData"/>
    /// </summary>
    /// <param name="extensions">The extension configurations to serialize.</param>
    public UpdateUserExtensionsRequestData(ExtensionsConfiguration extensions)
        => (Panel, Overlay, Component) =
        (
            extensions.PanelExtensions?.Extensions
                .Select(
                (extension, index) => new KeyValuePair<string, UserExtensionUpdate>(
                    (index + 1).ToString(),
                    extension.ToExtensionUpdate()
                    )
                ).ToImmutableDictionary(),
            extensions.OverlayExtensions?.Extensions
                .Select(
                (extension, index) => new KeyValuePair<string, UserExtensionUpdate>(
                    (index + 1).ToString(),
                    extension.ToExtensionUpdate()
                    )
                ).ToImmutableDictionary(),
            extensions.ComponentExtensions?.Extensions
                .Select(
                (extension, index) => new KeyValuePair<string, UserComponentExtensionUpdate>(
                    (index + 1).ToString(),
                    extension.ToExtensionUpdate()
                    )
                ).ToImmutableDictionary()
        );

    public static explicit operator UpdateUserExtensionsRequestData(ExtensionsConfiguration extensions)
        => new UpdateUserExtensionsRequestData(extensions);
}

/// <summary>
/// Used to get a unique signature for an extension.
/// Includes both the extension id and version.
/// </summary>
/// <param name="Id">The id of the extension.</param>
/// <param name="Version">The version of the extension.</param>
internal record ExtensionIdentifier(string Id, string Version);

/// <summary>
/// Used to serialize an extension configuration.
/// </summary>
/// /// <param name="Id">The id of the extension.</param>
/// <param name="Version">The version of the extension.</param>
public record UserExtensionUpdate(string Id, string Version)
    : UpdateExtensionParameters();

/// <summary>
/// Used to serialize an extension configuration.
/// </summary>
/// /// <param name="Id">The id of the extension.</param>
/// <param name="Version">The version of the extension.</param>
public record UserComponentExtensionUpdate(string Id, string Version)
    : UpdateComponentExtensionParameters();

internal static class InstalledExtensionExtensions
{
    /// <summary>
    /// Creates an <see cref="ExtensionIdentifier"/> for an <see cref="InstalledExtension"/> as returned by <see cref="GetUserExtensionsRequest"/>.
    /// </summary>
    /// <param name="extension">The extension to create an indentifier for.</param>
    public static ExtensionIdentifier ToIdentifier(this InstalledExtension extension)
        => new ExtensionIdentifier(extension.Id, extension.Version);
}

internal static class KeyValuePairExtensions
{
    /// <summary>
    /// Converts a consumer-created extension configuration into a the format Twitch API expects.
    /// </summary>
    public static UserExtensionUpdate ToExtensionUpdate(this KeyValuePair<ExtensionIdentifier, UpdateExtensionParameters> extensionConfig)
        => new UserExtensionUpdate(extensionConfig.Key.Id, extensionConfig.Key.Version)
        {
            Active = extensionConfig.Value.Active
        };

    /// <summary>
    /// Converts a consumer-created extension configuration into a the format Twitch API expects.
    /// </summary>
    public static UserComponentExtensionUpdate ToExtensionUpdate(this KeyValuePair<ExtensionIdentifier, UpdateComponentExtensionParameters> extensionConfig)
        => new UserComponentExtensionUpdate(extensionConfig.Key.Id, extensionConfig.Key.Version)
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
    public ExtensionsConfigurationType<UpdateExtensionParameters>? PanelExtensions { get; set; }
    /// <summary>
    /// The overlay extensions to update.
    /// </summary>
    public ExtensionsConfigurationType<UpdateExtensionParameters>? OverlayExtensions { get; set; }
    /// <summary>
    /// The component extensions to update.
    /// </summary>
    public ExtensionsConfigurationType<UpdateComponentExtensionParameters>? ComponentExtensions { get; set; }    
}

/// <summary>
/// Immutable container for configurations of a specific type of extension.
/// </summary>
/// <typeparam name="T">The type of extension configuration.</typeparam>
public record ExtensionsConfigurationType<T>
    where T : UpdateExtensionParameters
{
    private ImmutableDictionary<ExtensionIdentifier, T> _extensions;
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
    public ExtensionsConfigurationType<T> ConfigureExtension(string extensionId, string extensionVersion, T config)
        => new ExtensionsConfigurationType<T>(_extensions.SetItem(new ExtensionIdentifier(extensionId, extensionVersion), config));

    /// <summary>
    /// <inheritdoc cref="ConfigureExtension(string, string, T)"/>
    /// </summary>
    /// <param name="installedExtension">
    /// The extension to configure. 
    /// This is returned from a <see cref="GetUserExtensionsRequest"/>.
    /// </param>
    /// <param name="config"><inheritdoc cref="ConfigureExtension(string, string, T)" path="/param[@name='config']"/></param>
    /// <returns><inheritdoc cref="ConfigureExtension(string, string, T)"/></returns>
    public ExtensionsConfigurationType<T> ConfigureExtension(InstalledExtension installedExtension, T config)
        => new ExtensionsConfigurationType<T>(_extensions.SetItem(installedExtension.ToIdentifier(), config));
}

/// <summary>
/// Extension configuration for panel and overlay extensions.
/// </summary>
public record UpdateExtensionParameters
{
    /// <summary>
    /// Determines the extension’s activation state
    /// </summary>
    public required bool Active { get; set; }
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
    public int? X { get; set; }
    /// <summary>
    /// The y- coordinate of the extension.
    /// </summary>
    public int? Y { get; set; }
}
