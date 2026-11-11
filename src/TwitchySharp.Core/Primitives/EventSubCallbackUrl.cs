using System;
using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// A callback url for a Twitch EventSub webhook subscription.
/// </summary>
/// <param name="Value">The string value of the callback url.</param>
[Wrapper<string>]
public readonly partial record struct EventSubCallbackUrl(string Value)
{
    /// <summary>
    /// Create a <see cref="Uri"/> from the <see cref="Value"/>.
    /// </summary>
    /// <returns>A <see cref="Uri"/> representing the url <see cref="Value"/>.</returns>
    /// <exception cref="UriFormatException"/>
    public Uri ToUri() => new(Value);
}
