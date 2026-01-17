using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Models.Authorization.Responses;

namespace TwitchySharp.Api.Models.Authorization.Requests;
/// <summary>
/// Used to get an app access token from Twitch.
/// </summary>
/// <remarks>
/// Uses the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#client-credentials-grant-flow">client credentials grant flow</see>.
/// </remarks>
public record ClientCredentialsRequest
    : TwitchAuthorizationRequest<ClientCredentialsResponse>
{
    /// <param name="clientId">The client id of the application to get an access token for.</param>
    /// <param name="clientSecret">The client secret of the application to get an access token for.</param>
    public ClientCredentialsRequest(string clientId, string clientSecret)
        : base("/token")
    {
        Method = HttpMethod.Post;
        ClientId = clientId;
        Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", clientId },
            { "client_secret", clientSecret },
            { "grant_type", "client_credentials" }
        });
    }
}
