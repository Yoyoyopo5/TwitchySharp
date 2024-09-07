using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Authorization.Responses;
/// <summary>
/// Contains information about a valid user access token.
/// </summary>
public class ValidateAccessTokenResponse
{
    /// <summary>
    /// The client ID of the application the user has authorized.
    /// </summary>
    [JsonRequired]
    public string ClientId { get; } = string.Empty;
    /// <summary>
    /// The login (username) of the Twitch user associated with the access token.
    /// </summary>
    [JsonRequired]
    public string Login { get; } = string.Empty;
    /// <summary>
    /// The authorization scopes associated with the access token.
    /// </summary>
    [JsonRequired]
    public string[] Scopes { get; } = [];
    /// <summary>
    /// The user ID of the Twitch user associated with the access token.
    /// </summary>
    [JsonRequired]
    public string UserId { get; } = string.Empty;
    /// <summary>
    /// Seconds until the access token must be refreshed.
    /// </summary>
    [JsonRequired]
    public int ExpiresIn { get; } = 0;
}
