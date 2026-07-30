using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using TwitchySharp.Api;

namespace TwitchySharp.Tests.E2E;

public sealed class TokenStore(IEnumerable<AccessTokenDetails>? tokens = null)
{
    private readonly ConcurrentDictionary<TwitchIdentity, AccessTokenDetails> _store = tokens is null
        ? []
        : new(tokens.Select(t => new KeyValuePair<TwitchIdentity, AccessTokenDetails>(t.Identity, t)));

    public void AddOrUpdate(AccessTokenDetails details)
        => _store.AddOrUpdate(details.Identity, details, (_, _) => details);

    public bool TryGet(TwitchIdentity identity, [NotNullWhen(true)] out AccessTokenDetails? details)
        => _store.TryGetValue(identity, out details);

    public bool TryGet<TDetails>(TwitchIdentity identity, [NotNullWhen(true)] out TDetails? details)
        where TDetails : AccessTokenDetails
    {
        details = null;
        if (!_store.TryGetValue(identity, out AccessTokenDetails? baseDetails)
                || baseDetails is not TDetails derived)
            return false;
        details = derived;
        return true;
    }

    public bool TryGet(
        TwitchIdentity.User userIdentity,
        IReadOnlySet<Scope> requiresOneOf,
        [NotNullWhen(true)] out AccessTokenDetails.User? details
        )
    {
        details = null;
        if (!TryGet(userIdentity, out AccessTokenDetails.User? userDetails)
            || !requiresOneOf.Any(userDetails.Scopes.Contains))
            return false;
        details = userDetails;
        return true;
    }
}
