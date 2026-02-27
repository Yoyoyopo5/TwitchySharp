using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Resolves an <see cref="AccessToken"/> for a specific Twitch request requiring authorization.
/// </summary>
/// <param name="request">The authorization requirement.</param>
/// <returns>A <see cref="ValueTask"/> containing the resolved <see cref="AccessToken"/>, if any.</returns>
public delegate ValueTask<AccessToken?> BearerTokenResolver<T>(T key, CancellationToken ct = default);

internal static class BearerTokenResolution
{
    private readonly static BearerTokenResolver<IRequireAuthorization> GetOverrideToken
        = (request, _) => ValueTask.FromResult(request.OverrideAccessToken);

    /// <summary>
    /// Use the <see cref="IRequireAuthorization.OverrideAccessToken"/> if it exists.
    /// </summary>
    /// <param name="next">The next resolver to use, if the override token is <see langword="null"/>.</param>
    /// <returns>
    /// A <see cref="BearerTokenResolver"/> composed of the override token and <paramref name="next"/> as a fallback.
    /// </returns>
    public static BearerTokenResolver<IRequireAuthorization> UseOverrideToken(BearerTokenResolver<IRequireAuthorization> next)
        => async (request, ct) => (await GetOverrideToken(request, ct)) ?? await next(request, ct);

    public static Func<BearerTokenResolver<IRequireAuthorization>, BearerTokenResolver<IRequireAuthorization>> UseOverrideToken()
        => next => async (request, ct) => (await GetOverrideToken(request, ct)) ?? await next(request, ct);

    /// <summary>
    /// Configure a specialized <see cref="BearerTokenResolver"/> for <typeparamref name="TIdentity"/>.
    /// </summary>
    /// <typeparam name="TIdentity">The <see cref="TwitchApiIdentity"/> type to switch on.</typeparam>
    /// <param name="resolveWithIdentity">The specialized <see cref="BearerTokenResolver"/> for the <typeparamref name="TIdentity"/>.</param>
    /// <returns>
    /// A function returning a <see cref="BearerTokenResolver"/> using identity-based resolution, with a configurable next fallback.
    /// </returns>
    public static Func<BearerTokenResolver<IRequireAuthorization>, BearerTokenResolver<IRequireAuthorization>> UseIdentityResolution<TIdentity>(
        BearerTokenResolver<IRequireAuthorization> resolveWithIdentity
        )
        where TIdentity : TwitchApiIdentity
        => next => (request, ct) => request.Identity switch
        {
            TIdentity identity => resolveWithIdentity(request, ct),
            _ => next(request, ct)
        };

    /// <summary>
    /// Use a configured bearer access token for all requests.
    /// </summary>
    /// <param name="token">The token to use.</param>
    /// <returns>A <see cref="BearerTokenResolver"/> configured to use the <paramref name="token"/>.</returns>
    public static BearerTokenResolver<IRequireAuthorization> UseToken(AccessToken? token)
        => (_, _) => ValueTask.FromResult(token);
}
