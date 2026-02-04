using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Returns the <see cref="IRequireAuthorization.OverrideAccessToken"/> if the <see cref="ITwitchRequest"/>
/// implements <see cref="IRequireAuthorization"/> and it is not <see langword="null"/>.
/// </summary>
public record ConfiguredAccessTokenResolver() : IResolveAccessToken<IRequireAuthorization>
{
    public ValueTask<AccessTokenResolutionResult> GetToken(IRequireAuthorization request, CancellationToken ct = default)
        => ValueTask.FromResult<AccessTokenResolutionResult>(request.OverrideAccessToken switch
        {
            AccessToken token => new AccessTokenResolutionResult.Available<AccessToken>(token),
            _ => new AccessTokenResolutionResult.Unavailable()
        });
}
