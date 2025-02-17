using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.Chat;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatMessage"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatmessage">Channel Chat Message</see> for more information.
/// </remarks>
public record ChannelChatMessageNotification : EventSubNotification<ChannelChatMessageEvent, ChannelChatMessageCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelChatMessage"/>.
/// </summary>
public record ChannelChatMessageCondition
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the message was sent in.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The id of the user reading the message (usually a bot).
    /// </summary>
    public required string UserId { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatMessage"/> event.
/// </summary>
public record ChannelChatMessageEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) the message was sent in.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) the message was sent in.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) the message was sent in.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the user who sent the message.
    /// </summary>
    public required string ChatterUserId { get; init; }
    /// <summary>
    /// The display name of the user who sent the message.
    /// </summary>
    public required string ChatterUserName { get; init; }
    /// <summary>
    /// The login (username) of the user who sent the message.
    /// </summary>
    public required string ChatterUserLogin { get; init; }
    /// <summary>
    /// The id of the message.
    /// </summary>
    public required string MessageId { get; init; }
    /// <summary>
    /// The chat message.
    /// </summary>
    public required ChannelChatMessage Message { get; init; }
    /// <summary>
    /// The type of message.
    /// </summary>
    public required ChannelChatMessageType MessageType { get; init; }
    /// <summary>
    /// The badges of the chatter.
    /// </summary>
    public required ChannelChatMessageBadge[] Badges { get; init; }
    /// <summary>
    /// The cheer if the message contains a bits cheer.
    /// </summary>
    public ChannelChatMessageCheer? Cheer { get; init; }
    /// <summary>
    /// The color of the chatter's name in the chat room.
    /// This is a hexadecimal RGB color code in the form <c>#&lt;RGB&gt;</c>. 
    /// This may be empty if the user hasn't picked a name color.
    /// </summary>
    public required string Color { get; init; }
    /// <summary>
    /// The reply if the message is a reply to another message.
    /// </summary>
    public ChannelChatMessageReply? Reply { get; init; }
    /// <summary>
    /// The id of the channel points custom reward that was redeemed if the message included one.
    /// </summary>
    public string? ChannelPointsCustomRewardId { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) the message came from if it was sent during a shared chat session from another broadcaster's chat.
    /// </summary>
    public string? SourceBroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) the message came from if it was sent during a shared chat session from another broadcaster's chat.
    /// </summary>
    public string? SourceBroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) the message came from if it was sent during a shared chat session from another broadcaster's chat.
    /// </summary>
    public string? SourceBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the message in the source broadcaster's chat.
    /// Is <see langword="null"/> if the message did not come from another broadcaster during a shared chat session.
    /// </summary>
    public string? SourceMessageId { get; init; }
    /// <summary>
    /// The badges of the chatter in the source broadcaster's chat.
    /// Is <see langword="null"/> if the message did not come from another broadcaster during a shared chat session.
    /// </summary>
    public ChannelChatMessageBadge[]? SourceBadges { get; init; }
}

/// <summary>
/// Contains information about a specific message thread.
/// </summary>
public record ChannelChatMessageReply
{
    /// <summary>
    /// The id of the parent message of the thread.
    /// </summary>
    public required string ParentMessageId { get; init; }
    /// <summary>
    /// The text of the parent message of the thread.
    /// </summary>
    public required string ParentMessageBody { get; init; }
    /// <summary>
    /// The id of the user that sent the parent message of the thread.
    /// </summary>
    public required string ParentUserId { get; init; }
    /// <summary>
    /// The display name of the user that sent the parent message of the thread.
    /// </summary>
    public required string ParentUserName { get; init; }
    /// <summary>
    /// The login (username) of the user that sent the parent message of the thread.
    /// </summary>
    public required string ParentUserLogin { get; init; }
    /// <summary>
    /// The id of the last message of the thread.
    /// </summary>
    public required string ThreadMessageId { get; init; }
    /// <summary>
    /// The id of the user that sent the last message of the thread.
    /// </summary>
    public required string ThreadUserId { get; init; }
    /// <summary>
    /// The display name of the user that sent the last message of the thread.
    /// </summary>
    public required string ThreadUserName { get; init; }
    /// <summary>
    /// The login (username) of the user that sent the last message of the thread.
    /// </summary>
    public required string ThreadUserLogin { get; init; }
}

/// <summary>
/// Contains information about a cheer in a chat message.
/// </summary>
public record ChannelChatMessageCheer
{
    /// <summary>
    /// The amount of bits the user cheered.
    /// </summary>
    public required int Bits { get; init; }
}

