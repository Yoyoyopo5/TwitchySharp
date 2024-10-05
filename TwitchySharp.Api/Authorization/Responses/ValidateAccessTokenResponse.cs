using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Contains information about a valid user access token.
/// </summary>
public record ValidateAccessTokenResponse
{
    /// <summary>
    /// The client ID of the application the user has authorized.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string ClientId { get; private set; } = string.Empty;
    /// <summary>
    /// The login (username) of the Twitch user associated with the access token.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string Login { get; private set; } = string.Empty;
    /// <summary>
    /// The authorization scopes associated with the access token.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string[] Scopes { get; private set; } = [];
    /// <summary>
    /// The user ID of the Twitch user associated with the access token.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string UserId { get; private set; } = string.Empty;
    /// <summary>
    /// Seconds until the access token must be refreshed.
    /// </summary>
    [JsonInclude, JsonRequired]
    public int ExpiresIn { get; private set; } = 0;
}
