using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Authentication;
/// <summary>
/// Identifies a given user when using the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#device-code-grant-flow">Device Code Grant Flow</see>.
/// </summary>
/// <param name="Value">The string value of the device code.</param>
[Wrapper<string>]
public readonly partial record struct DeviceCode(string Value);
