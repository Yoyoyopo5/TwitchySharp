using TwitchySharp.EventSub.Enums.Events;
using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel.ChannelPoints;

/// <summary>
/// Contains information about a specific emote used in a reward redemption message.
/// </summary>
public record ChannelPointsRewardRedemptionMessageV2Emote : IChatMessageEmote
{
    public required string Id { get; init; }
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
    /// Set to <see cref="Array.Empty{ChatMessageEmoteFormat}"/>.
    /// </summary>
    IEnumerable<ChatMessageEmoteFormat> IChatMessageEmote.Format => _format;
}
