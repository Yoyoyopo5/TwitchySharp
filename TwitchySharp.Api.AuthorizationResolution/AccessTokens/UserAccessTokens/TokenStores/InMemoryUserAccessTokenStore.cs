using System.Collections.Concurrent;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// A thread-safe in-memory token cache.
/// </summary>
public class InMemoryTokenCache<TDetails>
    where TDetails : IAccessTokenDetails
{
    private readonly ConcurrentDictionary<AccessToken, TDetails> _tokenIndex = new();
    private readonly ConcurrentDictionary<TwitchApiIdentity, TDetails> _identityIndex = new();
    private readonly object _lock = new();

    /// <inheritdoc/>
    public ValueTask<UserAccessTokenDetails?> GetTokenDetails(IRequireAuthorization key, CancellationToken ct = default)
    {
        if (_identityIndex.TryGetValue(key.Identity, out TDetails? details) && details.Scopes.HasRequiredScope(key.ValidScopes))
            return ValueTask.FromResult<TDetails?>(details);
        return ValueTask.FromResult<TDetails?>(null);
    }

    /// <inheritdoc/>
    public ValueTask<UserAccessTokenDetails?> GetTokenDetails(UserAccessToken token, CancellationToken ct = default)
    {
        _tokenIndex.TryGetValue(token, out UserAccessTokenDetails? details);
        return ValueTask.FromResult(details);
    }

    /// <inheritdoc/>
    public ValueTask<UserAccessTokenDetails?> RemoveTokenDetails(UserAccessToken token, CancellationToken ct = default)
    {
        lock (_lock)
        {
            if (!_tokenIndex.TryRemove(token, out UserAccessTokenDetails? details))
                return ValueTask.FromResult(details);

            _identityIndex.TryRemove(details.Identity, out _);
            return ValueTask.FromResult<UserAccessTokenDetails?>(details);
        }
    }

    /// <inheritdoc/>
    public ValueTask<UserAccessTokenDetails> SaveTokenDetails(UserAccessTokenKey key, UserAccessTokenDetails details, CancellationToken ct = default)
    {
        lock (_lock)
        {
            // Remove old token for this user if one exists
            if (_identityIndex.TryGetValue(key.Identity, out UserAccessTokenDetails? existingDetails))
                _tokenIndex.TryRemove(existingDetails.AccessToken, out _);

            _identityIndex[key.Identity] = details;
            _tokenIndex[details.AccessToken] = details;

            return ValueTask.FromResult(details);
        }
    }
}
