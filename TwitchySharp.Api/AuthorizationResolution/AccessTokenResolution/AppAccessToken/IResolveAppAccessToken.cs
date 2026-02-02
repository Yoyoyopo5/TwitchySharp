using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

/// <summary>
/// Resolves an <see cref="AppAccessToken"/> for a given <see cref="ClientIdentity"/>.
/// </summary>
public interface IResolveAppAccessToken
{
    ValueTask<AppAccessToken?> GetToken(ClientIdentity identity, CancellationToken ct = default);
}
