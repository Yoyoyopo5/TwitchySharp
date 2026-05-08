using System.Collections.Generic;
using System.Net.Http;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Used to get an app access token from Twitch.
/// </summary>
/// <remarks>
/// Uses the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#client-credentials-grant-flow">client credentials grant flow</see>.
/// </remarks>
public record ClientCredentialsRequest
    : TwitchAuthorizationRequest<ClientCredentialsResponse>
{
    public override HttpMethod Method => HttpMethod.Post;
    protected override string Path => "/token";
    public override HttpContent? Content
        => new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", ClientId },
            { "client_secret", ClientSecret },
            { "grant_type", "client_credentials" }
        });

    /// <summary>
    /// The client id of the application to get an access token for.
    /// </summary>
    public required ClientId ClientId { get; init; }
    /// <summary>
    /// The client secret of the application to get an access token for.
    /// </summary>
    public required ClientSecret ClientSecret { get; init; }
}
