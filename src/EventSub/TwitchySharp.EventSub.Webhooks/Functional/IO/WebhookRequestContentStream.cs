using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Webhooks.Functional;

[Wrapper<Stream>]
public readonly partial record struct WebhookRequestContentStream(Stream Value);
