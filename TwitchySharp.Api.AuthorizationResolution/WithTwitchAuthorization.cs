using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

public record TwitchAuthorizationResolutionOptions
{
    public required ITwitchClient AuthorizationClient { get; init; }
    public required ClientSecretResolver SecretResolver { get; init; }
    public ClientIdResolver? FallbackClientIdResolver { get; init; }
    public BearerTokenResolver<IRequireAuthorization>? FallbackTokenResolver { get; init; }
    public ILoggerFactory? LoggerFactory { get; init; }
}

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


    private readonly static Func<TwitchAuthorizationResolutionOptions, ClientIdResolver> DefaultClientIdResolver =
        config => new MiddlewarePipelineBuilder<ClientIdResolver>()
        .Use(ClientIdentityResolution.UseConfiguredClientId)
        .Finally(config.FallbackClientIdResolver ?? ClientIdentityResolution.UseFallbackClientId(null));

    private readonly static Func<TwitchAuthorizationResolutionOptions, BearerTokenResolver<IRequireAuthorization>> DefaultBearerTokenResolver =
        config => new MiddlewarePipelineBuilder<BearerTokenResolver<IRequireAuthorization>>()
        .Use(BearerTokenResolution.UseOverrideToken)
        .Use(async (request, ct)
                => request.Identity switch
                {
                    UserIdentity userIdentity => await new MiddlewarePipelineBuilder<AccessTokenDetailsResolver<UserAccessTokenKey>>()
                        .Use(TokenDetailsResolution.UseRefresh<UserAccessTokenKey, UserAccessTokenDetails>(
                            new MiddlewarePipelineBuilder<AccessTokenRefresher<UserAccessTokenDetails>>()
                                .Use(next => new AccessTokenRefresher<UserAccessTokenDetails>(ThreadSafety.Lazily<UserAccessTokenDetails, UserAccessTokenDetails, AccessTokenRefreshResult>(key => key)((details, ct) => next(details, ct))))
                                .Finally(TokenRefreshing.CreateUserAccessTokenRefresher<UserAccessTokenDetails>(
                                        config.SecretResolver,
                                        config.AuthorizationClient,
                                        config.FallbackClientIdResolver is null ? null : async _ => await config.FallbackClientIdResolver(request, ct),
                                        config.LoggerFactory?.CreateLogger("RefreshUserAccessToken")
                                        )
                                    )
                                )
                            )
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
        .Finally(config.FallbackTokenResolver ?? BearerTokenResolution.UseToken(null));
}
