using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api;
/// <summary>
/// A refresh token for a user access token.
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/authentication/refresh-tokens">Refresh Tokens</see> for more information.
/// </remarks>
/// <param name="Value">The string value of the refresh token.</param>
[Wrapper<string>]
public readonly partial record struct RefreshToken(string Value);
