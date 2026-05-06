using Websocket.Client;

namespace TwitchySharp.EventSub.Websocket.Clients.Websocket.Client;

public interface IWebsocketClientFactory
{
    IWebsocketClient CreateWebsocketClient();
}

public class DefaultWebsocketClientFactory(Uri? eventSubWebsocketUri = null) : IWebsocketClientFactory
{
    private readonly Uri _defaultUri = eventSubWebsocketUri ?? new Uri(TwitchWebsocketUrl.TWITCH_WEBSOCKET_URL);
    public IWebsocketClient CreateWebsocketClient()
        => new WebsocketClient(_defaultUri);
}
