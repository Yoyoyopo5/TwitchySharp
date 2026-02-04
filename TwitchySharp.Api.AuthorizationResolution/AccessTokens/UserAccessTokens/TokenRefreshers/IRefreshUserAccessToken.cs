using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Defines methods for refreshing existing <see cref="UserAccessToken"/>s.
/// </summary>
public interface IRefreshUserAccessToken
{
    /// <summary>
    /// Refresh an existing <see cref="UserAccessToken"/> using its associated <see cref="RefreshToken"/> and <see cref="ClientIdentity"/>.
    /// </summary>
    /// <returns>An <see cref="AccessTokenRefreshResponse"/> with the access token details.</returns>
    ValueTask<AccessTokenRefreshResponse> RefreshUserAccessToken(ClientIdentity client, RefreshToken refreshToken, CancellationToken ct = default);
}