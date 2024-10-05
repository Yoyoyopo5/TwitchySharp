using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Contains device code used in the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#device-code-grant-flow">device code grant flow</see>.
/// </summary>
public record DeviceCodeResponse
{
    /// <summary>
    /// A code that identifies the user using the device.
    /// Used to form a <see cref="DeviceCodeTokenRequest"/> and get an access token for a user.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string DeviceCode { get; private set; } = string.Empty;
    /// <summary>
    /// The time in seconds until the <see cref="DeviceCode"/> becomes invalid.
    /// </summary>
    [JsonInclude, JsonRequired]
    public int ExpiresIn { get; private set; } = 0;
    /// <summary>
    /// The time in seconds until another valid device code can be requested.
    /// </summary>
    [JsonInclude, JsonRequired]
    public int Interval { get; private set; } = 0;
    /// <summary>
    /// The code that the user will use to authenticate at the <see cref="VerificationUri"/>.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string UserCode { get; private set; } = string.Empty;
    /// <summary>
    /// The URI that the user should be directed to to authorize the app.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string VerificationUri { get; private set; } = string.Empty;
}
