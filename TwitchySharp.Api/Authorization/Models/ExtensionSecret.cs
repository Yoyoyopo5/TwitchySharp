using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Authorization;

/// <summary>
/// An extension shared secret.
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/extensions/building/#managing-extension-secrets">Managing Extension Secrets</see> for more information.
/// </remarks>
/// <param name="Value">The string value of the shared secret.</param>
public readonly record struct ExtensionSecret(string Value) : IWrapValue<string>
{
    public static implicit operator string(ExtensionSecret secret)
        => secret.Value;
    public override string ToString()
        => Value;
}
