using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

/// <summary>
/// Resolves an <see cref="ExtensionJsonWebToken"/> for a given <see cref="ExtensionIdentity"/>.
/// </summary>
public interface IResolveExtensionJwt
{
    ValueTask<ExtensionJsonWebToken?> GetToken(ExtensionIdentity identity, CancellationToken ct = default);
}
