using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Used to get an app access token from Twitch.
/// Uses the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#client-credentials-grant-flow">client credentials grant flow</see>.
/// </summary>
/// <param name="clientId">The client id of the application to get an access token for.</param>
/// <param name="clientSecret">The client secret of the application to get an access token for.</param>
public class ClientCredentialsRequest(string clientId, string clientSecret)
    : AuthorizationApiRequest<ClientCredentialsResponse>("/token")
{
    /// <summary>
    /// The client id of the application that will be used in the request.
    /// </summary>
    public new string ClientId { get; } = clientId;
    /// <summary>
    /// The client secret of the application that will be used in the request.
    /// </summary>
    public string ClientSecret { get; } = clientSecret;
    public override string? Data => $"client_id={ClientId}&client_secret={ClientSecret}&grant_type=client_credentials";
    public override HttpMethod Method => HttpMethod.Post;
    public override string ContentType => "application/x-www-form-urlencoded";
}
