using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization.Requests;

namespace TwitchySharp.Api.Authorization.Responses;
/// <summary>
/// Contains device code used in the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#device-code-grant-flow">device code grant flow</see>.
/// </summary>
public class DeviceCodeResponse
{
    /// <summary>
    /// A code that identifies the user using the device.
    /// Used to form a <see cref="DeviceCodeTokenRequest"/> and get an access token for a user.
    /// </summary>
    [JsonRequired]
    public string DeviceCode { get; } = string.Empty;
    /// <summary>
    /// The time in seconds until the <see cref="DeviceCode"/> becomes invalid.
    /// </summary>
    [JsonRequired]
    public int ExpiresIn { get; } = 0;
    /// <summary>
    /// The time in seconds until another valid device code can be requested.
    /// </summary>
    [JsonRequired]
    public int Interval { get; } = 0;
    /// <summary>
    /// The code that the user will use to authenticate at the <see cref="VerificationUri"/>.
    /// </summary>
    [JsonRequired]
    public string UserCode { get; } = string.Empty;
    /// <summary>
    /// The URI that the user should be directed to to authorize the app.
    /// </summary>
    [JsonRequired]
    public string VerificationUri { get; } = string.Empty;
}
