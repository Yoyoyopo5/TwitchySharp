using System.Collections.Generic;
using System.Net.Http;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Used to get a user access token using the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#device-code-grant-flow">device code grant flow</see>.
/// </summary>
public record DeviceCodeTokenRequest
    : TwitchAuthorizationRequest<DeviceCodeTokenResponse>
{
    /// <param name="clientId">The client id of the application making the request.</param>
    /// <param name="scopes">The <see href="https://dev.twitch.tv/docs/authentication/scopes/">authorization scopes</see> to request.</param>
    /// <param name="deviceCode">The device code obtained from a <see cref="DeviceCodeRequest"/></param>
    public DeviceCodeTokenRequest(string clientId, IEnumerable<Scope> scopes, string deviceCode)
        : base("/token")
    {
        Method = HttpMethod.Post;
        ClientId = clientId;
        Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", clientId },
            { "scope", scopes.FormatScopes() },
            { "device_code", deviceCode },
            { "grant_type", "urn:ietf:params:oauth:grant-type:device_code" }
        });
    }
}
