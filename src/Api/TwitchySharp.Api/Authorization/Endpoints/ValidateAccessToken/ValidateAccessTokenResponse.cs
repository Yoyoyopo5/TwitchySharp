using System;
using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Contains information about a valid user access token.
/// </summary>
public record ValidateAccessTokenResponse
{
    /// <summary>
    /// The client ID of the application the user has authorized.
    /// </summary>
    public required ClientId ClientId { get; init; }
    /// <summary>
    /// The login (username) of the Twitch user associated with the access token.
    /// </summary>
    public required string Login { get; init; }
    /// <summary>
    /// The authorization scopes associated with the access token.
    /// </summary>
    public required Scope[] Scopes { get; init; }
    /// <summary>
    /// The user ID of the Twitch user associated with the access token.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// Time until the access token must be refreshed.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan ExpiresIn { get; init; }
}
