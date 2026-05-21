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
    public Uri ToUri() => new(Value);
}
