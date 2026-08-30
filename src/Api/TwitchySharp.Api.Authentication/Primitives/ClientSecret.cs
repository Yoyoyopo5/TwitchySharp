using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Authentication;
/// <summary>
/// A client secret generated for a specific app via the <see href="https://dev.twitch.tv/console">Twitch Developers Console</see>.
/// </summary>
/// <param name="Value">The string value of the secret.</param>
[Wrapper<string>]
public readonly partial record struct ClientSecret(string Value);
