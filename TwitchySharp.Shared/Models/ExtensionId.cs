using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch extension.
/// </summary>
/// <remarks>
/// This also functions as a <see cref="ClientId"/> and can be used to authenticate requests.
/// </remarks>
/// <param name="Value">The string value of the extension id.</param>
[Wrapper<string>]
public readonly partial record struct ExtensionId(string Value)
{
    public static implicit operator ClientId(ExtensionId id)
        => new(id.Value);
}