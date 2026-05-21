namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelSuspiciousUserMessage"/> event.
/// </summary>
public record ChannelSuspiciousUserMessageEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the suspicious user event occurred.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) in whose chat the suspicious user event occurred.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) in whose chat the suspicious user event occurred.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The user id of the suspicious user.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The display name of the suspicious user.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The login (username) of the suspicious user.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The current status of the suspicious user as set by a moderator.
    /// </summary>
    public required SuspiciousUserStatus LowTrustStatus { get; init; }
    /// <summary>
    /// An array of broadcaster (channel) user ids that the broadcaster is sharing bans with where the suspicious user is also banned.
    /// </summary>
    public required UserId[] SharedBanChannelIds { get; init; }
    /// <summary>
    /// The suspicious user types that apply to the suspicious user.
    /// </summary>
    public required ChannelSuspiciousUserType[] Types { get; init; }
    /// <summary>
    /// An evaluation of the likelihood the suspicious user is evading a ban on the broadcaster's channel.
    /// </summary>
    public required SuspiciousUserBanEvasionEvaluationLevel BanEvasionEvaluation { get; init; } // May be nullable, not clear in spec.
    /// <summary>
    /// The chat message sent by the suspicious user.
    /// </summary>
    public required SuspiciousUserChatMessage Message { get; init; }
}
