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
    public Uri ToUri() => new(Value);
}
