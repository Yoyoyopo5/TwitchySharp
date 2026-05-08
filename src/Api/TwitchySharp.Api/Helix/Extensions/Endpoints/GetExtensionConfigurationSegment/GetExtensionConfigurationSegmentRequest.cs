using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Gets the specified configuration segment from the specified extension.
/// </summary>
/// <remarks>
/// <b>Rate Limits:</b> You may retrieve each segment a maximum of 20 times per minute.
/// <br/>
/// Requires a signed JSON Web Token (JWT) created by an EBS.
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>.
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>).
/// Set the role field to external and the user_id field to the user id of the user that owns the extension.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extension-configuration-segment">Get Extension Configuration Segment</see> for more information.
/// </remarks>
public record GetExtensionConfigurationSegmentRequest
    : TwitchHelixRequest<GetExtensionConfigurationSegmentResponse>
{
    protected override string Path => "/extensions/configurations";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = ExtensionIdentity
    };

    /// <summary>
    /// The extension identity used for JWT authentication.
    /// </summary>
    public required TwitchIdentity.Extension ExtensionIdentity { get; init; }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("extension_id", ExtensionId)
            .Add("segment", Segments.Select(x => x.Value));

    /// <summary>
    /// <inheritdoc cref="ExtensionConfigurationSegmentType.Broadcaster"/>
    /// </summary>
    /// <param name="broadcasterId"><inheritdoc cref="BroadcasterId" path="/summary"/></param>
    /// <returns>A new request with the broadcaster segment added.</returns>
    public GetExtensionConfigurationSegmentRequest WithBroadcaster(UserId broadcasterId)
        => this with
        {
            Segments = Segments.Add(ExtensionConfigurationSegmentType.Broadcaster),
            BroadcasterId = broadcasterId
        };

    /// <summary>
    /// <inheritdoc cref="ExtensionConfigurationSegmentType.Developer"/>
    /// </summary>
    /// <param name="broadcasterId"><inheritdoc cref="BroadcasterId" path="/summary"/></param>
    /// <returns>A new request with the developer segment added.</returns>
    public GetExtensionConfigurationSegmentRequest WithDeveloper(UserId broadcasterId)
        => this with
        {
            Segments = Segments.Add(ExtensionConfigurationSegmentType.Developer),
            BroadcasterId = broadcasterId
        };

    /// <summary>
    /// <inheritdoc cref="ExtensionConfigurationSegmentType.Global"/>
    /// </summary>
    /// <returns>A new request with the global segment added.</returns>
    public GetExtensionConfigurationSegmentRequest WithGlobal()
        => this with { Segments = Segments.Add(ExtensionConfigurationSegmentType.Global) };

    /// <summary>
    /// The extension id of the extension that contains the configuration segment you want to get.
    /// </summary>
    public required ExtensionId ExtensionId { get; init; }

    /// <summary>
    /// The type of configuration segment(s) to get.
    /// </summary>
    /// <remarks>
    /// Use <see cref="WithGlobal"/>, <see cref="WithBroadcaster(UserId)"/>, and <see cref="WithDeveloper(UserId)"/> to configure.
    /// </remarks>
    public ImmutableHashSet<ExtensionConfigurationSegmentType> Segments { get; private init; } = [];

    /// <summary>
    /// The user id of the broadcaster (channel) whose extension configuration you want to get.
    /// </summary>
    /// <remarks>
    /// This parameter should be set if <see cref="Segments"/> includes <see cref="ExtensionConfigurationSegmentType.Broadcaster"/> or <see cref="ExtensionConfigurationSegmentType.Developer"/>.
    /// </remarks>
    public UserId? BroadcasterId { get; private init; }
}
