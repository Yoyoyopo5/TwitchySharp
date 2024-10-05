using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Used to get a device code from Twitch which can be used to get a user access token for a specific device.
/// Uses the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#device-code-grant-flow">device code grant flow</see>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="scopes">The <see href="https://dev.twitch.tv/docs/authentication/scopes/">authorization scopes</see> to request.</param>
public class DeviceCodeRequest(string clientId, IEnumerable<Scope> scopes)
    : AuthorizationApiRequest<DeviceCodeResponse>("/device")
{
    /// <summary>
    /// The client id of the application requesting the device code.
    /// </summary>
    public new string ClientId { get; } = clientId;
    /// <summary>
    /// The <see href="https://dev.twitch.tv/docs/authentication/scopes/">authorization scopes</see> that will be requested by the application.
    /// </summary>
    public IEnumerable<Scope> Scopes { get; } = scopes;

    public override string? Data => $"client_id={ClientId}&scopes={Scopes.FormatScopes()}";
    public override HttpMethod Method => HttpMethod.Post;
    public override string ContentType => "application/x-www-form-urlencoded";
}
