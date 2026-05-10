using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Webhooks;

/// <summary>
/// An EventSub webhook secret.
/// </summary>
/// <param name="Value">The <see langword="string"/> value of the secret.</param>
[Wrapper<string>]
public readonly partial record struct WebhookSecret(string Value);
