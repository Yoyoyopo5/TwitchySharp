using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api;

/// <summary>
/// An extension shared secret.
/// </summary>
/// <remarks>
/// <para>
/// This must be a base64 encoded string.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/extensions/building/#managing-extension-secrets">Managing Extension Secrets</see> for more information.
/// </remarks>
/// <param name="Value">The string value of the shared secret.</param>
[Wrapper<string>]
public readonly partial record struct ExtensionSecret(string Value)
{
    public byte[] Bytes { get; } = Convert.FromBase64String(Value);
}
