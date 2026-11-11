using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

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
    /// <summary>
    /// Use <see cref="ExtensionId"/> as <see cref="ClientId"/>.
    /// </summary>
    /// <param name="id">The <see cref="ExtensionId"/> to use.</param>
    public static implicit operator ClientId(ExtensionId id)
        => new(id.Value);
}
