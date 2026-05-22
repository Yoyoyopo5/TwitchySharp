using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Webhooks.Functional;

/// <summary>
/// The HMAC signature that you use to verify that Twitch sent the message.
/// </summary>
/// <param name="Value">The string value of the signature.</param>
[Wrapper<string>]
public readonly partial record struct WebhookMessageSignature(string Value);
