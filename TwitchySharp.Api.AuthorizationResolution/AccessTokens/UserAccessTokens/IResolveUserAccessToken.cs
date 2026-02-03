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
    /// <returns>A <see cref="UserAccessTokenResolutionResult"/> indicating the status of the token query.</returns>
    ValueTask<UserAccessTokenResolutionResult> GetToken(UserAccessTokenKey key, CancellationToken ct = default);
}