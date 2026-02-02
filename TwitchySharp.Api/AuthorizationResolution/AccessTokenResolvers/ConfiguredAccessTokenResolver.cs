using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

/// <summary>
/// Returns the <see cref="IRequireAuthorization.OverrideAccessToken"/> if the <see cref="ITwitchRequest"/>
/// implements <see cref="IRequireAuthorization"/> and it is not <see langword="null"/>.
/// </summary>
public record ConfiguredAccessTokenResolver() : ITokenResolver
{
    public ValueTask<AccessToken?> GetToken(ITwitchRequest request, CancellationToken ct = default)
        => ValueTask.FromResult((request as IRequireAuthorization)?.OverrideAccessToken);
}
