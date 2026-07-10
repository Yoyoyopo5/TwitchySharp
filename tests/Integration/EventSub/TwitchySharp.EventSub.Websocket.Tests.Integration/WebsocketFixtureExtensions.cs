using Microsoft.Extensions.DependencyInjection;
using TwitchySharp.EventSub.Websocket.Clients;
using TwitchySharp.EventSub.Websocket.Functional;

namespace TwitchySharp.EventSub.Websocket.Tests.Integration;

public static class WebsocketFixtureExtensions
{
    public record ConnectionScope(
        WebsocketFixture Fixture,
        IServiceScope ServiceScope,
        StopWebsocketClient Stop,
        TestHandler Handler,
        CancellationToken Ct) : IAsyncDisposable
    {
        public async ValueTask DisposeAsync()
        {
            ServiceScope.Dispose();
            await Stop(Ct);
        }

        public async Task SendMessage<TPayload>(EventSubWebsocketMessage<TPayload> message, CancellationToken ct)
        {
            if (Handler.Session is null)
                throw new InvalidOperationException("The handler does not have an open session");
            await Fixture.SendTestMessageAsync(Handler.Session.Id, message, ct);
        }
    }

    public static async Task<ConnectionScope> CreateTestConnection(this WebsocketFixture fixture, CancellationToken ct)
    {
        IServiceScope scope = fixture.NewServiceScope();
        StopWebsocketClient stop = await fixture.StartNewClient(scope.ServiceProvider, ct);
        TestHandler handler = scope.ServiceProvider.GetRequiredService<TestHandler>();
        return new ConnectionScope(fixture, scope, stop, handler, ct);
    } 
}
