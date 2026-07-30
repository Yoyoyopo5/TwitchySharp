namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatMessage"/> event.
/// </summary>
public record ChannelChatMessageEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) the message was sent in.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) the message was sent in.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) the message was sent in.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the user who sent the message.
    /// </summary>
    public required UserId ChatterUserId { get; init; }
    /// <summary>
    /// The display name of the user who sent the message.
    /// </summary>
    public required UserName ChatterUserName { get; init; }
    /// <summary>
    /// The login (username) of the user who sent the message.
    /// </summary>
    public required UserLogin ChatterUserLogin { get; init; }
    /// <summary>
    /// The id of the message.
    /// </summary>
    public required MessageId MessageId { get; init; }
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
    public required RgbColor Color { get; init; }
    /// <summary>
    /// The reply if the message is a reply to another message.
    /// </summary>
    public ChannelChatMessageReply? Reply { get; init; }
    /// <summary>
    /// The id of the channel points custom reward that was redeemed if the message included one.
    /// </summary>
    public RewardId? ChannelPointsCustomRewardId { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) the message came from if it was sent during a shared chat session from another broadcaster's chat.
    /// </summary>
    public UserId? SourceBroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) the message came from if it was sent during a shared chat session from another broadcaster's chat.
    /// </summary>
    public UserName? SourceBroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) the message came from if it was sent during a shared chat session from another broadcaster's chat.
    /// </summary>
    public UserLogin? SourceBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the message in the source broadcaster's chat.
    /// Is <see langword="null"/> if the message did not come from another broadcaster during a shared chat session.
    /// </summary>
    public MessageId? SourceMessageId { get; init; }
    /// <summary>
    /// The badges of the chatter in the source broadcaster's chat.
    /// Is <see langword="null"/> if the message did not come from another broadcaster during a shared chat session.
    /// </summary>
    public ChannelChatMessageBadge[]? SourceBadges { get; init; }
}
