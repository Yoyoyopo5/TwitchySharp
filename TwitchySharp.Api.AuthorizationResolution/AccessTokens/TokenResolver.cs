namespace TwitchySharp.Api.AuthorizationResolution;

public record TokenResolver<TKey, TIdentity, TToken>()
    : DelegatingResolver<IRequireAuthorization, AccessToken?, TKey, AccessTokenDetailsResolutionResult>
    where TIdentity : TwitchApiIdentity
    where TToken : AccessToken
{
    private readonly Func<IRequireAuthorization, TKey> _mapKey 
        = key => (TKey)(object)new AccessTokenKey<TIdentity>
        {
            Identity = (key.Identity as TIdentity)!
        };

    public TokenResolver(Func<IRequireAuthorization, TKey> mapKey)
        : this()
        => _mapKey = mapKey;

    public override required IResolveAsync<TKey, AccessTokenDetailsResolutionResult> InnerResolver { get; init; }
    protected override ValueTask<TKey> MapKey(IRequireAuthorization key, CancellationToken ct = default)
        => ValueTask.FromResult(_mapKey(key));
    protected override ValueTask<AccessToken?> MapResult(AccessTokenDetailsResolutionResult result, CancellationToken ct = default)
        => ValueTask.FromResult(result switch
        {
            IHaveAccessTokenDetails<AccessTokenDetails<TIdentity, TToken>> userResult => userResult.AccessTokenDetails.AccessToken as AccessToken,
            _ => null
        });
    public override ValueTask<AccessToken?> ResolveAsync(IRequireAuthorization key, CancellationToken ct = default)
    {
        if (key.Identity is not TIdentity)
            return ValueTask.FromResult<AccessToken?>(null);

        return base.ResolveAsync(key, ct);
    }
}

//public record UserAccessTokenResolver()
//    : TokenResolver<UserAccessTokenKey, UserIdentity, UserAccessToken>(key => new UserAccessTokenKey
//    {
//        Identity = (key.Identity as UserIdentity)!,
//        ValidScopes = key.ValidScopes
//    });

//public record AppAccessTokenResolver : TokenResolver<AccessTokenKey<ClientIdentity>, ClientIdentity, AppAccessToken>;
//public record ExtensionJsonWebTokenResolver : TokenResolver<AccessTokenKey<ExtensionIdentity>, ExtensionIdentity, ExtensionJsonWebToken>;

public static class IdentityAccessTokenResolverChainExtensions
{
    public static ResolverChain<IRequireAuthorization, AccessToken?> WithIdentity<TIdentity, TKey>(
        this ResolverChain<TKey, AccessTokenDetailsResolutionResult> chain,
        Func<IRequireAuthorization, TKey> mapKey
        )
        where TIdentity : TwitchApiIdentity
        => chain.Then(prev => new TokenResolver<TKey, TIdentity, AccessToken>(mapKey) { InnerResolver = prev });
}