namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Updates a configuration segment.
/// </summary>
/// <remarks>
/// The segment is limited to 5 KB.
/// Extensions that are active on a channel do not receive the updated configuration.
/// <br/>
/// <b>Rate Limits:</b> You may update the configuration a maximum of 20 times per minute.
/// <br/>
/// Requires a signed JSON Web Token (JWT) created by an EBS.
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>.
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>).
/// Set the role field to external and the user_id field to the user id of the user that owns the extension.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#set-extension-configuration-segment">Set Extension Configuration Segment</see> for more information.
/// </remarks>
public record SetExtensionConfigurationSegmentRequest
    : TwitchHelixRequest<SetExtensionConfigurationSegmentResponseContent>,
    IAuthenticatedTwitchRequest<TwitchRequestAuthenticationContext<TwitchIdentity.Extension>>
{
    protected override string Path => "/extensions/configurations";
    public override HttpMethod Method => HttpMethod.Put;
    private TwitchRequestAuthenticationContext<TwitchIdentity.Extension> DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.Extension(
            Configuration.ExtensionId,
            Configuration.BroadcasterId
            )
    };
    public TwitchRequestAuthenticationContext<TwitchIdentity.Extension> AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }

    public override object? ContentObject => Configuration;

    /// <summary>
    /// Data used to set the configuration.
    /// Use derived classes <see cref="SetExtensionConfigurationGlobalSegmentData"/>,
    /// <see cref="SetExtensionConfigurationDeveloperSegmentData"/>,
    /// <see cref="SetExtensionConfigurationBroadcasterSegmentData"/> for easier usage.
    /// </summary>
    public required SetExtensionConfigurationSegmentRequestData Configuration { get; init; }

    public override Func<Stream, CancellationToken, ValueTask<SetExtensionConfigurationSegmentResponseContent>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new SetExtensionConfigurationSegmentResponseContent());
}

/// <summary>
/// Contains data used to set an extension configuration segment.
/// Use derived classes <see cref="SetExtensionConfigurationGlobalSegmentData"/>, 
/// <see cref="SetExtensionConfigurationDeveloperSegmentData"/>,
/// <see cref="SetExtensionConfigurationBroadcasterSegmentData"/> for easier usage.
/// </summary>
/// <param name="Segment">
/// The segment type to configure.
/// </param>
public record SetExtensionConfigurationSegmentRequestData(ExtensionConfigurationSegmentType Segment)
{
    /// <summary>
    /// The id of the extension to update.
    /// </summary>
    public required ExtensionId ExtensionId { get; init; }
    /// <summary>
    /// The configuration segment to update.
    /// </summary>
    public ExtensionConfigurationSegmentType Segment { get; init; } = Segment;
    /// <summary>.
    /// The user id of the broadcaster that installed the extension.
    /// Include this property only if the <see cref="Segment"/> is set to <see cref="ExtensionConfigurationSegmentType.Developer"/> or <see cref="ExtensionConfigurationSegmentType.Broadcaster"/>.
    /// </summary>
    public UserId? BroadcasterId { get; protected init; }
    /// <summary>
    /// The contents of the segment.
    /// This may be in plain-text or string-encoded JSON.
    /// </summary>
    public string? Content { get; init; }
    /// <summary>
    /// The version number that identifies this definition of the segment’s data. 
    /// If not specified, the latest definition is updated.
    /// </summary>
    public ExtensionVersion? Version { get; init; }
}

/// <summary>
/// Set configuration for the <see cref="ExtensionConfigurationSegmentType.Global"/> segment.
/// </summary>
public record SetExtensionConfigurationGlobalSegmentData()
    : SetExtensionConfigurationSegmentRequestData(ExtensionConfigurationSegmentType.Global)
{
    /// <inheritdoc cref="SetExtensionConfigurationSegmentRequestData.Segment"/>
    public new ExtensionConfigurationSegmentType Segment => base.Segment;
}

/// <summary>
/// Set configuration for the <see cref="ExtensionConfigurationSegmentType.Developer"/> segment.
/// </summary>
public record SetExtensionConfigurationDeveloperSegmentData()
    : SetExtensionConfigurationSegmentRequestData(ExtensionConfigurationSegmentType.Developer)
{
    /// <summary>
    /// The user id of the broadcaster to update extension configuration data for.
    /// </summary>
    public new required UserId BroadcasterId { get => base.BroadcasterId!.Value; init => base.BroadcasterId = value; }
    /// <inheritdoc cref="SetExtensionConfigurationSegmentRequestData.Segment"/>
    public new ExtensionConfigurationSegmentType Segment => base.Segment;
}

/// <summary>
/// Set configuration for the <see cref="ExtensionConfigurationSegmentType.Broadcaster"/> segment.
/// </summary>
public record SetExtensionConfigurationBroadcasterSegmentData()
    : SetExtensionConfigurationSegmentRequestData(ExtensionConfigurationSegmentType.Broadcaster)
{
    /// <summary>
    /// The user id of the broadcaster to update extension configuration data for.
    /// </summary>
    public new required UserId BroadcasterId { get => base.BroadcasterId!.Value; init => base.BroadcasterId = value; }
    /// <inheritdoc cref="SetExtensionConfigurationSegmentRequestData.Segment"/>
    public new ExtensionConfigurationSegmentType Segment => base.Segment;
}
