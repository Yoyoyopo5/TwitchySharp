using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Stub class
/// </summary>
[Obsolete("Use Twitch Delegating Handlers")]
public class DefaultRequestAuthorizer()
    : IAuthorizeTwitchRequest
{
    public ValueTask<TwitchAuthorizationRequestOptions?> GetAuthorization(ITwitchRequest request, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