/// <summary>
/// Contains information about a specific badge displayed next to a chatter's display name.
/// </summary>
public record ChannelChatMessageBadge
{
    /// <summary>
    /// The id of the set that this badge belongs to
    /// (e.g. <c>Bits</c> or <c>Subscriber</c>).
    /// </summary>
    public required string SetId { get; init; }
    /// <summary>
    /// The id of the badge. 
    /// The exact meaning of this id varies by badge set.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// Extra metadata about the badge.
    /// Currently, this tag contains metadata only for subscriber badges, to indicate the number of months the user has been a subscriber.
    /// </summary>
    public required string Info { get; init; }
}

/// <summary>
/// Contains static definitions for possible types of chat messages.
/// </summary>
/// <param name="Value"></param>
public record ChannelChatMessageType(string Value)
    : ValueBackedEnum<string>(Value)
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

/// <summary>
/// Contains information about a specific chat message.
/// </summary>
public record ChannelChatMessage
{
    /// <summary>
    /// The text of the message.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The message fragments.
    /// </summary>
    public required ChannelChatMessageFragment[] Fragments { get; init; }
}

/// <summary>
/// Contains information about a specific message fragment.
/// </summary>
public record ChannelChatMessageFragment
{
    /// <summary>
    /// The fragment type.
    /// </summary>
    public required ChannelChatMessageFragmentType Type { get; init; }
    /// <summary>
    /// The text of the fragment.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The cheermote, if the <see cref="Type"/> is <see cref="ChannelChatMessageFragmentType.Cheermote"/>.
    /// </summary>
    public ChannelChatMessageCheermote? Cheermote { get; init; }
    /// <summary>
    /// The emote, if the <see cref="Type"/> is <see cref="ChannelChatMessageFragmentType.Emote"/>.
    /// </summary>
    public ChannelChatMessageEmote? Emote { get; init; }
    /// <summary>
    /// The mention, if the <see cref="Type"/> is <see cref="ChannelChatMessageFragmentType.Mention"/>.
    /// </summary>
    public ChannelChatMessageMention? Mention { get; init; }
}

/// <summary>
/// Contains static definitions for possible message fragment types.
/// </summary>
/// <param name="Value"></param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ChannelChatMessageFragmentType, string>))]
public record ChannelChatMessageFragmentType(string Value)
    : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// A plain-text message fragment.
    /// </summary>
    public static ChannelChatMessageFragmentType Text { get; } = new("text");
    /// <summary>
    /// A bits cheer.
    /// </summary>
    public static ChannelChatMessageFragmentType Cheermote { get; } = new("cheermote");
    /// <summary>
    /// An emote.
    /// </summary>
    public static ChannelChatMessageFragmentType Emote { get; } = new("emote");
    /// <summary>
    /// A mention.
    /// </summary>
    public static ChannelChatMessageFragmentType Mention { get; } = new("mention");
}

/// <summary>
/// Contains information about a specific bits cheer in a chat message.
/// </summary>
public record ChannelChatMessageCheermote
{
    /// <summary>
    /// The name portion of the Cheermote string that you use in chat to cheer Bits. 
    /// The full Cheermote string is the concatenation of {prefix} + {number of Bits}.
    /// </summary>
    /// <remarks>
    /// For example, if the prefix is “Cheer” and you want to cheer 100 Bits, the full Cheermote string is Cheer100. 
    /// When the Cheermote string is entered in chat, Twitch converts it to the image associated with the Bits tier that was cheered.
    /// </remarks>
    public required string Prefix { get; init; }
    /// <summary>
    /// The amount of bits cheered.
    /// </summary>
    public required int Bits { get; init; }
    /// <summary>
    /// The tier level of the cheermote.
    /// </summary>
    public required int Tier { get; init; }
}

/// <summary>
/// Contains information about a specific emote in a chat message.
/// </summary>
public record ChannelChatMessageEmote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The id of the set the emote belongs to.
    /// </summary>
    public required string EmoteSetId { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) who owns the emote.
    /// </summary>
    public required string OwnerId { get; init; }
    /// <summary>
    /// The formats the emote is available in.
    /// </summary>
    public required ChannelChatMessageEmoteFormat[] Format { get; init; }
}

/// <summary>
/// Contains information about a specific mention in a chat message.
/// </summary>
public record ChannelChatMessageMention
{
    /// <summary>
    /// The id of the user that was mentioned.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The display name of the user that was mentioned.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The login (username) of the user that was mentioned.
    /// </summary>
    public required string UserLogin { get; init; }
}

/// <summary>
/// Contains static definitions of possible emote formats.
/// </summary>
/// <param name="Value"></param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ChannelChatMessageEmoteFormat, string>))]
public record ChannelChatMessageEmoteFormat(string Value)
    : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// An animated GIF.
    /// </summary>
    public static ChannelChatMessageEmoteFormat Animated { get; } = new("animated");
    /// <summary>
    /// A static PNG.
    /// </summary>
    public static ChannelChatMessageEmoteFormat Static { get; } = new("static");
}
