using TwitchySharp.Shared.Models;
using Microsoft.Extensions.Logging;

namespace TwitchySharp.Api.AuthorizationResolution;

public record TwitchAuthorizationResolutionOptions
{
    public required ITwitchClient AuthorizationClient { get; init; }
    public ClientIdResolver? ResolveClientId { get; init; }
    public BearerTokenResolver<IRequireAuthorization>? ResolveBearerToken { get; init; }

    internal MiddlewarePipelineBuilder<ClientIdResolver> ClientIdResolverBuilder { get; init; } = new();
    internal MiddlewarePipelineBuilder<BearerTokenResolver<IRequireAuthorization>> BearerTokenResolverBuilder { get; init; } = new();

    public ILoggerFactory? LoggerFactory { get; init; }
}

public static partial class TwitchAuthorizationResolutionOptionsExtensions
{
    public static TwitchAuthorizationResolutionOptions ConfigureIdentity<TIdentity, TDetails>(this TwitchAuthorizationResolutionOptions options, 
        TokenResolutionOptions<TDetails> identityOptions
        )
        where TIdentity : TwitchApiIdentity
        where TDetails : IAccessTokenDetails
    {
        MiddlewarePipelineBuilder<AccessTokenDetailsResolver<IRequireAuthorization>> identityPipelineBuilder = new();

        if (identityOptions.OnNewToken is not null)
            identityPipelineBuilder.Use(next => async (requirement, ct) => {
                AccessTokenDetailsResolutionResult result = await next(requirement, ct);
                if (result is AccessTokenDetailsResolutionResult.New<TDetails> newTokenResult)
                    await identityOptions.OnNewToken(newTokenResult.AccessTokenDetails, ct);
                return result;
                });

        if (identityOptions.RefreshToken is not null)
            identityPipelineBuilder.Use(next => (requirement, ct) =>
                ThreadSafety.Lazily<IRequireAuthorization, TIdentity, AccessTokenDetailsResolutionResult>(
                    requirement => (TIdentity)requirement.Identity)(async (requirement, ct) =>
                    await next(requirement, ct) switch 
                    {
                        AccessTokenDetailsResolutionResult.Expired<TDetails> expiredTokenResult => 
                            (await identityOptions.RefreshToken(expiredTokenResult.AccessTokenDetails, ct)).ToResolutionResult(),
                        AccessTokenDetailsResolutionResult other => other
                    })(requirement, ct));

        if (identityOptions.AcquireNewToken is not null)
            identityPipelineBuilder.Use(next => (requirement, ct) => 
                ThreadSafety.Lazily<IRequireAuthorization, TIdentity, AccessTokenDetailsResolutionResult>(
                    requirement => (TIdentity)requirement.Identity)(async (requirement, ct) =>
                    await next(requirement, ct) switch
                    {
                        AccessTokenDetailsResolutionResult.Unavailable
                        or AccessTokenDetailsResolutionResult.Revoked<TDetails> =>
                            AccessTokenDetailsResolutionResult.FromDetails(await identityOptions.AcquireNewToken(requirement, ct)),
                        AccessTokenDetailsResolutionResult other => other
                    })(requirement, ct));
        
        AccessTokenDetailsResolver<IRequireAuthorization> identityPipeline = identityPipelineBuilder.Finally(async (requirement, ct) => 
                AccessTokenDetailsResolutionResult.FromDetails<TDetails>(
                    identityOptions.GetCachedToken is not null 
                    ? await identityOptions.GetCachedToken(requirement, ct) 
                    : default));
           
        // Add the identity pipeline conditionally if requirement identity is TIdentity
        options.BearerTokenResolverBuilder.Use(next => (requirement, ct) => 
                requirement.Identity is TIdentity 
                ? identityPipeline.ExtractBearerToken<IRequireAuthorization, TDetails>()(requirement, ct)
                : next(requirement, ct));
       
        return options;
    }

    // May not even need this if the per-identity options types are convertible to TokenResolutionOptions.
    public static TwitchAuthorizationResolutionOptions ConfigureUserIdentity(this TwitchAuthorizationResolutionOptions options,
        UserAccessTokenResolutionOptions userOptions)
        => options.ConfigureIdentity<UserIdentity, UserAccessTokenDetails>(userOptions);
}

public record UserAccessTokenResolutionOptions
{
    public Func<IRequireAuthorization, CancellationToken, ValueTask<UserAccessTokenDetails?>>? GetCachedToken { get; init; }
    public Func<UserAccessTokenDetails, CancellationToken, ValueTask>? OnNewToken { get; init; }
    public Func<IRequireAuthorization, CancellationToken, ValueTask<UserAccessTokenDetails?>>? AcquireNewToken { get; init; } 

    public required ClientSecretResolver ClientSecretResolver { get; init; }
    public required ITwitchClient AuthorizationClient { get; init; }
    public Func<UserAccessTokenDetails, CancellationToken, ValueTask<ClientId?>>? ResolveFallbackClientId { get; init; }
    public ILoggerFactory? LoggerFactory { get; init; }

    public static implicit operator TokenResolutionOptions<UserAccessTokenDetails>(UserAccessTokenResolutionOptions options)
        => new()
        {
            GetCachedToken = options.GetCachedToken,
            RefreshToken = TokenRefreshing.CreateUserAccessTokenRefresher(
                    options.ClientSecretResolver,
                    options.AuthorizationClient,
                    options.ResolveFallbackClientId,
                    options.LoggerFactory
                    ),
            AcquireNewToken = options.AcquireNewToken,
            OnNewToken = options.OnNewToken
        };
}

public record AppAccessTokenResolutionOptions
{
    public static implicit operator TokenResolutionOptions<AccessTokenDetails<ClientIdentity, AppAccessToken>>(AppAccessTokenResolutionOptions options)
        => new()
        {

        };
}

public record ExtensionAccessTokenResolutionOptions
{
    public static implicit operator TokenResolutionOptions<AccessTokenDetails<ExtensionIdentity, ExtensionJsonWebToken>>(ExtensionAccessTokenResolutionOptions options)
        => new()
        {

        };
}
