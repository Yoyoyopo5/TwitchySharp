using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using TwitchySharp.Api;
using TwitchySharp.Api.Authentication;

namespace TwitchySharp.Tests.E2E;

public sealed class TokenStore(IEnumerable<IAccessTokenDetails<TwitchIdentity>>? tokens = null)
    : ITwitchTokenCache<ClientId, AccessTokenDetails.App>,
    ITwitchTokenCache<TwitchIdentity.User, AccessTokenDetails.User>,
    ITwitchTokenCache<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt>
{
    private readonly ConcurrentDictionary<TwitchIdentity, IAccessTokenDetails<TwitchIdentity>> _store = tokens is null
        ? []
        : new(tokens.Select(t => new KeyValuePair<TwitchIdentity, IAccessTokenDetails<TwitchIdentity>>(t.Identity, t)));

    public void AddOrUpdate(IAccessTokenDetails<TwitchIdentity> details)
        => _store.AddOrUpdate(details.Identity, details, (_, _) => details);

    public bool TryGet(TwitchIdentity identity, [NotNullWhen(true)] out IAccessTokenDetails<TwitchIdentity>? details)
        => _store.TryGetValue(identity, out details);

    public bool TryGet<TDetails>(TwitchIdentity identity, [NotNullWhen(true)] out TDetails? details)
        where TDetails : IAccessTokenDetails<TwitchIdentity>
    {
        details = default;
        if (!_store.TryGetValue(identity, out IAccessTokenDetails<TwitchIdentity>? baseDetails)
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

    private TDetails? GetOrDefault<TDetails>(TwitchIdentity identity)
        where TDetails : IAccessTokenDetails<TwitchIdentity>
        => _store.GetValueOrDefault(identity) is TDetails result
            ? result
            : default;

    public ValueTask<AccessTokenDetails.App?> GetOrDefault(ClientId key, CancellationToken ct)
        => ValueTask.FromResult(GetOrDefault<AccessTokenDetails.App>(new TwitchIdentity.Client(key)));
    public ValueTask<ITwitchTokenCache<ClientId, AccessTokenDetails.App>> Set(ClientId key, AccessTokenDetails.App value)
    {
        AddOrUpdate(value);
        return ValueTask.FromResult<ITwitchTokenCache<ClientId, AccessTokenDetails.App>>(this);
    }

    public ValueTask<AccessTokenDetails.User?> GetOrDefault(TwitchIdentity.User key, CancellationToken ct)
        => ValueTask.FromResult(GetOrDefault<AccessTokenDetails.User>(key));
    public ValueTask<ITwitchTokenCache<TwitchIdentity.User, AccessTokenDetails.User>> Set(TwitchIdentity.User key, AccessTokenDetails.User value)
    {
        AddOrUpdate(value);
        return ValueTask.FromResult<ITwitchTokenCache<TwitchIdentity.User, AccessTokenDetails.User>>(this);
    }

    public ValueTask<AccessTokenDetails.ExtensionJwt?> GetOrDefault(TwitchIdentity.Extension key, CancellationToken ct)
        => ValueTask.FromResult(GetOrDefault<AccessTokenDetails.ExtensionJwt>(key));
    public ValueTask<ITwitchTokenCache<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt>> Set(TwitchIdentity.Extension key, AccessTokenDetails.ExtensionJwt value)
    {
        AddOrUpdate(value);
        return ValueTask.FromResult<ITwitchTokenCache<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt>>(this);
    }
}
