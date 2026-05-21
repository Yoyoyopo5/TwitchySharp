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
    public Uri ToUri() => new(Value);
}
