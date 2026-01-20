using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Identifies a given user when using the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#device-code-grant-flow">Device Code Grant Flow</see>.
/// </summary>
/// <param name="Value">The string value of the device code.</param>
[JsonConverter(typeof(WrapperJsonConverter<DeviceCode, string>))]
public readonly record struct DeviceCode(string Value) : IWrapValue<string>
{
    public static implicit operator string(DeviceCode secret)
        => secret.Value;
    public override string ToString()
        => Value;
}
