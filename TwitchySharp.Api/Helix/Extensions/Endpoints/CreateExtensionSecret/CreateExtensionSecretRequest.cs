using System;
using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Creates a shared secret used to sign and verify JWT tokens.
/// </summary>
/// <remarks>
/// <para>
/// Creating a new secret removes the current secrets from service.
/// Use this function only when you are ready to use the new secret it returns.
/// </para>
/// Requires a signed JSON Web Token (JWT) created by an EBS.
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>.
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>).
/// The role field must be set to external.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-extension-secret">Create Extension Secret</see> for more information.
/// </remarks>
public record CreateExtensionSecretRequest
    : TwitchHelixRequest<CreateExtensionSecretResponse>
{
    protected override string Path => "/extensions/jwt/secrets";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchApiIdentity DefaultIdentity => ExtensionIdentity;
    public override IEnumerable<Scope> ValidScopes => [];

    /// <summary>
    /// The extension identity used for JWT authentication.
    /// </summary>
    public required ExtensionIdentity ExtensionIdentity { get; set; }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("extension_id", ExtensionId)
            .Add("delay", Delay?.TotalSeconds.ToString());

    /// <summary>
    /// The id of the extension to apply the shared secret to.
    /// </summary>
    public required ExtensionId ExtensionId { get; set; }

    /// <summary>
    /// The amount of time to delay activating the secret.
    /// </summary>
    /// <remarks>
    /// The delay should provide enough time for instances of the extension to gracefully switch over to the new secret.
    /// The minimum delay is 300 seconds (5 minutes).
    /// The default is 300 seconds.
    /// </remarks>
    public TimeSpan? Delay { get; set; }
}