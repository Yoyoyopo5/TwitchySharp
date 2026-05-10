using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub;

/// <summary>
/// Wraps a <see cref="Stream"/> of UTF8 encoded JSON text.
/// </summary>
/// <remarks>
/// Use with <see cref="NotificationDeserializer"/>.
/// </remarks>
/// <param name="Value">The JSON stream.</param>
[Wrapper<Stream>]
public readonly partial record struct NotificationPayloadStream(Stream Value);
