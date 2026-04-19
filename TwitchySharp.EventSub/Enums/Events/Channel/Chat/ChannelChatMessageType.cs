using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Channel.Chat;

/// <summary>
/// Contains static definitions for possible types of chat messages.
/// </summary>
/// <param name="Value"></param>
[Wrapper<string>]
public readonly partial record struct ChannelChatMessageType(string Value)
{
    /// <summary>
    /// A plain-text message.
    /// </summary>
    public static ChannelChatMessageType Text { get; } = new("text");
    /// <summary>
    /// A message that has been highlighted via the highlight channel points redemption.
    /// </summary>
    public static ChannelChatMessageType ChannelPointsHighlighted { get; } = new("channel_points_highlighted");
    /// <summary>
    /// A message that has been sent in sub-only mode using the sub-only message channel points redemption.
    /// </summary>
    public static ChannelChatMessageType ChannelPointsSubOnly { get; } = new("channel_points_sub_only");
    /// <summary>
    /// A user's first message in the channel.
    /// </summary>
    public static ChannelChatMessageType UserIntro { get; } = new("user_intro");
    /// <summary>
    /// A message sent with the message effect bits power-up.
    /// </summary>
    public static ChannelChatMessageType PowerUpsMessageEffect { get; } = new("power_ups_message_effect");
    /// <summary>
    /// An emote sent with the gigantify bits power-up.
    /// </summary>
    public static ChannelChatMessageType PowerUpsGigantifiedEmote { get; } = new("power_ups_gigantified_emote");
}
