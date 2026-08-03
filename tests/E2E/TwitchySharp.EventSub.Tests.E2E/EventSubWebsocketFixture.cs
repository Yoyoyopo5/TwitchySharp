using System.Net.WebSockets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TwitchySharp.Api;
using TwitchySharp.EventSub.Tests.E2E;
using TwitchySharp.EventSub.Websocket;
using TwitchySharp.EventSub.Websocket.Clients;
using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.EventSub.Websocket.Serialization;
using TwitchySharp.Tests.E2E;
using Websocket.Client;

[assembly: AssemblyFixture(typeof(EventSubWebsocketFixture))]

namespace TwitchySharp.EventSub.Tests.E2E;

public sealed class EventSubWebsocketFixture : TwitchTestApplication
{
    protected override HostApplicationBuilder ConfigureTwitchApplication(HostApplicationBuilder builder)
    {
        builder.Configuration
            .AddUserSecrets<EventSubWebsocketFixture>();

        builder.Services
            .AddHttpClient<TwitchClient>();

        builder.Services
            .AddTransient<ProcessWebsocketMessage>(sp => WebsocketMessageDeserializer.Create());

        builder.Services
            .AddTransient<StartEventSubWebsocketClient>(sp => EventSubWebsocketClient.Create(ctx =>
            {
                WebsocketClient ws = new(ctx.Uri)
                {
                    IsTextMessageConversionEnabled = false,
                    IsStreamDisposedAutomatically = false
                };
                ws.MessageReceived.Subscribe(message =>
                {
                    if (message.Stream is null)
                    {
                        TestContext.Current.AddWarning("The websocket client message Stream was null.");
                        return;
                    }

                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            await ctx.OnMessage(message.Stream, TestContext.Current.CancellationToken);
                        }
                        catch (Exception ex)
                        {
                            TestContext.Current.AddAttachment("pipeline-exception", ex.ToString());
                            TestContext.Current.CancelCurrentTest(); // We may not need this.
                        }
                    });
                });

                return async ct =>
                {
                    await ws.StartOrFail();
                    return async ct => await ws.StopOrFail(WebSocketCloseStatus.NormalClosure, "Connection closed."); ;
                };
            }).WithReconnects(ex => TestContext.Current.AddWarning($"Client reconnect exception: {ex}")));

        return builder;
    }
}

public static class WebsocketFixtureExtensions
{
    public static Task<StopWebsocketClient> StartWebsocketClient(
        this EventSubWebsocketFixture fixture,
        Func<ProcessWebsocketMessage, ProcessWebsocketMessage> configurePipeline,
        CancellationToken ct = default
        )
        => fixture.ApplicationHost.Services.GetRequiredService<StartEventSubWebsocketClient>()(
            configurePipeline(fixture.ApplicationHost.Services.GetRequiredService<ProcessWebsocketMessage>()),
            new("wss://eventsub.wss.twitch.tv/ws"),
            ct
            );
}
