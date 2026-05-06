using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Resolves an <see cref="IAccessToken"/> for a specific Twitch request requiring authorization.
/// </summary>
/// <param name="context">The authorization context.</param>
/// <returns>A <see cref="ValueTask"/> containing the resolved <see cref="IAccessToken"/>, if any.</returns>
public delegate ValueTask<IAccessToken?> BearerTokenResolver(TwitchRequestAuthorizationContext context, CancellationToken ct = default);

internal static class BearerTokenResolution
{
    private readonly static BearerTokenResolver GetOverrideToken
        = (context, _) => ValueTask.FromResult(context.AccessToken);

    /// <summary>
    /// Use the <see cref="TwitchRequestAuthorizationContext.AccessToken"/> if it exists.
    /// </summary>
    /// <param name="next">The next resolver to use, if the override token is <see langword="null"/>.</param>
    /// <returns>
    /// A <see cref="BearerTokenResolver"/> composed of the override token and <paramref name="next"/> as a fallback.
    /// </returns>
    public static BearerTokenResolver UseOverrideToken(BearerTokenResolver next)
        => async (context, ct) => (await GetOverrideToken(context, ct)) ?? await next(context, ct);

    public static Func<BearerTokenResolver, BearerTokenResolver> UseOverrideToken()
        => next => async (context, ct) => (await GetOverrideToken(context, ct)) ?? await next(context, ct);

    /// <summary>
    /// Short-circuit to <see langword="null"/> <see cref="IAccessToken"/> if the explicit
    /// <see cref="TwitchIdentity.None"/> is used in the <see cref="TwitchRequestAuthorizationContext"/>.
    /// </summary>
    public static BearerTokenResolver UseNoneIdentity(BearerTokenResolver next)
        => (context, ct) => context.Identity is TwitchIdentity.None
            ? ValueTask.FromResult<IAccessToken?>(null)
            : next(context, ct);
}
