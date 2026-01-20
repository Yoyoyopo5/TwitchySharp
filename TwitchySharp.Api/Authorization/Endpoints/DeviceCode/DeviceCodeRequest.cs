using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Used to get a device code from Twitch which can be used to get a user access token for a specific device.
/// </summary>
/// <remarks>
/// Uses the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#device-code-grant-flow">device code grant flow</see>.
/// </remarks>
public record DeviceCodeRequest
    : TwitchAuthorizationRequest<DeviceCodeResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="scopes">The <see href="https://dev.twitch.tv/docs/authentication/scopes/">authorization scopes</see> to request.</param>
    public DeviceCodeRequest(ClientId clientId, IEnumerable<Scope> scopes)
        : base("/device")
    {
        Method = HttpMethod.Post;
        ClientId = clientId;
        Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", clientId },
            { "scopes", scopes.FormatScopes() }
        });
    }
}
