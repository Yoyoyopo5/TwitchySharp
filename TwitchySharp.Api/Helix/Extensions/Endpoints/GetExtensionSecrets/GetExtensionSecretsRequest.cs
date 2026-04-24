using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Gets an extension's list of shared secrets.
/// </summary>
/// <remarks>
/// Requires a signed JSON Web Token (JWT) created by an EBS.
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>.
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>).
/// The role field must be set to external.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extension-secrets">Get Extension Secrets</see> for more information.
/// </remarks>
public record GetExtensionSecretsRequest
    : TwitchHelixRequest<GetExtensionSecretsResponse>
{
    protected override string Path => "/extensions/jwt/secrets";
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
            .Add("extension_id", ExtensionId);

    /// <summary>
    /// The id of the extension whose shared secrets you want to get.
    /// </summary>
    public required ExtensionId ExtensionId { get; init; }
}
