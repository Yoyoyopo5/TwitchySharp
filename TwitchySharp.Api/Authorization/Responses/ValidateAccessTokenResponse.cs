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
    public required string ClientId { get; init; }
    /// <summary>
    /// The login (username) of the Twitch user associated with the access token.
    /// </summary>
    public required string Login { get; init; }
    /// <summary>
    /// The authorization scopes associated with the access token.
    /// </summary>
    public required string[] Scopes { get; init; }
    /// <summary>
    /// The user ID of the Twitch user associated with the access token.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// Seconds until the access token must be refreshed.
    /// </summary>
    public required int ExpiresIn { get; init; }
}
