using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

/// <summary>
/// Options used to configure Twitch Hexlix API authorization header resolution.
/// </summary>
/// <remarks>
/// The <see cref="TwitchRequestAuthorizationContext"/> on the <see cref="IAuthorizedTwitchRequest"/> interface implemented by requests needing <c>Client-Id</c> and
/// <c>Authorization</c> headers is used to resolve the actual header values using functions defined on these options.
/// </remarks>
public record TwitchAuthorizationResolutionOptions
{
    /// <summary>
    /// The fallback client id resolver to use when no other configured resolvers match.
    /// </summary>
    /// <remarks>
    /// Defaults to return a <see langword="null"/> client id.
    /// You can set this if you want to use a single client id for all requests.
    /// </remarks>
    public ClientIdResolver FallbackClientIdResolver { get; init; }
        = (context, ct) => ValueTask.FromResult((ClientId?)null);

    /// <summary>
    /// The fallback bearer token resolver to use when no other configured resolver match.
    /// </summary>
    /// <remarks>
    /// Defaults to return a <see langword="null"/> token.
    /// You can set this if you want to use a single access token for all requests.
    /// </remarks>
    public BearerTokenResolver FallbackBearerTokenResolver { get; init; }
        = (context, ct) => ValueTask.FromResult((IAccessToken?)null);

    internal MiddlewarePipelineBuilder<ClientIdResolver> ClientIdResolverBuilder { get; init; }
        = new MiddlewarePipelineBuilder<ClientIdResolver>()
            .Use(ClientIdentityResolution.UseNoneIdentity)
            .Use(ClientIdentityResolution.UseConfiguredClientId);
    internal MiddlewarePipelineBuilder<BearerTokenResolver> BearerTokenResolverBuilder { get; init; }
        = new MiddlewarePipelineBuilder<BearerTokenResolver>()
            .Use(BearerTokenResolution.UseOverrideToken)
            .Use(BearerTokenResolution.UseNoneIdentity);

    internal ClientIdResolver ClientIdResolver => ClientIdResolverBuilder.Finally(FallbackClientIdResolver);
    internal BearerTokenResolver BearerTokenResolver => BearerTokenResolverBuilder.Finally(FallbackBearerTokenResolver);
}

public static partial class TwitchAuthorizationResolutionOptionsExtensions
{
    /// <summary>
    /// Configure bearer token resolution for a specific <see cref="TwitchIdentity"/>.
    /// </summary>
    /// <remarks>
    /// Handles pattern matching requests by identity, plus thread safety on refresh and acquire new.
    /// </remarks>
    /// <typeparam name="TIdentity">The <see cref="TwitchIdentity"/> to configure bearer token resolution for.</typeparam>
    /// <typeparam name="TDetails">The <see cref="AccessTokenDetails"/> that should be resolved for <typeparamref name="TIdentity"/>.</typeparam>
    /// <param name="options">The options to configure.</param>
    /// <param name="identityOptions">The identity-specific bearer token resolution options.</param>
    /// <returns>The configured <paramref name="options"/>.</returns>
    public static TwitchAuthorizationResolutionOptions ConfigureIdentity<TIdentity, TDetails>(this TwitchAuthorizationResolutionOptions options,
        TokenResolutionOptions<TDetails> identityOptions
        )
        where TIdentity : TwitchIdentity
        where TDetails : AccessTokenDetails
    {
        MiddlewarePipelineBuilder<AccessTokenDetailsResolver> identityPipelineBuilder = new();

        if (identityOptions.RefreshToken is not null)
        {
            // We must construct the lazy function here so the cache creation stays outside the pipeline.
            var lazilyRefreshAndUpdate = ThreadSafety.Lazily<TDetails, TIdentity, AccessTokenDetailsResolutionResult>(
                    request => (TIdentity)request.Identity)(async (details, ct) =>
                    {
                        AccessTokenDetailsResolutionResult result = (await identityOptions.RefreshToken(details, ct)).ToResolutionResult();
                        if (identityOptions.OnNewToken is not null && result is AccessTokenDetailsResolutionResult.New<TDetails> newTokenResult)
                            await identityOptions.OnNewToken(newTokenResult.AccessTokenDetails, ct);
                        return result;
                    });

            identityPipelineBuilder.Use(next => async (context, ct) =>
                await next(context, ct) switch
                {
                    AccessTokenDetailsResolutionResult.Expired<TDetails> expiredTokenResult =>
                        (await lazilyRefreshAndUpdate(expiredTokenResult.AccessTokenDetails, ct)),
                    AccessTokenDetailsResolutionResult other => other
                });
        }


        if (identityOptions.AcquireNewToken is not null)
        {
            // We must construct the lazy function here so the cache creation stays outside the pipeline.
            var lazilyAcquireNewAndUpdate = ThreadSafety.Lazily<TwitchRequestAuthorizationContext, TIdentity, TDetails?>(
                    request => (TIdentity)request.Identity)(async (context, ct) =>
                    {
                        TDetails? newToken = await identityOptions.AcquireNewToken(context, ct);
                        if (identityOptions.OnNewToken is not null && newToken is not null)
                            await identityOptions.OnNewToken(newToken, ct);
                        return newToken;
                    });

            identityPipelineBuilder.Use(next => async (context, ct) =>
                await next(context, ct) switch
                {
                    AccessTokenDetailsResolutionResult invalid when invalid is // Have to get a little freaky here to combine the result types   
                    AccessTokenDetailsResolutionResult.Unavailable or
                        AccessTokenDetailsResolutionResult.Revoked<TDetails> =>
                        await lazilyAcquireNewAndUpdate(context, ct) switch
                        {
                            { } newToken => new AccessTokenDetailsResolutionResult.New<TDetails>(newToken),
                            _ => invalid
                        },
                    AccessTokenDetailsResolutionResult other => other
                });
        }


        AccessTokenDetailsResolver identityPipeline = identityPipelineBuilder.Finally(async (request, ct) =>
                AccessTokenDetailsResolutionResult.FromDetails( // This checks expiry.
                    identityOptions.GetCachedToken is not null
                    ? await identityOptions.GetCachedToken(request, ct)
                    : default));

        // Add the identity pipeline to run conditionally if request identity is TIdentity
        options.BearerTokenResolverBuilder.Use(next =>
        {
            var extractor = identityPipeline.ExtractBearerToken<TDetails>();
            return (context, ct) =>
                context.Identity is TIdentity
                ? extractor(context, ct)
                : next(context, ct);
        });

        return options;
    }
}
