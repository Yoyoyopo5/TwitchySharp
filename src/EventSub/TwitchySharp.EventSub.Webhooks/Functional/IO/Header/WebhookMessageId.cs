using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Webhooks.Functional;

/// <summary>
/// An ID that uniquely identifies this message.
/// </summary>
/// <remarks>
/// This is an opaque ID, and is not required to be in any particular format.
/// </remarks>
[Wrapper<string>]
public readonly partial record struct WebhookMessageId(string Value);
