using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

/// <summary>
/// Defines methods for storing, retrieving, and removing <see cref="UserAccessTokenDetails"/>s.
/// </summary>
public interface IUserAccessTokenStore
{
    /// <summary>
    /// Retrieve a <see cref="UserAccessTokenDetails"/> for a given <see cref="UserAccessTokenKey"/>.
    /// </summary>
    /// <param name="key">The key to get <see cref="UserAccessTokenDetails"/> for.</param>
    /// <returns>A <see cref="ValueTask"/> containing the <see cref="UserAccessTokenDetails"/> associated with the <paramref name="key"/>, if any.</returns>
    ValueTask<UserAccessTokenDetails?> GetToken(UserAccessTokenKey key, CancellationToken ct = default);
    /// <summary>
    /// Remove the stored <see cref="UserAccessTokenDetails"/> for a given <see cref="UserAccessToken"/>.
    /// </summary>
    /// <param name="token">The user access token to remove.</param>
    /// <returns>A <see cref="ValueTask"/> continaing the removed <see cref="UserAccessTokenDetails"/>, if any.</returns>
    ValueTask<UserAccessTokenDetails?> TryRemoveToken(UserAccessToken token, CancellationToken ct = default);
    /// <summary>
    /// Add or update the <see cref="UserAccessTokenDetails"/> for a given <see cref="UserAccessTokenKey"/>.
    /// </summary>
    /// <param name="key">The key to set the <see cref="UserAccessTokenDetails"/> for.</param>
    /// <param name="details">The details to set.</param>
    /// <returns>The <see cref="UserAccessTokenDetails"/> that were stored.</returns>
    ValueTask<UserAccessTokenDetails> SaveToken(UserAccessTokenKey key, UserAccessTokenDetails details, CancellationToken ct = default);
}
