using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwitchySharp.Api.Tests.E2E;
using TwitchySharp.Tests.E2E;

[assembly: AssemblyFixture(typeof(TwitchClientFixture))]

namespace TwitchySharp.Api.Tests.E2E;

public sealed class TwitchClientFixture : TwitchTestApplication
{
    protected override HostApplicationBuilder ConfigureTwitchApplication(HostApplicationBuilder builder)
    {
        builder.Configuration.AddUserSecrets<TwitchClientFixture>();

        builder.Services
            .AddTransient<ResponseRecorder>()
            .AddHttpClient<TwitchClient>()
            .AddHttpMessageHandler<ResponseRecorder>();

        return builder;
    }
}
