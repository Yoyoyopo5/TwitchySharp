using System;
using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// A url.
/// </summary>
/// <param name="Value">The string value of the url.</param>
[Wrapper<string>]
public readonly partial record struct Url(string Value)
{
    /// <summary>
    /// Create a <see cref="Uri"/> from the <see cref="Url.Value"/>.
    /// </summary>
    /// <returns>A new <see cref="Uri"/> of the <see cref="Url.Value"/>.</returns>
    /// <exception cref="UriFormatException"/>
    public Uri ToUri() => new(Value);
}
