namespace TwitchySharp.Api.AuthorizationResolution;

public interface ITokenStore<in TToken, in TKey, TDetails>
    where TToken : AccessToken
    where TDetails : IAccessTokenDetails
{
    /// <summary>
    /// Get stored <typeparamref name="TDetails"/> associated with a specific <typeparamref name="TToken"/>.
    /// </summary>
    /// <param name="accessToken">The access token to retrieve details for.</param>
    /// <returns>A <see cref="ValueTask"/> containing the stored <typeparamref name="TDetails"/> associated with the <paramref name="accessToken"/>, if any.</returns>
    ValueTask<TDetails?> GetTokenDetails(TToken accessToken, CancellationToken ct = default);
    /// <summary>
    /// Retrieve a <typeparamref name="TDetails"/> for a given <typeparamref name="TKey"/>.
    /// </summary>
    /// <param name="key">The key to get <typeparamref name="TDetails"/> for.</param>
    /// <returns>A <see cref="ValueTask"/> containing the <typeparamref name="TDetails"/> associated with the <paramref name="key"/>, if any.</returns>
    ValueTask<TDetails?> GetTokenDetails(TKey key, CancellationToken ct = default);
    /// <summary>
    /// Remove the stored <typeparamref name="TDetails"/> for a given <typeparamref name="TToken"/>.
    /// </summary>
    /// <param name="accessToken">The access token to remove.</param>
    /// <returns>A <see cref="ValueTask"/> continaing the removed <typeparamref name="TDetails"/>, if any.</returns>
    ValueTask<TDetails?> RemoveTokenDetails(TToken accessToken, CancellationToken ct = default);
    /// <summary>
    /// Add or update the <typeparamref name="TDetails"/> for a given <typeparamref name="TKey"/>.
    /// </summary>
    /// <param name="key">The key to set the <typeparamref name="TDetails"/> for.</param>
    /// <param name="details">The details to set.</param>
    /// <returns>The <typeparamref name="TDetails"/> that were stored.</returns>
    ValueTask<TDetails> SaveTokenDetails(TKey key, TDetails details, CancellationToken ct = default);
}
