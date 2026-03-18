using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api;
/// <summary>
/// A refresh token for a user access token.
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/authentication/refresh-tokens">Refresh Tokens</see> for more information.
/// </remarks>
/// <param name="Value">The string value of the refresh token.</param>
[JsonConverter(typeof(WrapperJsonConverter<RefreshToken, string>))]
public readonly record struct RefreshToken(string Value) : IWrapValue<string>
{
    public static implicit operator string(RefreshToken secret)
        => secret.Value;
    public override string ToString()
        => Value;
}
