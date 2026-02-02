using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Resolves a <see cref="UserAccessToken"/> for a given <see cref="UserAccessTokenKey"/>.
/// </summary>
public interface IResolveUserAccessToken
{
    /// <summary>
    /// Get the user access token for the given key.
    /// </summary>
    /// <param name="key">Contains information used to retrieve a specific <see cref="UserAccessToken"/>.</param>
    /// <returns>A <see cref="ValueTask"/> containing the <see cref="UserAccessToken"/> associated with the <paramref name="key"/>, if any.</returns>
    ValueTask<UserAccessToken?> GetToken(UserAccessTokenKey key, CancellationToken ct = default);
}