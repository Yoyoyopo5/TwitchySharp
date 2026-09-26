using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Updates an installed extension's information.
/// </summary>
/// <remarks>
/// You can update the extension's activation state, ID, and version number.
/// If you try to activate an extension under multiple extension types, the last write wins (and there is no guarantee of write order).
/// <para>
/// Requires a user access token that includes <see cref="Scope.UserEditBroadcast"/>.
/// The broadcaster who created the token is the one whose extensions will be updated.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-user-extensions">Update User Extensions</see> for more information.
/// </remarks>
public record UpdateUserExtensionsRequest
    : TwitchHelixRequest<UpdateUserExtensionsResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/users/extensions";
    public override HttpMethod Method => HttpMethod.Put;
    private UserWithScopesAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(UserId),
        ValidScopes = ImmutableHashSet.Create(Scope.UserEditBroadcast)
    };
    public UserWithScopesAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }

    /// <summary>
    /// The user id of the broadcaster whose extensions will be updated.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId UserId { get; init; }

    // Note: Unsure of how this function actually behaves. I'm assuming only included extensions are updated, but if all extensions are updated, this could delete extensions.
    // Class may need to be re-written during testing because of how crap the docs are for this one. Very strange models as well.

    // Okay, I've clarified the behavior:
    // The dictionary format basically represents a channel's extension "slots".
    // There are 3 slots for panel extensions, 1 for overlay, and 2 for component.
    // You do not need to provide each one per request, only included slots are updated.
    // You cannot new add slots, 200 OK is returned but the extension does not get activated.
    // Responses include all slot configs and are the same as GetUserActiveExtensionsResponse.
    // If you try to activate an extension that doesn't support the type, 200 OK is returned but config is not updated.
    // This endpoint has a lot of "slient failures" where 200 OK is returned for requests that should be 400 Bad Request.
    // Likewise, it acts more like a PATCH request than a PUT, where you can update individual slots without including the rest.
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
            Panel = ConvertToNumberedDict(extensions.PanelExtensions),
            Overlay = ConvertToNumberedDict(extensions.OverlayExtensions),
            Component = ConvertToNumberedDict(extensions.ComponentExtensions)
        };

    private static ImmutableDictionary<string, T>? ConvertToNumberedDict<T>(ImmutableArray<T> array)
        => new Dictionary<string, T>(array.Select((data, index) => new KeyValuePair<string, T>((index + 1).ToString(), data))) // Twitch expects 1-indexed object here.
            .ToImmutableDictionary() switch
        {
            { Count: 0 } => null,
            { } dict => dict
        };
}

/// <summary>
/// Used to properly serialize a <see cref="UpdateUserExtensionsRequest"/>.
/// </summary>
internal record UpdateUserExtensionsMaps
{
    /// <summary>
    /// The panel extensions that will be updated.
    /// </summary>
    public IReadOnlyDictionary<string, UpdateExtensionParameters?>? Panel { get; init; }
    /// <summary>
    /// The overlay extensions that will be updated.
    /// </summary>
    public IReadOnlyDictionary<string, UpdateExtensionParameters?>? Overlay { get; init; }
    /// <summary>
    /// The component extensions that will be updated.
    /// </summary>
    public IReadOnlyDictionary<string, UpdateComponentExtensionParameters?>? Component { get; init; }
}

/// <summary>
/// Contains information used to update a broadcaster's active extensions.
/// </summary>
/// <remarks>
/// Each extension type has a specific amount of slots.
/// </remarks>
public record ExtensionsConfiguration
{
    /// <summary>
    /// The panel extension slots to update.
    /// </summary>
    /// <remarks>
    /// There are 3 panel extension slots.
    /// </remarks>
    public ImmutableArray<UpdateExtensionParameters?> PanelExtensions { get; init; } = [];
    /// <summary>
    /// The overlay extension slots to update.
    /// </summary>
    /// <remarks>
    /// There is 1 overlay extension slot.
    /// </remarks>
    public ImmutableArray<UpdateExtensionParameters?> OverlayExtensions { get; init; } = [];
    /// <summary>
    /// The component extension slots to update.
    /// </summary>
    /// <remarks>
    /// There are 2 component extension slots.
    /// </remarks>
    public ImmutableArray<UpdateComponentExtensionParameters?> ComponentExtensions { get; init; } = [];
}

/// <summary>
/// Extension configuration for panel and overlay extensions.
/// </summary>
public record UpdateExtensionParameters
{
    /// <summary>
    /// Determines the status to set the extension slot to.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If <see langword="true"/>, activates an extension in the slot.
    /// The <see cref="Id"/> and <see cref="Version"/> must also be set,
    /// and the extension must be installed on the broadcaster's channel.
    /// </para>
    /// <para>
    /// If <see langword="false"/>, deactivates any extension that is currently in the slot, freeing it.
    /// The <see cref="Id"/> and <see cref="Version"/> do not need to be set.
    /// </para>
    /// </remarks>
    public bool Active { get; init; } = false;
    /// <summary>
    /// The id of the extension to activate.
    /// </summary>
    public ExtensionId? Id { get; init; }
    /// <summary>
    /// The version of the extension to activate.
    /// </summary>
    public ExtensionVersion? Version { get; init; }

    public static UpdateExtensionParameters ActivateSlot(ExtensionId extensionId, ExtensionVersion extensionVersion)
        => new()
        {
            Active = true,
            Id = extensionId,
            Version = extensionVersion
        };

    public static UpdateExtensionParameters ActivateSlot(InstalledExtension extension)
        => new()
        {
            Active = true,
            Id = extension.Id,
            Version = extension.Version
        };

    public static UpdateExtensionParameters DeactivateSlot()
        => new();
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

    public new static UpdateComponentExtensionParameters ActivateSlot(ExtensionId extensionId, ExtensionVersion extensionVersion)
        => new()
        {
            Active = true,
            Id = extensionId,
            Version = extensionVersion
        };

    public new static UpdateComponentExtensionParameters ActivateSlot(InstalledExtension extension)
        => new()
        {
            Active = true,
            Id = extension.Id,
            Version = extension.Version
        };

    public new static UpdateComponentExtensionParameters DeactivateSlot()
        => new();

    public UpdateComponentExtensionParameters WithPosition(int x, int y)
        => this with
        {
            X = x,
            Y = y
        };
}
