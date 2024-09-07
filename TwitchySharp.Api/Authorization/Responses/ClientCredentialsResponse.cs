using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Authorization.Responses;
/// <summary>
/// Contains an app access token. 
/// See <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#client-credentials-grant-flow">client credentials code flow</see> for more information.
/// </summary>
public class ClientCredentialsResponse
{
    /// <summary>
    /// The app access token. Use this to authorize API requests that require an app access token.
    /// </summary>
    [JsonRequired]
    public string AccessToken { get; } = string.Empty;
    /// <summary>
    /// Time in seconds that the access token will remain valid.
    /// </summary>
    [JsonRequired]
    public int ExpiresIn { get; } = 0;
    /// <summary>
    /// The type of access token. This should always be bearer.
    /// </summary>
    [JsonRequired]
    public string TokenType { get; } = string.Empty;
}
