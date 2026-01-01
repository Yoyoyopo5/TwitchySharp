using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelModerateV2"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelmoderate-v2">Channel Moderate V2</see> for more information.
/// </remarks>
public record ChannelModerateV2Notification : EventSubNotification<ChannelModerateV2Event, ChannelModerateV2Condition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelModerateV2"/>.
/// </summary>
public record ChannelModerateV2Condition : ChannelModerateCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelModerateV2"/> event.
/// </summary>
public record ChannelModerateV2Event : ChannelModerateEvent // This may come back to bite us in the ass later.
{
    /// <summary>
    /// Data associated with a warn command.
    /// This is <see langword="null"/> unless <see cref="ChannelModerateEvent.Action"/> is set to <see cref="ChannelModerateActionType.Warn"/>.
    /// </summary>
    public ChannelModerateWarnAction? Warn { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Warn"/> action.
/// </summary>
public record ChannelModerateWarnAction
{
    /// <summary>
    /// The id of the user being warned.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user being warned.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user being warned.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The reason given for the warning.
    /// </summary>
    public string? Reason { get; init; }
    /// <summary>
    /// Chat rules cited for the warning.
    /// </summary>
    public string[]? ChatRulesCited { get; init; }
}
