using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Default access token resolver that combines a default <see cref="ConfiguredAccessTokenResolver"/> with a supplied <see cref="IdentityTypeTokenResolver"/>.
/// </summary>
/// <remarks>
/// Resolution order:
/// <list type="number">
/// <item>
/// <see cref="ConfiguredAccessTokenResolver"/>
/// </item>
/// <item>
/// <see cref="IdentityTypeTokenResolver"/>
/// </item>
/// </list>
/// <para>
/// This should cover most common scenarios. If you have more complex needs, consider implementing
/// <see cref="IResolveAccessToken{TKey}"/> directly and passing it to a <see cref="SequentialResolver{TKey}"/>
/// to define your own resolution pipeline.
/// </para>
/// </remarks>
/// <param name="IdentityResolver">The identity resolver to use.</param>
public sealed record DefaultTokenResolver(
    IRefreshAccessToken<UserAccessTokenDetails> userAccessTokenRefresher,
    ITokenStore<UserAccessToken, UserAccessTokenKey, UserAccessTokenDetails> userAccessTokenStore
    ) : IResolveAsync<IRequireAuthorization, AccessToken?>
{
    private readonly SequentialResolver<IRequireAuthorization, AccessToken?> _resolver = [
        new ConfiguredAccessTokenResolver(),
        AccessTokenResolverChain
            .RetrieveFromStore(userAccessTokenStore)
            .ThenRefreshExpired(userAccessTokenRefresher)
            .ThenSaveNewTokens(userAccessTokenStore, null)
            .ConcurrentlyOn(key => key.Identity, null) // We need to make sure that Refresh is gated so that if there are multiple requests waiting, they don't all refresh one by one, instead they should get the new refreshed token.
            .WithIdentity<UserIdentity, UserAccessTokenKey>(auth => new UserAccessTokenKey {
                Identity = (UserIdentity)auth.Identity,
                ValidScopes = auth.ValidScopes
            })
        ];

    /// <inheritdoc/>
    public ValueTask<AccessToken?> ResolveAsync(IRequireAuthorization requiresAuthorization, CancellationToken ct = default)
        => _resolver.ResolveAsync(requiresAuthorization, ct);
}