using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Authorization.Responses;
/// <summary>
/// Contains an access and refresh token from the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#device-code-grant-flow">device code grant flow</see>.
/// </summary>
public class DeviceCodeTokenResponse
{
    /// <summary>
    /// A user access token that can be used in Twitch API requests that require it.
    /// </summary>
    [JsonRequired]
    public string AccessToken { get; } = string.Empty;
    /// <summary>
    /// The time in seconds until the access token becomes invalid.
    /// </summary>
    [JsonRequired]
    public int ExpiresIn { get; } = 0;
    /// <summary>
    /// A token that can be used to get a new access token without requiring the user to reauthorize the app.
    /// </summary>
    [JsonRequired]
    public string RefreshToken { get; } = string.Empty;
    /// <summary>
    /// The <see href="https://dev.twitch.tv/docs/authentication/scopes/">authorization scopes</see> associated with the access token.
    /// </summary>
    [JsonRequired]
    public string[] Scope { get; } = [];
    /// <summary>
    /// The type of the access token. This should always be bearer.
    /// </summary>
    [JsonRequired]
    public string TokenType { get; } = string.Empty;
}
