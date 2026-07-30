using System.Text;
using Xunit;
using TwitchySharp.Api;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Tests.E2E;

public static class UserAccessTokenExtensions
{
    private static AccessTokenRefreshResult.Refreshed<AccessTokenDetails.User> ToRefreshResult(
        this TwitchResponse<AccessTokenRefreshResponse> refreshResponse,
        UserId userId,
        ClientId clientId
        )
        => new(new AccessTokenDetails.User()
        {
            Identity = new TwitchIdentity.User(userId, clientId),
            AccessToken = new UserAccessToken(refreshResponse.Content.AccessToken),
            RefreshToken = new RefreshToken(refreshResponse.Content.RefreshToken),
            Scopes = refreshResponse.Content.Scope?.Select(s => new Scope(s)).ToHashSet() ?? [],
            ExpiresAt = DateTimeOffset.UtcNow + refreshResponse.Content.ExpiresIn
        });

    public static async ValueTask<AccessTokenRefreshResult> RefreshUserAccessToken(
        this ITwitchClient client,
        AccessTokenDetails.User tokenDetails,
        ClientSecret clientSecret,
        CancellationToken ct
        )
    {
        if (tokenDetails is not { Identity.ClientId: not null, RefreshToken: not null })
        {
            TestContext.Current.AddWarning("Failed to refresh a user access token because the token details are missing required information (client id or refresh token).");
            return new AccessTokenRefreshResult.Expired<AccessTokenDetails.User>(tokenDetails);
        }

        try
        {
            return (await client.SendAsync(new AccessTokenRefreshRequest()
            {
                ClientId = tokenDetails.Identity.ClientId.Value,
                ClientSecret = clientSecret,
                RefreshToken = tokenDetails.RefreshToken.Value
            }, ct)).ToRefreshResult(tokenDetails.Identity.UserId, tokenDetails.Identity.ClientId.Value);
        }
        catch (TwitchApiException ex)
        {
            TestContext.Current.AddWarning($"""
                Failed to refresh a user access token.
                {ex.StatusCode} response from Twitch:
                {Encoding.UTF8.GetString(ex.Content)}
                """);
            return new AccessTokenRefreshResult.Expired<AccessTokenDetails.User>(tokenDetails);
        }
    }
}
