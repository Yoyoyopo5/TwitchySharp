using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Helpers.JsonConverters.DateTime;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Automod;
public record AutomodMessageHoldNotification : EventSubNotification<AutomodMessageHoldEvent, AutomodMessageHoldCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.AutomodMessageHold"/>.
/// </summary>
public record AutomodMessageHoldCondition
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get Automod Message Hold notifications for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's chat.
    /// </summary>
    public required string ModeratorUserId { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.AutomodMessageHold"/> event.
/// </summary>
public record AutomodMessageHoldEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the Automod caught the message for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the Automod caught the message for.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the Automod caught the message for.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the user that sent the caught message.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that sent the caught message.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that sent the caught message.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The id of the message that was flagged by the Automod.
    /// </summary>
    public required string MessageId { get; init; }
    /// <summary>
    /// The message that was flagged.
    /// </summary>
    public required AutomodCaughtChatMessage Message { get; init; }
    /// <summary>
    /// The category that the message was flagged under.
    /// Dev Note: not sure what all of the possible values are for this, so I'm leaving as a string for now.
    /// </summary>
    public required string Category { get; init; }
    /// <summary>
    /// The level of severity for the caught message.
    /// Ranges from 1 to 4.
    /// </summary>
    public required int Level { get; init; }
    /// <summary>
    /// The date and time when the Automod caught the message.
    /// </summary>
    public required DateTimeOffset HeldAt { get; init; }
}

/// <summary>
/// Contains information about the message that was caught by Automod.
/// </summary>
public record AutomodCaughtChatMessage
{
    /// <summary>
    /// The full text of the message.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The segments of the message that triggered the Automod.
    /// </summary>
    public required AutomodCaughtMessageFragment[] Fragments { get; init; }
}

/// <summary>
/// Contains information about an individual message fragment that triggered Automod.
/// </summary>
public record AutomodCaughtMessageFragment
{
    /// <summary>
    /// The type of message fragment.
    /// </summary>
    public required AutomodCaughtMessageFragmentType Type { get; init; }
    /// <summary>
    /// The text of the fragment.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The emote that triggered the Automod, if any.
    /// </summary>
    public AutomodCaughtChatEmote? Emote { get; init; }
    /// <summary>
    /// The bits cheermote that triggered the Automod, if any.
    /// </summary>
    public AutomodCaughtCheermote? Cheermote { get; init; }
}

/// <summary>
/// Contains static definitions for potential Automod message fragment types.
/// </summary>
/// <param name="Value"></param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<AutomodCaughtMessageFragmentType, string>))]
public record AutomodCaughtMessageFragmentType(string Value)
    : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// A text fragment.
    /// </summary>
    public static AutomodCaughtMessageFragmentType Text { get; } = new("text");
    /// <summary>
    /// An emote fragment.
    /// </summary>
    public static AutomodCaughtMessageFragmentType Emote { get; } = new("emote");
    /// <summary>
    /// A bits cheermote fragment.
    /// </summary>
    public static AutomodCaughtMessageFragmentType Cheermote { get; } = new("cheermote");
}

/// <summary>
/// Contains information about a specific chat emote that triggered Automod.
/// </summary>
public record AutomodCaughtChatEmote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The id of the emote set that the emote belongs to.
    /// </summary>
    public required string EmoteSetId { get; init; }
}

/// <summary>
/// Contains information about a specific cheermote that triggered Automod.
/// </summary>
public record AutomodCaughtCheermote
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
