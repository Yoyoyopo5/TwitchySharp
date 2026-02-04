using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Default access token resolver that first checks the request's configured override token,
/// then falls back to identity-based resolution.
/// </summary>
/// <remarks>
/// Resolution order:
/// <list type="number">
/// <item>
/// Request's override access token (from <see cref="IRequireAuthorization.OverrideAccessToken"/>)
/// </item>
/// <item>
/// Identity-based resolution via <see cref="IdentityTokenResolver"/>
/// </item>
/// </list>
/// <para>
/// This should cover most common scenarios. If you have more complex needs, consider implementing
/// <see cref="IResolveAccessToken"/> directly and passing it to a <see cref="SequentialAccessTokenResolver"/>
/// to define your own resolution pipeline.
/// </para>
/// </remarks>
public sealed record DefaultTokenResolver : IResolveAccessToken
{
    private readonly SequentialAccessTokenResolver _resolver;

    /// <summary>
    /// Initializes a new instance of <see cref="DefaultTokenResolver"/>.
    /// </summary>
    /// <param name="userAccessTokenResolver">Resolver for <see cref="UserIdentity"/> requests.</param>
    /// <param name="appAccessTokenResolver">Resolver for <see cref="ClientIdentity"/> requests.</param>
    /// <param name="extensionJwtResolver">Resolver for <see cref="ExtensionIdentity"/> requests.</param>
    public DefaultTokenResolver(
        IResolveUserAccessToken? userAccessTokenResolver = null,
        IResolveAppAccessToken? appAccessTokenResolver = null,
        IResolveExtensionJsonWebToken? extensionJwtResolver = null)
    {
        _resolver = new SequentialAccessTokenResolver(
        [
            new ConfiguredAccessTokenResolver(),
            new IdentityTokenResolver(userAccessTokenResolver, appAccessTokenResolver, extensionJwtResolver)
        ]);
    }

    /// <inheritdoc/>
    public ValueTask<AccessToken?> GetToken(ITwitchRequest request, CancellationToken ct = default)
        => _resolver.GetToken(request, ct);
}
