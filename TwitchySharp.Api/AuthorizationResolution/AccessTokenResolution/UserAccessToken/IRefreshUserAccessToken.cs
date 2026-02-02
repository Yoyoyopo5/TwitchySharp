using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

/// <summary>
/// Defines methods for refreshing existing <see cref="UserAccessToken"/>s.
/// </summary>
public interface IRefreshUserAccessToken
{
    /// <summary>
    /// Refresh an existing <see cref="UserAccessTokenDetails"/> using its <see cref="RefreshToken"/>.
    /// </summary>
    /// <param name="details">The details to refresh.</param>
    /// <returns>A new <see cref="UserAccessTokenDetails"/> with a refreshed <see cref="UserAccessToken"/>.</returns>
    ValueTask<UserAccessTokenDetails> RefreshUserAccessToken(UserAccessTokenDetails details, CancellationToken ct = default);
}