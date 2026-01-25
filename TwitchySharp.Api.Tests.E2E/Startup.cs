using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Tests.E2E;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IResolveTwitchRateLimits, DefaultTwitchRateLimitResolver>();
        services.AddTransient<TwitchRateLimitingHandler>();
        services.AddHttpClient<TwitchClient>("TwitchApi")
            .AddHttpMessageHandler<TwitchRateLimitingHandler>();

        services.AddTransient<TwitchClient>();
    }
}

public interface ITwitchTokenResolver<TToken>
    where TToken : AccessToken
{
    ValueTask<TToken> GetToken();
}
