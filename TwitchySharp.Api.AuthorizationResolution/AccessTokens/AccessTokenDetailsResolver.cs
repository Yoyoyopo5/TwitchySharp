namespace TwitchySharp.Api.AuthorizationResolution;

public delegate ValueTask<AccessTokenDetailsResolutionResult> AccessTokenDetailsResolver<TKey>(TKey key, CancellationToken ct = default);

public static partial class AccessTokenDetailsResolverExtensions
{
    /// <summary>
    /// Extract the output <see cref="AccessToken"/> of the <paramref name="detailsResolver"/>, if it exists.
    /// </summary>
    /// <typeparam name="TKey"/>The key type used to resolve the token.</typeparam>
    /// <typeparam name="TDetails">The token details type.</typeparam>
    /// <param name="detailsResolver">The details resolver to extract the <see cref="AccessToken"/> from.</param>
    /// <returns>A <see cref="BearerTokenResolver{TKey}"/> that returns the <see cref="AccessToken"/> from the <paramref name="detailsResolver"/>.</returns>
    public static BearerTokenResolver<TKey> ExtractBearerToken<TKey, TDetails>(this AccessTokenDetailsResolver<TKey> detailsResolver)
        where TDetails : IAccessTokenDetails
        => async (key, ct) => 
        await detailsResolver(key, ct) switch 
        {
            IHaveAccessTokenDetails<TDetails> hasToken => hasToken.AccessTokenDetails.AccessToken,
            _ => default
        }; 
}
