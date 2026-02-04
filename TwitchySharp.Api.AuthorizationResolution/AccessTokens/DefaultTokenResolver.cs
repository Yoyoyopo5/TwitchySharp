using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Default access token resolver that combines a default <see cref="ConfiguredAccessTokenResolver"/> with a supplied <see cref="IdentityTokenResolver"/>.
/// </summary>
/// <remarks>
/// Resolution order:
/// <list type="number">
/// <item>
/// <see cref="ConfiguredAccessTokenResolver"/>
/// </item>
/// <item>
/// <see cref="IdentityTokenResolver"/>
/// </item>
/// </list>
/// <para>
/// This should cover most common scenarios. If you have more complex needs, consider implementing
/// <see cref="IResolveAccessToken{TKey}"/> directly and passing it to a <see cref="SequentialAccessTokenResolver{TKey}"/>
/// to define your own resolution pipeline.
/// </para>
/// </remarks>
/// <param name="IdentityResolver">The identity resolver to use.</param>
public sealed record DefaultTokenResolver(
    IdentityTokenResolver IdentityResolver
    ) : IResolveAccessToken<IRequireAuthorization>
{
    private readonly SequentialAccessTokenResolver<IRequireAuthorization> _resolver = [
        new ConfiguredAccessTokenResolver(),
        IdentityResolver
        ];

    /// <inheritdoc/>
    public ValueTask<AccessTokenResolutionResult> GetToken(IRequireAuthorization requiresAuthorization, CancellationToken ct = default)
        => _resolver.GetToken(requiresAuthorization, ct);
}
