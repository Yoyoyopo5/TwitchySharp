using System.Collections.Generic;
using System.Net.Http;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Use this request to obtain a new user access token for a previously authorized user.
/// </summary>
/// <remarks>
/// Requires a refresh token obtained when authorizing a user using the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#authorization-code-grant-flow">authorization code grant flow</see>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/authentication/refresh-tokens/">refresh tokens</see> for more information.
/// </remarks>
public record AccessTokenRefreshRequest
    : TwitchAuthorizationRequest<AccessTokenRefreshResponse>
{
    public override HttpMethod Method => HttpMethod.Post;
    protected override string Path => "/token";
    public override HttpContent? Content
        => new FormUrlEncodedContent(
            new Dictionary<string, string>()
            {
                { "client_id", ClientId },
                { "client_secret", ClientSecret },
                { "refresh_token", RefreshToken },
                { "grant_type", "refresh_token" }
            });

    /// <summary>
    /// The client id of the application that the user originally authorized.
    /// </summary>
    public required ClientId ClientId { get; init; }
    /// <summary>
    /// The client secret of the application that the user originally authorized.
    /// </summary>
    public required ClientSecret ClientSecret { get; init; }
    /// <summary>
    /// The refresh token for the user access token.
    /// </summary>
    public required RefreshToken RefreshToken { get; init; }
}
