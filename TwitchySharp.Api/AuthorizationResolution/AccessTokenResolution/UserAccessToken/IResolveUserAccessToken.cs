using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

/// <summary>
/// Resolves a <see cref="UserAccessToken"/> for a given <see cref="UserIdentity"/>.
/// </summary>
public interface IResolveUserAccessToken
{
    ValueTask<UserAccessToken?> GetToken(UserAccessTokenKey key, CancellationToken ct = default);
}
