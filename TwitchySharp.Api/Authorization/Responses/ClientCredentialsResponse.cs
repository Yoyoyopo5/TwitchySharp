using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Contains an app access token. 
/// See <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#client-credentials-grant-flow">client credentials code flow</see> for more information.
/// </summary>
public record ClientCredentialsResponse
{
    /// <summary>
    /// The app access token. Use this to authorize API requests that require an app access token.
    /// </summary>
    public required string AccessToken { get; init; }
    /// <summary>
    /// Time in seconds that the access token will remain valid.
    /// </summary>
    public required int ExpiresIn { get; init; }
    /// <summary>
    /// The type of access token. This should always be bearer.
    /// </summary>
    public required string TokenType { get; init; }
}
