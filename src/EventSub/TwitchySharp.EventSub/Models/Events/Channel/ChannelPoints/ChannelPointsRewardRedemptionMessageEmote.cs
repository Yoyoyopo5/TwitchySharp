using TwitchySharp.EventSub.Enums.Events;
using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel.ChannelPoints;

/// <summary>
/// Contains information about a specific emote in a channel points reward redemption chat message.
/// </summary>
public record ChannelPointsRewardRedemptionMessageEmote : IChatMessageEmote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The character index of the chat message where the emote begins.
    /// </summary>
    public required int Begin { get; init; }
    /// <summary>
    /// The character index of the chat message where the emote ends.
    /// </summary>
    public required int End { get; init; }

    /// <summary>
    /// Not supported for this event type.
    /// Set to <see cref="string.Empty"/>.
    /// </summary>
    string IChatMessageEmote.EmoteSetId => string.Empty;
    /// <summary>
    /// Not supported for this event type.
    /// Set to <see cref="string.Empty"/>.
    /// </summary>
    string IChatMessageEmote.OwnerId => string.Empty;
    private readonly static ChatMessageEmoteFormat[] _format = [];
    /// <summary>
    /// Not supported for this event type.
    /// Set to <see langword="null"/>.
    /// </summary>
    IEnumerable<ChatMessageEmoteFormat> IChatMessageEmote.Format => _format;
}
