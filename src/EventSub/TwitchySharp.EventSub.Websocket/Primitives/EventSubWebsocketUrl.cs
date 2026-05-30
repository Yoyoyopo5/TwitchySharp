using TwitchySharp.Infrastructure.Functional;
using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Websocket;

/// <summary>
/// A Twitch EventSub Websocket url.
/// </summary>
/// <param name="Value">The string value of the url.</param>
[Wrapper<string>]
public readonly partial record struct EventSubWebsocketUrl(string Value)
{
    public Validation<Uri> ToUri(UriCreationOptions options = default) => Uri.TryCreate(Value, options, out Uri? uri) ? uri : new Error("Invalid Uri");
}
