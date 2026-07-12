using System.Text;
using Xunit;
using TwitchySharp.Api;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Tests.E2E;

public static class AppAccessTokenExtensions
{
    private static AccessTokenDetails.App ToAppAccessTokenDetails(
        this TwitchResponse<ClientCredentialsResponse> response,
        ClientId clientId
        )
        => new()
        {
            AccessToken = response.Content.AccessToken,
            ExpiresAt = DateTimeOffset.UtcNow + response.Content.ExpiresIn,
            Identity = new(clientId)
        };

    public static async ValueTask<AccessTokenDetails.App?> GetNewAppAccessToken(
        this ITwitchClient client,
        ClientId? clientId,
        ClientSecret clientSecret,
        CancellationToken ct
        )
    {
        if (clientId is not ClientId)
        {
            TestContext.Current.AddWarning("Failed to get an app access token because the request context client id was null.");
            return null;
        }

        try
        {
            return (await client.SendAsync(new ClientCredentialsRequest()
            {
                ClientId = clientId.Value,
                ClientSecret = clientSecret
            }, ct)).ToAppAccessTokenDetails(clientId.Value);
        }
        catch (TwitchApiException ex)
        {
            TestContext.Current.AddWarning($"""
                Failed to acquire a new app access token.
                {ex.StatusCode} response from Twitch:
                {Encoding.UTF8.GetString(ex.Content)}
                """);
            return null;
        }
    }
}
