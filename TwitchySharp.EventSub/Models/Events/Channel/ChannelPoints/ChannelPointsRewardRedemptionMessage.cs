using TwitchySharp.EventSub.Interfaces;

namespace TwitchySharp.EventSub.Models.Events.Channel.ChannelPoints;

/// <summary>
/// Contains information about a message submitted with a channel points reward redemption.
/// </summary>
public record ChannelPointsRewardRedemptionMessage : IChatMessage
{
    public required string Text { get; init; }
    /// <summary>
    /// The emotes included in the chat message.
    /// </summary>
    public required ChannelPointsRewardRedemptionMessageEmote[] Emotes { get; init; }

    /// <summary>
    /// Not supported for this event type.
    /// Set to <see cref="Array.Empty{IChatMessageFragment}"/>.
    /// </summary>
    private readonly static IChatMessageFragment[] _fragments = [];
    IEnumerable<IChatMessageFragment> IChatMessage.Fragments => _fragments;
}
