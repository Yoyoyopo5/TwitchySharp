using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.User.Whisper;
using TwitchySharp.EventSub.Models.Notifications;

namespace TwitchySharp.EventSub.Models.Notifications.User.Whisper;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.WhisperReceived"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#userwhispermessage">Whisper Received</see> for more information.
/// </remarks>
public record WhisperReceivedNotification : EventSubNotification<WhisperReceivedEvent, WhisperReceivedCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.WhisperReceived"/>.
/// </summary>
public record WhisperReceivedCondition : UserCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.WhisperReceived"/> event.
/// </summary>
public record WhisperReceivedEvent
{
    /// <summary>
    /// The id of the user sending the message.
    /// </summary>
    public required string FromUserId { get; init; }
    /// <summary>
    /// The display name of the user sending the message.
    /// </summary>
    public required string FromUserName { get; init; }
    /// <summary>
    /// The login (username) of the user sending the message.
    /// </summary>
    public required string FromUserLogin { get; init; }
    /// <summary>
    /// The id of the user receiving the message.
    /// </summary>
    public required string ToUserId { get; init; }
    /// <summary>
    /// The display name of the user receiving the message.
    /// </summary>
    public required string ToUserName { get; init; }
    /// <summary>
    /// The login (username) of the user receiving the message.
    /// </summary>
    public required string ToUserLogin { get; init; }
    /// <summary>
    /// The id of the whisper.
    /// </summary>
    public required string WhisperId { get; init; }
    /// <summary>
    /// The whisper message.
    /// </summary>
    public required WhisperMessage Message { get; init; }
}
