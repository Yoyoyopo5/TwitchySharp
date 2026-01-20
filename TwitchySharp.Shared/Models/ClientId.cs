using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;
/// <summary>
/// An id representing a specific app registered via the <see href="https://dev.twitch.tv/console">Twitch Developers Console</see>.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<ClientId, string>))]
public readonly record struct ClientId(string Value) : IWrapValue<string>
{
    public static implicit operator string(ClientId id)
        => id.Value;
    public override string ToString()
        => Value;
}
