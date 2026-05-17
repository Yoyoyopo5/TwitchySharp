using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Webhooks.Http;

/// <summary>
/// The UTC date and time (in RFC3339 format) that Twitch sent the notification.
/// </summary>
/// <param name="Value">The timestamp value in RFC3339 string format.</param>
// I'm leaving this as a string value since its main use is validating the request hash.
[Wrapper<string>]
public readonly partial record struct EventSubMessageTimestamp(string Value);
