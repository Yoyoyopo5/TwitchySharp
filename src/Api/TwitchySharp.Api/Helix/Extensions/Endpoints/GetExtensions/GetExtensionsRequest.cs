using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Gets information about an extension.
/// </summary>
/// <remarks>
/// Requires a signed JSON Web Token (JWT) created by an EBS.
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>.
/// The signed JWT must include the role field (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>).
/// The role field must be set to external.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extensions">Get Extensions</see> for more information.
/// </remarks>
public record GetExtensionsRequest
    : TwitchHelixRequest<GetExtensionsResponse>
{
    protected override string Path => "/extensions";
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
            .Add("extension_id", ExtensionId)
            .Add("extension_version", ExtensionVersion?.ToString());

    /// <summary>
    /// The id of the extension to get.
    /// </summary>
    public required ExtensionId ExtensionId { get; init; }

    /// <summary>
    /// The version of the extension to get.
    /// </summary>
    /// <remarks>
    /// If <see langword="null"/>, it returns the latest, released version.
    /// If the extension doesn't have a released version, you must specify a version; otherwise, <see cref="GetExtensionsResponse.Data"/> is empty.
    /// </remarks>
    public ExtensionVersion? ExtensionVersion { get; init; }
}
