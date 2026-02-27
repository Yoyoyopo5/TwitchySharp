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

internal record TwitchClientBuilder // Need to move this to Api project.
    : MiddlewarePipelineBuilder<TwitchRequestHandler>;

internal static class TwitchClientBuilderExtensions
{
    public static TwitchClientBuilder UseAuthorizationResolution(this TwitchClientBuilder builder, 
        ClientIdResolver? fallbackClientResolver = null, 
        BearerTokenResolver<IRequireAuthorization>? fallbackTokenResolver = null
        )
        => (TwitchClientBuilder)builder.Use(next => async (context, ct) => await next(context.Request switch
        {
            IRequireAuthorization requiresAuthorization => context with
            {
                ClientId = await DefaultClientIdResolver(fallbackClientResolver)(requiresAuthorization, ct),
                BearerToken = await DefaultBearerTokenResolver(fallbackTokenResolver)(requiresAuthorization, ct)
            },
            _ => context
        }, ct));


    private readonly static Func<ClientIdResolver?, ClientIdResolver> DefaultClientIdResolver =
        next => new MiddlewarePipelineBuilder<ClientIdResolver>()
        .Use(ClientIdentityResolution.UseConfiguredClientId)
        .Finally(next ?? ClientIdentityResolution.UseFallbackClientId(null));

    private static readonly ConcurrentDictionary<TwitchApiIdentity, SemaphoreSlim> _semaphores = [];

    private readonly static Func<BearerTokenResolver<IRequireAuthorization>?, BearerTokenResolver<IRequireAuthorization>> DefaultBearerTokenResolver =
        next => new MiddlewarePipelineBuilder<BearerTokenResolver<IRequireAuthorization>>() // Oh my god
        .Use(BearerTokenResolution.UseOverrideToken)
        .Use(BearerTokenResolution.UseIdentityResolution<UserIdentity>(
            async (request, ct) => 
                (await Concurrent.UseConcurrent<UserAccessTokenKey, AccessTokenDetailsResolutionResult>(key => _semaphores.GetOrAdd(key.Identity, (_) => new SemaphoreSlim(1, 1)))(
                    (key, ct) => TokenDetailsResolution.UseRefresh<UserAccessTokenKey, UserAccessTokenDetails>(TokenRefreshing.CreateUserAccessTokenRefresher<UserAccessTokenKey>(
                        (clientId, ct) => default,
                        null
                        ))((key, ct) => throw new NotImplementedException())(key, ct)
                    )(new UserAccessTokenKey 
                    {
                        Identity = (UserIdentity)request.Identity,
                        ValidScopes = request.ValidScopes
                    }, ct)) switch
                {
                    IHaveAccessTokenDetails<UserAccessTokenDetails> result => result.AccessTokenDetails.AccessToken,
                    _ => null
                }
            ))
        .Use(BearerTokenResolution.UseIdentityResolution<ExtensionIdentity>(
            (request, ct) => throw new NotImplementedException()
            ))
        .Use(BearerTokenResolution.UseIdentityResolution<TwitchApiIdentity>(
            (request, ct) => throw new NotImplementedException()
            ))
        .Finally(next ?? BearerTokenResolution.UseToken(null));
}
