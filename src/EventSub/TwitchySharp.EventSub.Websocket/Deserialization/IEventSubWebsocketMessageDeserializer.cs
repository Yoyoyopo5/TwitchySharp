using TwitchySharp.EventSub.Websocket.Messages;

namespace TwitchySharp.EventSub.Websocket.Deserialization;
/// <summary>
/// Defines methods for interpreting text data from a Twitch EventSub WebSocket session.
/// See <see cref="DefaultWebsocketMessageDeserializer"/>.
/// </summary>
public interface IEventSubWebsocketMessageDeserializer
{
    /// <summary>
    /// Deserialize a text message received from the WebSocket session.
    /// </summary>
    /// <param name="message">The received message as a <see cref="Stream"/>.</param>
    /// <returns>The deserialized message.</returns>
    ValueTask<IEventSubWebsocketMessage> DeserializeMessage(Stream message, CancellationToken ct = default);
}
