using System;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Contains an access and refresh token from the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#device-code-grant-flow">device code grant flow</see>.
/// </summary>
public record DeviceCodeTokenResponse
{
    /// <summary>
    /// A user access token that can be used in Twitch API requests that require it.
    /// </summary>
    public required UserAccessToken AccessToken { get; init; }
    /// <summary>
    /// The time until the access token becomes invalid.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan ExpiresIn { get; init; }
    /// <summary>
    /// A token that can be used to get a new access token without requiring the user to reauthorize the app.
    /// </summary>
    public required RefreshToken RefreshToken { get; init; }
    /// <summary>
    /// The <see href="https://dev.twitch.tv/docs/authentication/scopes/">Authorization Scopes</see> associated with the <see cref="AccessToken"/>.
    /// </summary>
    public Scope[]? Scope { get; init; }
    /// <summary>
    /// The type of the access token. This should always be <c>bearer</c>.
    /// </summary>
    public required string TokenType { get; init; }
}
