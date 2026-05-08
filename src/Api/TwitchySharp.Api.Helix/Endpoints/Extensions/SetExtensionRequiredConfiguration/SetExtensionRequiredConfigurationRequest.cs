namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Updates the extension's required_configuration string.
/// </summary>
/// <remarks>
/// Use this endpoint if your extension requires the broadcaster to configure the extension before activating it (to require configuration, you must select Custom/My Own Service in Extension <see href="https://dev.twitch.tv/docs/extensions/life-cycle/#capabilities">Capabilities</see>).
/// For more information, see <see href="https://dev.twitch.tv/docs/extensions/building#required-configurations">Required Configurations</see> and <see href="https://dev.twitch.tv/docs/extensions/building#setting-required-configuration-with-the-configuration-service-optional">Setting Required Configuration</see>.
/// <br/>
/// Requires a signed JSON Web Token (JWT) created by an EBS.
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>.
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>).
/// Set the role field to external and the user_id field to the user id of the user that owns the extension.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#set-extension-required-configuration">Set Extension Required Configuration</see> for more information.
/// </remarks>
public record SetExtensionRequiredConfigurationRequest
    : TwitchHelixRequest<SetExtensionRequiredConfigurationResponse>
{
    protected override string Path => "/extensions/required_configuration";
    public override HttpMethod Method => HttpMethod.Put;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.Extension(
            ExtensionOwnerId,
            BroadcasterId,
            Configuration.ExtensionId
            )
    };

    /// <summary>
    /// The user id of the owner of the extension.
    /// </summary>
    public required UserId ExtensionOwnerId { get; init; }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);
    public override object? ContentObject => Configuration;

    /// <summary>
    /// The user id of the broadcaster with the extension to set required configuration for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The data used to set the required configuration setting.
    /// </summary>
    public required SetExtensionRequiredConfigurationRequestData Configuration { get; init; }

    protected override ValueTask<SetExtensionRequiredConfigurationResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
        => ValueTask.FromResult(new SetExtensionRequiredConfigurationResponse());
}

/// <summary>
/// Contains data used to set an extension's required configuration setting.
/// </summary>
public record SetExtensionRequiredConfigurationRequestData
{
    /// <summary>
    /// The id of the extension to update.
    /// </summary>
    public required ExtensionId ExtensionId { get; init; }
    /// <summary>
    /// The version of the extension to update.
    /// </summary>
    public required ExtensionVersion ExtensionVersion { get; init; }
    /// <summary>
    /// The required_configuration string to use with the extension.
    /// </summary>
    public required string RequiredConfiguration { get; init; }
}
