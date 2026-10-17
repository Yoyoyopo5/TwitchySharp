using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// A cheermote tier for a pinned message cheermote.
/// </summary>
/// <param name="Value">The integer value of the tier.</param>
[Wrapper<int>]
public readonly partial record struct PinnedMessageCheermoteTier(int Value);
