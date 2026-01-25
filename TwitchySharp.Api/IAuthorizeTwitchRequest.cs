using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api;

/// <summary>
/// Determines mapping between <see cref="IRequireAuthorization"/> instances and <see cref="TwitchAuthorizationRequestOptions"/>.
/// </summary>
/// <remarks>
/// Use the <see cref="DefaultRequestAuthorizer"/>.
/// </remarks>
public interface IAuthorizeTwitchRequest
{
    ValueTask<TwitchAuthorizationRequestOptions?> GetAuthorization(IRequireAuthorization request, CancellationToken ct = default);
}

/// <param name="tokenResolver">The token resolver to use.</param>
public class DefaultRequestAuthorizer(ITokenResolver tokenResolver)
    : IAuthorizeTwitchRequest
{
    private readonly ITokenResolver _tokenResolver = tokenResolver;
    public async ValueTask<TwitchAuthorizationRequestOptions?> GetAuthorization(IRequireAuthorization request, CancellationToken ct = default)
    {
        AccessToken? token = request.OverrideAccessToken
            ?? await _tokenResolver.GetToken(request.Identity, request.ValidScopes, ct).ConfigureAwait(false);

        // Return options even if ClientId is null - some endpoints only need bearer token
        return new TwitchAuthorizationRequestOptions(request.Identity.ClientId, token);
    }
}
