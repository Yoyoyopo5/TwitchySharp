using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Webhooks.Responses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore.Tests.Integration;

public class Program { }

public class WebhooksFixture : WebApplicationFactory<Program>
{
    private const string FAKE_SECRET = "super_secure_secret";
    private const string FAKE_PATH = "/test-webhooks";
    public TestHandler Handler => Services.GetRequiredService<IWebhookEventSubHandler>() as TestHandler ?? throw new InvalidOperationException("The IWebhookEventSubHandler is not registered as TestHandler.");
    public string Secret => Services.GetRequiredService<IConfiguration>().GetRequiredSection("TwitchWebhooks").GetValue<string>("Secret") ?? string.Empty;
    public string Path => Services.GetRequiredService<IConfiguration>().GetRequiredSection("TwitchWebhooks").GetValue<string>("Path") ?? string.Empty;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((ctx, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "TwitchWebhooks:Secret", FAKE_SECRET },
                    { "TwitchWebhooks:Path", FAKE_PATH }
                });
        });
        builder.ConfigureServices((ctx, s) =>
        {
            s.AddSingleton<IWebhookEventSubHandler, TestHandler>();
            s.AddTwitchEventSubWebhooksVerification(options =>
            {
                options.Secret = ctx.Configuration.GetRequiredSection("TwitchWebhooks").GetValue<string>("Secret") ?? string.Empty;
            });
            s.AddTwitchEventSubWebhooks();
        });
        builder.Configure(app =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapTwitchWebhooks(app.ApplicationServices.GetRequiredService<IConfiguration>().GetRequiredSection("TwitchWebhooks").GetValue<string>("Path") ?? "/");
            });
        });
    }

    protected override IHostBuilder? CreateHostBuilder()
        => Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseTestServer();
            });
}

public class TestHandler : IWebhookEventSubHandler
{
    public EventSubSubscription? ActiveSubscription { get; set; }
    public IEventSubNotification? LastNotification { get; set; }

    public ValueTask OnNotified(IEventSubNotification notification, CancellationToken ct = default)
    {
        LastNotification = notification;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnSubscriptionRevoked(EventSubSubscription revokedSubscription, CancellationToken ct = default)
    {
        if (ActiveSubscription is null)
            return ValueTask.CompletedTask;
        if (ActiveSubscription.Id == revokedSubscription.Id)
            ActiveSubscription = null;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnCallbackVerification(EventSubSubscription newSubscription, string challenge, CancellationToken ct = default)
    {
        ActiveSubscription = newSubscription;
        return ValueTask.CompletedTask;
    }
}
