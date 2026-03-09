using Microsoft.Extensions.Logging;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution;

internal record TwitchRequestContext // Need to move this to Api project.
{
    public required TwitchRequest Request { get; init; }
    public ClientId? ClientId { get; init; }
    public AccessToken? BearerToken { get; init; }

    public static implicit operator TwitchRequestContext(TwitchRequest request)
      => new() { Request = request };
}

internal delegate ValueTask<TwitchResponse> TwitchRequestHandler(TwitchRequestContext context, CancellationToken ct = default);

public record TwitchClientBuilder // Need to move this to Api project.
    : MiddlewarePipelineBuilder<TwitchRequestHandler>;

public static class TwitchClientBuilderExtensions
{
    public static TwitchClientBuilder UseAuthorizationResolution(this TwitchClientBuilder builder,
        Func<TwitchAuthorizationResolutionOptions> configure
        )
    {
        TwitchAuthorizationResolutionOptions config = configure();
        return (TwitchClientBuilder)builder.Use(next => async (context, ct) => await next(context.Request switch
        {
            IRequireAuthorization requiresAuthorization => context with
            {
                ClientId = await DefaultClientIdResolver(config)(requiresAuthorization, ct),
                BearerToken = await DefaultBearerTokenResolver(config)(requiresAuthorization, ct)
            },
            _ => context
        }, ct));
    }


    // TODO: Remove, absorb into options record
    private readonly static Func<TwitchAuthorizationResolutionOptions, ClientIdResolver> DefaultClientIdResolver =
        config => new MiddlewarePipelineBuilder<ClientIdResolver>()
        .Use(ClientIdentityResolution.UseConfiguredClientId)
        .Finally(config.ResolveClientId ?? ClientIdentityResolution.UseFallbackClientId(null));

    // TODO: Remove, absord into options record
    private readonly static Func<TwitchAuthorizationResolutionOptions, BearerTokenResolver<IRequireAuthorization>> DefaultBearerTokenResolver =
        config => new MiddlewarePipelineBuilder<BearerTokenResolver<IRequireAuthorization>>()
        .Use(BearerTokenResolution.UseOverrideToken)
        .Use(async (request, ct)  
                => config.IdentityConfigs.GetValueOrDefault(request.Identity.GetType()) switch 
                    {
                        ITokenResolutionOptions identityConfig => await new MiddlewarePipelineBuilder<AccessTokenDetailsResolver<object?>>()
                            // Write new tokens
                            .Use(identityConfig.TokenStore switch 
                                {
                                    { } store => ,
                                    _ => next => (key, ct) => next(key, ct) // Noop
                                }
                                )
                            // Proactive refresh
                            .Use(identityConfig.Refresher switch 
                                {
                                    { } refresher =>  
                                    TokenDetailsResolution.UseRefresh<object?, IAccessTokenDetails>(
                                
                                        new MiddlewarePipelineBuilder<AccessTokenRefresher<IAccessTokenDetails>>()
                                        .Use(next => new AccessTokenRefresher<IAccessTokenDetails>(ThreadSafety.Lazily<IAccessTokenDetails, IAccessTokenDetails, AccessTokenRefreshResult>(key => key)((details, ct) => next(details, ct))))
                                        .Finally(refresher)),
                                            // TODO: Move this to an extension on IdentityOptions
                                            // .Finally(TokenRefreshing.CreateUserAccessTokenRefresher<UserAccessTokenDetails>(
                                            // config.SecretResolver,
                                            // config.AuthorizationClient,
                                            // config.FallbackClientIdResolver is null ? null : async _ => await config.FallbackClientIdResolver(request, ct),
                                            // config.LoggerFactory?.CreateLogger("RefreshUserAccessToken")
                                            // )
                                    _ => next => (key, ct) => next(key, ct) // Noop
                                }
                                )
                            // Get from store
                            .Finally(async (key, ct) => AccessTokenDetailsResolutionResult.FromDetails(identityConfig.TokenStore switch
                                {
                                    { } store => await store.GetTokenDetails(key, ct),
                                    _ => null
                                }))(identityConfig.SelectKey(request), ct) switch // Execute and extract token 
                                {
                                    IHaveAccessTokenDetails<IAccessTokenDetails> available => available.AccessTokenDetails.AccessToken,
                                    _ => null
                                }
                                ,
                        _ => null
                    }
                )
        .Use(async (request, ct)
                => request.Identity switch
                {

                    UserIdentity userIdentity => await new MiddlewarePipelineBuilder<AccessTokenDetailsResolver<UserAccessTokenKey>>()
                        // Get Token from Store => Proactive Refresh => Write New Token to Store
                        // Write New Tokens
                        .Use(TokenStore.WriteNewTokens<UserAccessTokenKey>(
                            
                            ))
                        // Proactive Refresh
                        .Use(TokenDetailsResolution.UseRefresh<UserAccessTokenKey, UserAccessTokenDetails>(
                            new MiddlewarePipelineBuilder<AccessTokenRefresher<UserAccessTokenDetails>>()
                                .Use(next => new AccessTokenRefresher<UserAccessTokenDetails>(ThreadSafety.Lazily<UserAccessTokenDetails, UserAccessTokenDetails, AccessTokenRefreshResult>(key => key)((details, ct) => next(details, ct))))
                                .Finally(TokenRefreshing.CreateUserAccessTokenRefresher<UserAccessTokenDetails>(
                                        config.ResolveClientSecret,
                                        config.AuthorizationClient,
                                        config.ResolveClientId is null ? null : async _ => await config.ResolveClientId(request, ct),
                                        config.LoggerFactory?.CreateLogger("RefreshUserAccessToken")
                                        )
                                    )
                                )
                            )
                        // Get Token from Store
                        .Finally((key, ct) => throw new NotImplementedException())(new UserAccessTokenKey { Identity = userIdentity, ValidScopes = request.ValidScopes }, ct) switch
                        {
                            IHaveAccessTokenDetails<UserAccessTokenDetails> result => result.AccessTokenDetails.AccessToken,
                            _ => null
                        },
                    ExtensionIdentity extensionIdentity => throw new NotImplementedException(),
                    TwitchApiIdentity { ClientId: not null } appIdentity => throw new NotImplementedException(),
                    _ => null
                }
            )
        .Finally(config.ResolveBearerToken ?? BearerTokenResolution.UseToken(null));
}
