using System;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers.JsonConverters;

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
    public required DeviceCode DeviceCode { get; init; }
    /// <summary>
    /// The time until the <see cref="DeviceCode"/> becomes invalid.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan ExpiresIn { get; init; }
    /// <summary>
    /// The time until another valid device code can be requested.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan Interval { get; init; }
    /// <summary>
    /// The code that the user will use to authenticate at the <see cref="VerificationUri"/>.
    /// </summary>
    public required string UserCode { get; init; }
    /// <summary>
    /// The URI that the user should be directed to to authorize the app.
    /// </summary>
    public required Uri VerificationUri { get; init; }
}
