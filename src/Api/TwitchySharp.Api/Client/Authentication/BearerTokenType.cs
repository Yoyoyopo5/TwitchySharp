using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api;

/// <summary>
/// Contains static definitions for possible Twitch bearer token types.
/// </summary>
/// <remarks>
/// Twitch uses different bearer token types for API authentication.
/// <see href="https://dev.twitch.tv/docs/authentication/#app-access-tokens">App Access Tokens</see>,
/// <see cref=""/>
/// </remarks>
/// <param name="Value"></param>
[Wrapper<string>]
public readonly partial record struct BearerTokenType(string Value)
{
    /// <summary>
    /// Used for endpoints that require a user's permission to call.
    /// </summary>
    /// <remarks>
    /// See <see href="https://dev.twitch.tv/docs/authentication/#user-access-tokens">User Access Tokens</see>
    /// for more information.
    /// </remarks>
    public static BearerTokenType UserAccessToken { get; } = new("user");
    /// <summary>
    /// Used for endpoints that aren't called on behalf of a user.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Alternatively, some user-authenticated endpoints allow for the use of app access tokens
    /// as long as the user has previously authorized the app with the correct scope.
    /// </para>
    /// See <see href="https://dev.twitch.tv/docs/authentication/#app-access-tokens">App Access Tokens</see>
    /// for more information.
    /// </remarks>
    public static BearerTokenType AppAccessToken { get; } = new("app");
    /// <summary>
    /// Used for certain endpoints that interact with Twitch extensions.
    /// </summary>
    /// <remarks>
    /// See <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>
    /// for more information.
    /// </remarks>
    public static BearerTokenType ExtensionJwt { get; } = new("extension");
}
