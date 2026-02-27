using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Saves <see cref="AccessTokenDetailsResolutionResult.New{TDetails}"/> access tokens to a <see cref="ITokenStore{TToken, TKey, TDetails}"/>.
/// </summary>
/// <typeparam name="TKey">The access token resolver key type.</typeparam>
/// <typeparam name="TToken">The access token type.</typeparam>
/// <typeparam name="TDetails">The access token details type.</typeparam>
/// <param name="TokenStore">The token store to write to.</param>
public record NewTokenWriter<TKey, TToken, TDetails>(
    ITokenStore<TToken, TKey, TDetails> TokenStore,
    ILogger<NewTokenWriter<TKey, TToken, TDetails>>? Logger = null
    )
    : DelegatingResolver<TKey, AccessTokenDetailsResolutionResult>
    where TToken : AccessToken
    where TDetails : IAccessTokenDetails
{
    private readonly ILogger<NewTokenWriter<TKey, TToken, TDetails>> _logger = Logger ?? NullLogger<NewTokenWriter<TKey, TToken, TDetails>>.Instance;
    public override async ValueTask<AccessTokenDetailsResolutionResult> ResolveAsync(TKey key, CancellationToken ct = default)
    {
        AccessTokenDetailsResolutionResult innerResult = await base.ResolveAsync(key, ct);
        if (innerResult is not AccessTokenDetailsResolutionResult.New<TDetails> newToken)
            return innerResult;

        TKey generatedKey = newToken.AccessTokenDetails switch
        {
            UserAccessTokenDetails userToken // We need to do this because scopes can be different on a refreshed token.
                => (TKey)(object)new UserAccessTokenKey
                {
                    Identity = userToken.Identity,
                    ValidScopes = userToken.Scopes
                },
            _ => key
        };
        
        await TokenStore.SaveTokenDetails(generatedKey, newToken.AccessTokenDetails, ct);
        _logger.LogInformation("Saved new token for {Identity}", newToken.AccessTokenDetails.Identity);
        return innerResult;
    }
}


