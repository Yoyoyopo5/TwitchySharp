using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Webhooks.Crypto;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore.Tests.Unit;

public class Test_TwitchEventSubWebhooksServiceExtensions
{
    // We may want to move some of this into integration testing
    // since the AddTwitchEventSubWebhooks call is doing significant
    // orchestration between different units.

    private static ServiceProvider BuildMockServiceProvider(Func<IServiceProvider, Func<ProcessWebhookRequest, ProcessWebhookRequest>>? configure = null)
        => new ServiceCollection().AddTwitchEventSubWebhooks(configure ?? (sp => process => process)).BuildServiceProvider();

    private const string CORRECT_SECRET = "super_secure_secret";

    private const string CALLBACK_VERIFICATION_JSON = """
        {
            "challenge": "fake-challenge",
            "subscription": {
                "id": "12345",
                "status": "webhook_callback_verification_pending",
                "type": "channel.chat.clear",
                "version": "1",
                "cost": 1,
                "condition": {
                    "broadcaster_user_id": "1",
                    "user_id": "1"
                },
                "transport": {
                    "method": "webhook",
                    "callback": "https://fake-callback.com"
                },
                "created_at": "2026-06-21T10:46:12.26371Z"
            }
        }
        """;

    private static DefaultHttpContext CreateStubContext()
    {
        DefaultHttpContext context = new();

        context.Request.Headers.Add(new("Twitch-Eventsub-Message-Id", new("12345")));
        context.Request.Headers.Add(new("Twitch-Eventsub-Message-Type", new(EventSubWebhookMessageType.WebhookCallbackVerification)));
        context.Request.Headers.Add(new("Twitch-Eventsub-Message-Signature", new("93c64c8f37e5d13cd95963d28b9c92d7d0c0283d343443d01e351b39d8295968")));
        context.Request.Headers.Add(new("Twitch-Eventsub-Message-Timestamp", new("1248174")));
        context.Request.Headers.Add(new("Twitch-Eventsub-Subscription-Type", new(EventSubSubscriptionType.ChannelChatClear.Type)));
        context.Request.Headers.Add(new("Twitch-Eventsub-Subscription-Version", new(EventSubSubscriptionType.ChannelChatClear.Version)));

        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(CALLBACK_VERIFICATION_JSON));

        return context;
    }

    [Fact]
    public void AddTwitchEventSubWebhooks_GetService_ReturnsHandleAspNetWebhookRequest()
    {
        ServiceProvider sp = BuildMockServiceProvider();

        HandleAspNetWebhookRequest? pipeline = sp.GetService<HandleAspNetWebhookRequest>();

        Assert.NotNull(pipeline);
    }

    [Fact]
    public async Task AddTwitchEventSubWebhooks_InvokePipeline_CallsConfiguredFunctions()
    {
        string? recievedChallenge = null;

        ServiceProvider sp = BuildMockServiceProvider(sp => process => process
            .MapCallbackVerification((subscription, challenge, ct) =>
            {
                recievedChallenge = challenge;
                return ValueTask.CompletedTask;
            })
            );

        await sp.GetRequiredService<HandleAspNetWebhookRequest>()(CreateStubContext(), TestContext.Current.CancellationToken);

        Assert.Equal("fake-challenge", recievedChallenge);
    }
}
