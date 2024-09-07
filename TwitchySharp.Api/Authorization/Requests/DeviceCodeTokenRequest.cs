using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TwitchySharp.Api.Authorization.Responses;

namespace TwitchySharp.Api.Authorization.Requests;
/// <summary>
/// Used to get a user access token using the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#device-code-grant-flow">device code grant flow</see>
/// </summary>
/// <param name="clientId">The client id of the application making the request.</param>
/// <param name="scopes">The <see href="https://dev.twitch.tv/docs/authentication/scopes/">authorization scopes</see> to request.</param>
/// <param name="deviceCode">The device code obtained from a <see cref="DeviceCodeRequest"/></param>
public class DeviceCodeTokenRequest(string clientId, IEnumerable<Scope> scopes, string deviceCode)
    : AuthorizationApiRequest<DeviceCodeTokenResponse>("/token")
{
    /// <summary>
    /// The client id of the application that the user will authorize.
    /// </summary>
    public new string ClientId { get; } = clientId;
    /// <summary>
    /// The <see href="https://dev.twitch.tv/docs/authentication/scopes/">authorization scopes</see> that will be requested from the user.
    /// </summary>
    public IEnumerable<Scope> Scopes { get; } = scopes;
    /// <summary>
    /// The device code obtained from a previous <see cref="DeviceCodeRequest"/> that will be used in the request.
    /// </summary>
    public string DeviceCode { get; } = deviceCode;

    public override string? Data => $"client_id={ClientId}&scope={Scopes.FormatScopes()}&device_code={DeviceCode}&grant_type=urn:ietf:params:oauth:grant-type:device_code";
    public override HttpMethod Method => HttpMethod.Post;
    public override string ContentType => "application/x-www-form-urlencoded";
}
