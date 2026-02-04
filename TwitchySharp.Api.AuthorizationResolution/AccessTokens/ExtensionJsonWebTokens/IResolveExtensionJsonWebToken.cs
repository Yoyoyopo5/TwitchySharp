using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Resolves an <see cref="ExtensionJsonWebToken"/> for a given <see cref="ExtensionIdentity"/>.
/// </summary>
public interface IResolveExtensionJsonWebToken
{
    ValueTask<ExtensionJsonWebToken?> GetToken(ExtensionIdentity identity, CancellationToken ct = default);
}
