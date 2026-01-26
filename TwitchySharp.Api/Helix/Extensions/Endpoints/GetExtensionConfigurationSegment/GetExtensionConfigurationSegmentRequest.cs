using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

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
    private readonly HashSet<ExtensionConfigurationSegmentType> _segments = [];

    protected override string Path => "/extensions/configurations";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => TwitchApiIdentity.Default;
    public override IEnumerable<Scope> ValidScopes => [];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("extension_id", ExtensionId)
            .Add("segment", Segments.Select(x => x.Value));

    /// <summary>
    /// <inheritdoc cref="ExtensionConfigurationSegmentType.Broadcaster"/>
    /// </summary>
    /// <param name="broadcasterId"><inheritdoc cref="BroadcasterId" path="/summary"/></param>
    /// <returns>This instance.</returns>
    public GetExtensionConfigurationSegmentRequest AddBroadcaster(UserId broadcasterId)
    {
        _segments.Add(ExtensionConfigurationSegmentType.Broadcaster);
        BroadcasterId = broadcasterId;
        return this;
    }

    /// <summary>
    /// <inheritdoc cref="ExtensionConfigurationSegmentType.Developer"/>
    /// </summary>
    /// <param name="broadcasterId"><inheritdoc cref="BroadcasterId" path="/summary"/></param>
    /// <returns>This instance.</returns>
    public GetExtensionConfigurationSegmentRequest AddDeveloper(UserId broadcasterId)
    {
        _segments.Add(ExtensionConfigurationSegmentType.Developer);
        BroadcasterId = broadcasterId;
        return this;
    }

    /// <summary>
    /// <inheritdoc cref="ExtensionConfigurationSegmentType.Global"/>
    /// </summary>
    /// <returns>This instance.</returns>
    public GetExtensionConfigurationSegmentRequest AddGlobal()
    {
        _segments.Add(ExtensionConfigurationSegmentType.Global);
        return this;
    }

    /// <summary>
    /// The extension id of the extension that contains the configuration segment you want to get.
    /// </summary>
    public required ExtensionId ExtensionId { get; set; }

    /// <summary>
    /// The type of configuration segment(s) to get.
    /// </summary>
    /// <remarks>
    /// Use <see cref="AddGlobal"/>, <see cref="AddBroadcaster(UserId)"/>, and <see cref="AddDeveloper(UserId)"/> to configure.
    /// </remarks>
    public IEnumerable<ExtensionConfigurationSegmentType> Segments => _segments;

    /// <summary>
    /// The user id of the broadcaster (channel) whose extension configuration you want to get.
    /// </summary>
    /// <remarks>
    /// This parameter should be set if <see cref="Segments"/> includes <see cref="ExtensionConfigurationSegmentType.Broadcaster"/> or <see cref="ExtensionConfigurationSegmentType.Developer"/>.
    /// </remarks>
    public UserId? BroadcasterId { get; private set; }
}