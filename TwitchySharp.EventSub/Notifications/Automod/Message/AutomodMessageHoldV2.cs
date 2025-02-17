using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Automod.Message;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.AutomodMessageHoldV2"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#automodmessagehold-v2">Automod Message Hold V2</see> for more information.
/// </remarks>
public record AutomodMessageHoldV2Notification : EventSubNotification<AutomodMessageHoldV2Event, AutomodMessageHoldV2Condition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.AutomodMessageHoldV2"/>
/// </summary>
public record AutomodMessageHoldV2Condition
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
/// Contains information about a specific <see cref="EventSubSubscriptionType.AutomodMessageHoldV2"/> event.
/// </summary>
public record AutomodMessageHoldV2Event
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
    /// The date and time when the Automod caught the message.
    /// </summary>
    public required DateTimeOffset HeldAt { get; init; }
    /// <summary>
    /// The reason the Automod caught the message.
    /// </summary>
    public required AutomodHoldReason Reason { get; init; }
    /// <summary>
    /// Contains information about the Automod settings that caused the hold.
    /// Is <see langword="null"/> unless <see cref="Reason"/> is <see cref="AutomodHoldReason.Automod"/>.
    /// </summary>
    public AutomodHold? Automod { get; init; }
    /// <summary>
    /// Contains information about the blocked term that caused the hold.
    /// Is <see langword="null"/> unless <see cref="Reason"/> is <see cref="AutomodHoldReason.BlockedTerm"/>.
    /// </summary>
    public BlockedTermHold? BlockedTermHold { get; init; }
}

/// <summary>
/// Contains information about a specific Automod hold, including the Automod settings that triggered the hold.
/// </summary>
public record AutomodHold
{
    /// <summary>
    /// The Automod category that triggered the hold.
    /// </summary>
    public required string Category { get; init; }
    /// <summary>
    /// The level of severity of the held message.
    /// </summary>
    public required int Level { get; init; }
    /// <summary>
    /// The bounds of the text that caused the message to be caught.
    /// </summary>
    public required AutomodHoldBoundary[] Boundaries { get; init; }
}

/// <summary>
/// Contains information about a specific location in a message that triggered Automod.
/// </summary>
public readonly record struct AutomodHoldBoundary
{
    /// <summary>
    /// Index in the message for the start of the problem (0 indexed, inclusive).
    /// </summary>
    [JsonPropertyName("start_pos")]
    public required int StartPosition { get; init; }
    /// <summary>
    /// Index in the message for the start of the problem (0 indexed, inclusive).
    /// </summary>
    [JsonPropertyName("end_pos")]
    public required int EndPosition { get; init; }
}

/// <summary>
/// Contains information about a specific Automod hold that was triggered by a blocked term.
/// </summary>
public record BlockedTermHold
{
    /// <summary>
    /// The list of blocked terms found in the message.
    /// </summary>
    public required BlockedTerm[] TermsFound { get; init; }
}

/// <summary>
/// Contains information about a specific blocked term found in a message held by Automod.
/// </summary>
public record BlockedTerm
{
    /// <summary>
    /// The id of the blocked term.
    /// </summary>
    public required string TermId { get; init; }
    /// <summary>
    /// The bounds of the blocked term that caused the message to be caught.
    /// </summary>
    public required AutomodHoldBoundary Boundary { get; init; }
    /// <summary>
    /// The user id of the broadcaster that owns the blocked term.
    /// </summary>
    public required string OwnerBroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster that owns the blocked term.
    /// </summary>
    public required string OwnerBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster that owns the blocked term.
    /// </summary>
    public required string OwnerBroadcasterUserName { get; init; }
}

/// <summary>
/// Contains static definitions for possible reasons Automod holds a message.
/// </summary>
/// <param name="Value"></param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<AutomodHoldReason, string>))]
public record AutomodHoldReason(string Value)
    : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// The Automod held the message due to its configuration.
    /// </summary>
    public static AutomodHoldReason Automod { get; } = new("automod");
    /// <summary>
    /// The Automod held the message due to a manually blocked term appearing in it.
    /// </summary>
    public static AutomodHoldReason BlockedTerm { get; } = new("blocked_term");
}
