using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Enums.Events.Automod.Terms;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Automod.Terms;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.AutomodTermsUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#automodtermsupdate">Automod Terms Update</see> for more information.
/// </remarks>
public record AutomodTermsUpdateNotification : EventSubNotification<AutomodTermsUpdateEvent, AutomodTermsUpdateCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.AutomodTermsUpdate"/>
/// </summary>
public record AutomodTermsUpdateCondition : BroadcasterModeratorCondition;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.AutomodTermsUpdate"/> event.
/// </summary>
public record AutomodTermsUpdateEvent : IHaveBroadcaster, IHaveModerator
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the Automod terms were updated for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the Automod terms were updated for.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the Automod terms were updated for.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator that updated the Automod terms.
    /// </summary>
    public required string ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator that updated the Automod terms.
    /// </summary>
    public required string ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator that updated the Automod terms.
    /// </summary>
    public required string ModeratorUserName { get; init; }
    /// <summary>
    /// The status change applied to the terms.
    /// </summary>
    public required AutomodTermsUpdateAction Action { get; init; }
    /// <summary>
    /// Inidicates whether this term was added due to an Automod message approve/deny action.
    /// </summary>
    public required bool FromAutomod { get; init; }
    /// <summary>
    /// The list of the terms that had a status change.
    /// </summary>
    public required string[] Terms { get; init; }
}