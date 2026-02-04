using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

public interface ITokenResolver
{
    /// <summary>
    /// Resolves an <see cref="AccessToken"/> based on the provided <see cref="ITwitchRequest"/>.
    /// </summary>
    /// <param name="request">The request to get an access token for.</param>
    /// <returns>An <see cref="AccessToken"/> resolved from the <see cref="ITwitchRequest"/>, if any.</returns>
    ValueTask<AccessToken?> GetToken(ITwitchRequest request, CancellationToken ct = default);
}