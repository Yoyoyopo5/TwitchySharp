using System.Collections.Concurrent;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// A thread-safe in-memory implementation of <see cref="IUserAccessTokenStore"/>.
/// </summary>
/// <remarks>
/// Stores one token per user/client pair. Scope matching at lookup time uses "at least one of" semantics:
/// if the key specifies scopes, the stored token must have at least one of those scopes to match.
/// </remarks>
public class InMemoryUserAccessTokenStore : IUserAccessTokenStore
{
    private readonly ConcurrentDictionary<UserAccessToken, UserAccessTokenDetails> _tokenIndex = new();
    private readonly ConcurrentDictionary<UserIdentity, UserAccessTokenDetails> _userIndex = new();
    private readonly object _lock = new();

    /// <inheritdoc/>
    public ValueTask<UserAccessTokenDetails?> GetTokenDetails(UserAccessTokenKey key, CancellationToken ct = default)
    {
        if (_userIndex.TryGetValue(key.User, out UserAccessTokenDetails? details) && details.Scopes.HasRequiredScope(key.ValidScopes))
            return ValueTask.FromResult<UserAccessTokenDetails?>(details);
        return ValueTask.FromResult<UserAccessTokenDetails?>(null);
    }

    /// <inheritdoc/>
    public ValueTask<UserAccessTokenDetails?> GetTokenDetails(UserAccessToken token, CancellationToken ct = default)
    {
        _tokenIndex.TryGetValue(token, out UserAccessTokenDetails? details);
        return ValueTask.FromResult(details);
    }

    /// <inheritdoc/>
    public ValueTask<UserAccessTokenDetails?> RemoveToken(UserAccessToken token, CancellationToken ct = default)
    {
        lock (_lock)
        {
            if (!_tokenIndex.TryRemove(token, out UserAccessTokenDetails? details))
                return ValueTask.FromResult(details);

            _userIndex.TryRemove(details.User, out _);
            return ValueTask.FromResult<UserAccessTokenDetails?>(details);
        }
    }

    /// <inheritdoc/>
    public ValueTask<UserAccessTokenDetails> SaveTokenDetails(UserAccessTokenKey key, UserAccessTokenDetails details, CancellationToken ct = default)
    {
        lock (_lock)
        {
            // Remove old token for this user if one exists
            if (_userIndex.TryGetValue(key.User, out UserAccessTokenDetails? existingDetails))
                _tokenIndex.TryRemove(existingDetails.AccessToken, out _);

            _userIndex[key.User] = details;
            _tokenIndex[details.AccessToken] = details;

            return ValueTask.FromResult(details);
        }
    }
}
