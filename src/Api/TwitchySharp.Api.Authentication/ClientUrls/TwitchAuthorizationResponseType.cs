using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Authentication;

/// <summary>
/// Contains static definitions for possible authorization response types.
/// </summary>
/// <param name="Value">The string value of the response type.</param>
[Wrapper<string>]
public readonly partial record struct TwitchAuthorizationResponseType(string Value)
{
    /// <summary>
    /// Get a OpenID Connect ID token directly in the fragment portion of the redirect URI.
    /// </summary>
    /// <remarks>
    /// Valid response type for the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#implicit-grant-flow">implicit grant flow</see>.
    /// </remarks>
    public static TwitchAuthorizationResponseType IdToken { get; } = new("id_token");
    /// <summary>
    /// Get a <see cref="UserAccessToken"/> directly in the fragment portion of the redirect URI.
    /// </summary>
    /// <remarks>
    /// Valid response type for the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#implicit-grant-flow">implicit grant flow</see>.
    /// </remarks>
    public static TwitchAuthorizationResponseType Token { get; } = new("token");
    /// <summary>
    /// Get an authorization code that can be exchanged for a user access token and refresh token in a <see cref="AuthorizationCodeRequest"/>.
    /// </summary>
    /// <remarks>
    /// Only valid response type for the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#authorization-code-grant-flow">authorization code grant flow</see>.
    /// </remarks>
    internal static TwitchAuthorizationResponseType Code { get; } = new("code");
}
