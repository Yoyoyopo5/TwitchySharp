using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Api;
using TwitchySharp.Api.Authentication;
using TwitchySharp.Api.Helix.Streams;
using Xunit;

namespace TwitchySharp.Tests.E2E;

public static class TwitchClientExtensions
{
    private static async Task<TwitchStream?> GetStream(
        this TestingTwitchClient client,
        UserId broadcasterId,
        CancellationToken ct
        )
        => (await client.SendAsync(new GetStreamsRequest() { UserIds = [broadcasterId] }, new TestName("get-streams"), ct))
            .Content.Data.SingleOrDefault();

    public static async Task SkipIfBroadcasterIsNotStreaming(
        this TestingTwitchClient client,
        UserId broadcasterId,
        CancellationToken ct
        )
        => Assert.SkipWhen(
            (await client.GetStream(broadcasterId, ct)) is null,
            "The broadcaster is not live."
            );

    public static TwitchClient AddClientConfiguration(
        this TwitchClient client,
        IServiceProvider sp
        )
        => client.ConfigureAsNullCoalesce<TwitchClient, ClientId?>((scope, ct) =>
            scope.ResolveOrDefault<TestName>(ct).MapAsync(testName => sp.GetAuthorizingConfigForEndpoint<ITestIdentity<TwitchIdentity>>(testName)?.Identity?.ClientId))
            .ConfigureAsNullCoalesce<TwitchClient, ClientSecret?>((scope, ct) => scope.ResolveOrDefault<ClientId?>(ct)
                .MapAsync(clientId => clientId.HasValue ? sp.GetClientConfig(clientId.Value)?.ClientSecret : default));

    public static TwitchClient AddExtensionConfiguration(
        this TwitchClient client,
        IServiceProvider sp
        )
        => client.ConfigureAsNullCoalesce<TwitchClient, ExtensionSecret?>((scope, ct) =>
            scope.ResolveOrDefault<ExtensionId?>(ct).MapAsync(extensionId => extensionId.HasValue
                ? sp.GetConfig<ExtensionConfiguration>(config => config.ExtensionId == extensionId)?.Secret
                : default))
            .ConfigureAsNullCoalesce<TwitchClient, ExtensionOwnerId?>((scope, ct) =>
            scope.ResolveOrDefault<ExtensionId?>(ct).MapAsync(extensionId => extensionId.HasValue
                ? sp.GetConfig<ExtensionConfiguration>(config => config.ExtensionId == extensionId)?.ExtensionOwnerUserId
                : default)
            );
}
