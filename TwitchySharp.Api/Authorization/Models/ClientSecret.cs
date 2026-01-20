using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// A client secret generated for a specific app via the <see href="https://dev.twitch.tv/console">Twitch Developers Console</see>.
/// </summary>
/// <param name="Value">The string value of the secret.</param>
public readonly record struct ClientSecret(string Value) : IWrapValue<string>
{
    public static implicit operator string(ClientSecret secret)
        => secret.Value;
    public override string ToString()
        => Value;
}
