using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Automod.Message;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.AutomodMessageUpdate"/>
/// </summary>
/// <remarks>
/// <see cref="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#automodmessageupdate">Automod Message Update</see> for more information.
/// </remarks>
public record AutomodMessageUpdateNotification : EventSubNotification<AutomodMessageUpdateEvent, AutomodMessageUpdateCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.AutomodMessageUpdate"/>
/// </summary>
public record AutomodMessageUpdateCondition : BroadcasterModeratorCondition;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.AutomodMessageUpdate"/> event.
/// </summary>
public record AutomodMessageUpdateEvent // Twitch docs are really inconsistent with this event type, may need to revisting in testing.
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the held Automod message was updated in.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the held Automod message was updated in.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the held Automod message was updated in.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the user that sent the original chat message that was held by Automod.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that sent the original chat message that was held by Automod.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that sent the original chat message that was held by Automod.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The user id of the moderator that updated the held Automod message.
    /// </summary>
    public required string ModeratorUserId { get; init; }
    /// <summary>
    /// The display name of the moderator that updated the held Automod message.
    /// </summary>
    public required string ModeratorUserName { get; init; }
    /// <summary>
    /// The login (username) of the moderator that updated the held Automod message.
    /// </summary>
    public required string ModeratorUserLogin { get; init; }
    /// <summary>
    /// The id of the message that was updated.
    /// </summary>
    public required string MessageId { get; init; }
    /// <summary>
    /// The message that was updated.
    /// </summary>
    public required AutomodMessageUpdateChatMessage Message { get; init; }
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
    /// The status of the updated automod message.
    /// </summary>
    public required AutomodMessageUpdateStatus Status { get; init; }
    /// <summary>
    /// The date and time when the Automod caught the message.
    /// </summary>
    public required DateTimeOffset HeldAt { get; init; }
}

/// <summary>
/// Contains information about a chat message that received an automod update.
/// </summary>
public record AutomodMessageUpdateChatMessage
{
    /// <summary>
    /// The content of the message.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// Metadata surrounding the potential inappropriate fragments of the message.
    /// </summary>
    public required AutomodMessageUpdateChatMessageFragment[] Fragments { get; init; }

}

/// <summary>
/// Contains information about a specific fragment of a chat message that was caught by automod.
/// </summary>
public record AutomodMessageUpdateChatMessageFragment
{
    /// <summary>
    /// The message text of the fragment.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The emote of the fragment.
    /// This is <see langword="null"/> if the fragment is not an emote.
    /// </summary>
    public AutomodCaughtChatEmote? Emote { get; init; }
    /// <summary>
    /// The bits cheer emote of the fragment.
    /// This is <see langword="null"/> if the fragment is not a bits cheermote.
    /// </summary>
    public AutomodCaughtCheermote? Cheermote { get; init; }
}

/// <summary>
/// Represents the status of an updated automod message.
/// </summary>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<AutomodMessageUpdateStatus, string>))]
public record AutomodMessageUpdateStatus(string Value) : ValueBackedEnum<string>(Value)
{
    public static AutomodMessageUpdateStatus Approved { get; } = new("approved");
    public static AutomodMessageUpdateStatus Denied { get; } = new("denied");
    public static AutomodMessageUpdateStatus Expired { get; } = new("expired");
}
