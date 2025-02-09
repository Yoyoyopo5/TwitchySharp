using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Contains an access and refresh token from the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#device-code-grant-flow">device code grant flow</see>.
/// </summary>
public record DeviceCodeTokenResponse
{
    /// <summary>
    /// A user access token that can be used in Twitch API requests that require it.
    /// </summary>
    public required string AccessToken { get; init; }
    /// <summary>
    /// The time in seconds until the access token becomes invalid.
    /// </summary>
    public required int ExpiresIn { get; init; }
    /// <summary>
    /// A token that can be used to get a new access token without requiring the user to reauthorize the app.
    /// </summary>
    public required string RefreshToken { get; init; }
    /// <summary>
    /// The <see href="https://dev.twitch.tv/docs/authentication/scopes/">authorization scopes</see> associated with the access token.
    /// </summary>
    public string[]? Scope { get; init; }
    /// <summary>
    /// The type of the access token. This should always be bearer.
    /// </summary>
    public required string TokenType { get; init; }
}
