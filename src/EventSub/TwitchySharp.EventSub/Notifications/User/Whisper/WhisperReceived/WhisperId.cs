using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// The id of a specific Twitch Whisper message.
/// </summary>
/// <param name="Value">The string value of the whisper message id.</param>
[Wrapper<string>]
public readonly partial record struct WhisperId(string Value);
