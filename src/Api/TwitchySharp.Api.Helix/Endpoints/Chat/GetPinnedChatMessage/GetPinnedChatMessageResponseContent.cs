namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Contains a list of pinned messages.
/// </summary>
public record GetPinnedChatMessageResponseContent
{
    /// <summary>
    /// The list of pinned messages.
    /// </summary>
    /// <remarks>
    /// The list is empty is no messages are pinned.
    /// </remarks>
    public required PinnedChatMessage[] Data { get; init; }
}
