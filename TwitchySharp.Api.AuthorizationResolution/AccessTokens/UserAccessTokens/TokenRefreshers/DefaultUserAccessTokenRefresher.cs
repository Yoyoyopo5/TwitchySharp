using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// An <see cref="IRefreshUserAccessToken"/> implementation that uses an <see cref="ITwitchClient"/>
/// to refresh user access tokens by sending <see cref="AccessTokenRefreshRequest"/>s.
/// </summary>
/// <param name="twitchClient">The Twitch client to use for making refresh requests.</param>
/// <param name="secretResolver">The resolver to use for obtaining the client secret.</param>
public class DefaultUserAccessTokenRefresher(
    ITwitchClient twitchClient,
    IResolveClientSecret secretResolver
    ) : IRefreshUserAccessToken
{

    private readonly ITwitchClient _twitchClient = twitchClient;
    private readonly IResolveClientSecret _secretResolver = secretResolver;

    /// <inheritdoc/>
    public async ValueTask<AccessTokenRefreshResponse> RefreshUserAccessToken(
        ClientIdentity client,
        RefreshToken refreshToken,
        CancellationToken ct = default)
    {
        if (await _secretResolver.GetClientSecret(client.ClientId, ct) is not ClientSecret secret)
            throw new InvalidOperationException(
                $"Unable to resolve client secret for client {client.ClientId}.");

        AccessTokenRefreshRequest request = new()
        {
            ClientId = client.ClientId,
            ClientSecret = secret,
            RefreshToken = refreshToken
        };

        ITwitchResponse<AccessTokenRefreshResponse> response =
            await _twitchClient.SendAsync(request, ct);
        return response.Content;
    }
}
