using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Response containing access and refresh tokens for a user that authorized the app.
/// See <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#authorization-code-grant-flow">authorization code grant flow</see> for more information.
/// </summary>
public record AuthorizationCodeResponse
{
    /// <summary>
    /// The access token for the user. Use this when accessing API endpoints that require it.
    /// </summary>
    public required string AccessToken { get; init; } = string.Empty;
    /// <summary>
    /// Time in seconds until the access token needs to be refreshed.
    /// Note that a user can revoke access to an app at anytime, causing API requests to return HTTP code 401 before the token expires.
    /// </summary>
    public required int ExpiresIn { get; init; }
    /// <summary>
    /// A token that can be used to get a new access token without requiring the user to reauthorize the app.
    /// See <see href="https://dev.twitch.tv/docs/authentication/refresh-tokens/">refresh tokens</see> for more information.
    /// </summary>
    public required string RefreshToken { get; init; }
    /// <summary>
    /// The <see href="https://dev.twitch.tv/docs/authentication/scopes/">authorization scopes</see> associated with the access token.
    /// </summary>
    public required string[] Scope { get; init; }
    /// <summary>
    /// The type of the access token. This should always be bearer.
    /// </summary>
    public required string TokenType { get; init; }

    /// <summary>
    /// An OIDC id token in the form of a base-64 encoded JWT containing data about the authorizing user.
    /// </summary>
    public string? IdToken { get; init; }
}

public static class AuthorizationCodeResponseExtensions
{
    /// <summary>
    /// Converts the <see cref="IdToken"/> property into a <see cref="JsonWebToken"/> if it exists.
    /// </summary>
    public static JsonWebToken? GetJwt(this AuthorizationCodeResponse authResponse)
        => authResponse.IdToken is null ? null : new(authResponse.IdToken);

    /// <summary>
    /// Converts the <see cref="IdToken"/> property into a <see cref="TwitchOidc"/> if it exists.
    /// </summary>
    public static OneOf<TwitchOidc, Exception>? GetOidc(this AuthorizationCodeResponse authResponse)
        => authResponse.IdToken is null ? (OneOf<TwitchOidc, Exception>?)null : TwitchOidc.FromJsonWebToken(authResponse.GetJwt()!); // Can safely ignore null here because it will only be null if the IdToken is null.
}
