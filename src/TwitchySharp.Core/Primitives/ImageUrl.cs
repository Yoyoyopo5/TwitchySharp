using System;
using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// A url pointing to a specific image.
/// </summary>
/// <param name="Value">The string value of the url.</param>
[Wrapper<string>]
public readonly partial record struct ImageUrl(string Value)
{
    /// <summary>
    /// Create a <see cref="Uri"/> from the url <see cref="Value"/>.
    /// </summary>
    /// <returns>A <see cref="Uri"/> of the image url.</returns>
    /// <exception cref="UriFormatException"/>
    public Uri ToUri() => new(Value);
}
