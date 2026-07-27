using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TwitchySharp.Api;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Tests.E2E;

public static class AccessTokenServiceExtensions
{
    private const string NEW_TOKEN = "new";
    private const string CACHED_TOKEN = "cached";

    public static IServiceCollection AddAccessTokens(
        this IServiceCollection sc,
        Func<IServiceProvider, IEnumerable<AccessTokenDetails>>? populateTokens = null
        )
        => sc.AddSingleton<TokenStore>(sp => new(populateTokens is null ? null : populateTokens(sp)))
            .AddAppAccessTokens()
            .AddUserAccessTokens()
            .AddExtensionJwts();

    public static IServiceCollection AddAppAccessTokens(this IServiceCollection sc)
        => sc
        .AddKeyedTransient<AccessTokenDetailsResolver<AccessTokenDetails.App>>(CACHED_TOKEN, (sp, _) => (ctx, ct) =>
        {
            sp.GetRequiredService<TokenStore>().TryGet(ctx.Identity, out AccessTokenDetails.App? details);
            return ValueTask.FromResult(details);
        })
        .AddKeyedTransient<AccessTokenDetailsResolver<AccessTokenDetails.App>>(NEW_TOKEN, (sp, _) => (ctx, ct)
            => sp.GetRequiredService<IOptions<List<ClientConfiguration>>>().Value.FirstOrDefault(config => config.ClientId == ctx.Identity.ClientId) is ClientConfiguration clientConfig
                        ? sp.GetRequiredService<ITwitchClient>().GetNewAppAccessToken(clientConfig.ClientId, clientConfig.ClientSecret, ct)
                        : ValueTask.FromResult<AccessTokenDetails.App?>(null)
            )
        .AddTransient<AccessTokenRefresher<AccessTokenDetails.App>>(sp => async (details, ct) =>
            {
                List<ClientConfiguration> clientConfigs = sp.GetRequiredService<IOptions<List<ClientConfiguration>>>().Value;
                return clientConfigs.FirstOrDefault(config => config.ClientId == details.Identity.ClientId) is not ClientConfiguration clientConfig
                    ? new AccessTokenRefreshResult.Expired<AccessTokenDetails.App>(details)
                    : await sp.GetRequiredService<ITwitchClient>().GetNewAppAccessToken(clientConfig.ClientId, clientConfig.ClientSecret, ct) switch
                    {
                        AccessTokenDetails.App newToken => new AccessTokenRefreshResult.Refreshed<AccessTokenDetails.App>(newToken),
                        _ => new AccessTokenRefreshResult.Expired<AccessTokenDetails.App>(details)
                    };
            })
        .AddTransient<Func<AccessTokenDetails.App, CancellationToken, ValueTask>>(sp => (details, ct) =>
            {
                sp.GetRequiredService<TokenStore>().AddOrUpdate(details);
                return ValueTask.CompletedTask;
            });

    public static IServiceCollection AddUserAccessTokens(this IServiceCollection sc)
        => sc
        .AddKeyedTransient<AccessTokenDetailsResolver<AccessTokenDetails.User>>(CACHED_TOKEN, (sp, _) => (ctx, ct) =>
            {
                sp.GetRequiredService<TokenStore>().TryGet(ctx.Identity, out AccessTokenDetails.User? details);
                return ValueTask.FromResult(details);
            })
        .AddTransient<AccessTokenRefresher<AccessTokenDetails.User>>(sp => (details, ct) =>
            {
                List<ClientConfiguration> clientConfigs = sp.GetRequiredService<IOptions<List<ClientConfiguration>>>().Value;
                return clientConfigs.FirstOrDefault(config => config.ClientId == details.Identity.ClientId) is ClientConfiguration clientConfig
                    ? sp.GetRequiredService<ITwitchClient>().RefreshUserAccessToken(details, clientConfig.ClientSecret, ct)
                    : ValueTask.FromResult<AccessTokenRefreshResult>(new AccessTokenRefreshResult.Expired<AccessTokenDetails.User>(details));
            })
        .AddTransient<Func<AccessTokenDetails.User, CancellationToken, ValueTask>>(sp => (details, ct) =>
            {
                sp.GetRequiredService<TokenStore>().AddOrUpdate(details);
                return ValueTask.CompletedTask;
            });

    public static IServiceCollection AddExtensionJwts(this IServiceCollection sc)
        => sc
        .AddKeyedTransient<AccessTokenDetailsResolver<AccessTokenDetails.ExtensionJwt>>(NEW_TOKEN, (sp, _) => async (ctx, ct) =>
            {
                List<ExtensionConfiguration> extensionConfigs = sp.GetRequiredService<IOptions<List<ExtensionConfiguration>>>().Value;
                return ctx.Identity is TwitchIdentity.Extension ext
                    && extensionConfigs.FirstOrDefault(config => config.ExtensionId == ext.ExtensionId) is ExtensionConfiguration extensionConfig
                    ? await ext.SignNewJwt(extensionConfig.Secret)
                    : null;
            })
        .AddKeyedTransient<AccessTokenDetailsResolver<AccessTokenDetails.ExtensionJwt>>(CACHED_TOKEN, (sp, _) => (ctx, ct) =>
            {
                sp.GetRequiredService<TokenStore>().TryGet(ctx.Identity, out AccessTokenDetails.ExtensionJwt? details);
                return ValueTask.FromResult(details);
            })
        .AddTransient<AccessTokenRefresher<AccessTokenDetails.ExtensionJwt>>(sp => async (details, ct) =>
            {
                List<ExtensionConfiguration> extensionConfigs = sp.GetRequiredService<IOptions<List<ExtensionConfiguration>>>().Value;
                return extensionConfigs.FirstOrDefault(config => config.ExtensionId == details.Identity.ExtensionId) is not ExtensionConfiguration extensionConfig
                    ? new AccessTokenRefreshResult.Expired<AccessTokenDetails.ExtensionJwt>(details)
                    : new AccessTokenRefreshResult.Refreshed<AccessTokenDetails.ExtensionJwt>(await details.Identity.SignNewJwt(extensionConfig.Secret));
            })
        .AddTransient<Func<AccessTokenDetails.ExtensionJwt, CancellationToken, ValueTask>>(sp => (details, _) =>
            {
                sp.GetRequiredService<TokenStore>().AddOrUpdate(details);
                return ValueTask.CompletedTask;
            });

    public static TwitchAuthorizationResolutionOptions AddTokens<TIdentity, TDetails>(this TwitchAuthorizationResolutionOptions options, IServiceProvider sp)
        where TIdentity : TwitchIdentity
        where TDetails : AccessTokenDetails
        => options.ConfigureIdentity<TIdentity, TDetails>(new()
        {
            GetCachedToken = sp.GetKeyedService<AccessTokenDetailsResolver<TDetails>>(CACHED_TOKEN),
            AcquireNewToken = sp.GetKeyedService<AccessTokenDetailsResolver<TDetails>>(NEW_TOKEN),
            RefreshToken = sp.GetService<AccessTokenRefresher<TDetails>>(),
            OnNewToken = sp.GetService<Func<TDetails, CancellationToken, ValueTask>>()
        });

    public static TwitchAuthorizationResolutionOptions AddTokens(this TwitchAuthorizationResolutionOptions options, IServiceProvider sp)
        => options
            .AddTokens<TwitchIdentity.Client, AccessTokenDetails.App>(sp)
            .AddTokens<TwitchIdentity.User, AccessTokenDetails.User>(sp)
            .AddTokens<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt>(sp);
}
