namespace TwitchySharp.Api;

public delegate ValueTask<TDetails?> AccessTokenDetailsResolver<TDetails>(TwitchRequestAuthorizationContext context, CancellationToken ct = default)
    where TDetails : AccessTokenDetails;

internal delegate ValueTask<AccessTokenDetailsResolutionResult> AccessTokenDetailsResolver(TwitchRequestAuthorizationContext context, CancellationToken ct = default);

internal static partial class AccessTokenDetailsResolverExtensions
{
    /// <summary>
    /// Extract the output <see cref="IAccessToken"/> of the <paramref name="detailsResolver"/>, if it exists.
    /// </summary>
    /// <typeparam name="TKey">The key type used to resolve the token.</typeparam>
    /// <typeparam name="TDetails">The token details type.</typeparam>
    /// <param name="detailsResolver">The details resolver to extract the <see cref="IAccessToken"/> from.</param>
    /// <returns>A <see cref="BearerTokenResolver"/> that returns the <see cref="IAccessToken"/> from the <paramref name="detailsResolver"/>.</returns>
    internal static BearerTokenResolver ExtractBearerToken<TDetails>(this AccessTokenDetailsResolver detailsResolver)
        where TDetails : AccessTokenDetails
        => async (key, ct) =>
        await detailsResolver(key, ct) switch
        {
            AccessTokenDetailsResolutionResult.Available<TDetails> hasToken => hasToken.AccessTokenDetails.AccessToken,
            _ => default
        };
}
