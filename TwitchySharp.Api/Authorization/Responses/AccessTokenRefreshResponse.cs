using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Authorization.Responses;
/// <summary>
/// Contains a refreshed user access token and the refresh token used to refresh it.
/// </summary>
public record AccessTokenRefreshResponse
{
    /// <summary>
    /// The access token for the user. Use this when accessing API endpoints that require it.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string AccessToken { get; private set; } = string.Empty;
    /// <summary>
    /// A token that can be used to get a new access token without requiring the user to reauthorize the app.
    /// See <see href="https://dev.twitch.tv/docs/authentication/refresh-tokens/">refresh tokens</see> for more information.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string RefreshToken { get; private set; } = string.Empty;
    /// <summary>
    /// The <see href="https://dev.twitch.tv/docs/authentication/scopes/">authorization scopes</see> associated with the access token.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string[] Scope { get; private set; } = [];
    /// <summary>
    /// The type of the access token. This should always be bearer.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string TokenType { get; private set; } = string.Empty;
}
