using System;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Contains a refreshed user access token and the refresh token used to refresh it.
/// </summary>
public record AccessTokenRefreshResponse
{
    /// <summary>
    /// The access token for the user. Use this when accessing API endpoints that require it.
    /// </summary>
    public required UserAccessToken AccessToken { get; init; }
    /// <summary>
    /// A token that can be used to get a new access token without requiring the user to reauthorize the app.
    /// See <see href="https://dev.twitch.tv/docs/authentication/refresh-tokens/">refresh tokens</see> for more information.
    /// </summary>
    public required RefreshToken RefreshToken { get; init; }
    /// <summary>
    /// Time until the access token needs to be refreshed.
    /// Note that a user can revoke access to an app at anytime, causing API requests to return HTTP code 401 before the token expires.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan ExpiresIn { get; init; }
    /// <summary>
    /// The <see href="https://dev.twitch.tv/docs/authentication/scopes/">authorization scopes</see> associated with the access token.
    /// </summary>
    public Scope[]? Scope { get; init; }
    /// <summary>
    /// The type of the access token. This should always be <c>bearer</c>.
    /// </summary>
    public required string TokenType { get; init; }
}
