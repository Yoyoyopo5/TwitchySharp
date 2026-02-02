using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

/// <summary>
/// Defines methods for requesting new <see cref="UserAccessToken"/>s.
/// </summary>
public interface IRequestUserAccessToken
{
    /// <summary>
    /// Request a new <see cref="UserAccessTokenDetails"/> for the given <see cref="UserAccessTokenKey"/>.
    /// </summary>
    /// <param name="key">The key containing information about the <see cref="UserAccessToken"/> to create.</param>
    /// <returns>A new <see cref="UserAccessTokenDetails"/> for the <paramref name="key"/>.</returns>
    ValueTask<UserAccessTokenDetails> RequestUserAccessToken(UserAccessTokenKey key, CancellationToken ct = default);
}
