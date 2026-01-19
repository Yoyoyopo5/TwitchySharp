using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Contains chat setttings.
/// </summary>
public record ChatSettings
{
    /// <summary>
    /// The user id of the broadcaster who has the chat settings.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// Determines whether chat messages must contain only emotes. 
    /// Is <see langword="true"/> if chat messages may contain only emotes; otherwise, <see langword="false"/>.
    /// </summary>
    public required bool EmoteMode { get; init; }
    /// <summary>
    /// Determines whether the broadcaster restricts the chat room to followers only. 
    /// Is <see langword="true"/> if the broadcaster restricts the chat room to followers only; otherwise, <see langword="false"/>.
    /// See <see cref="FollowerModeDuration"/> for how long users must follow the broadcaster before being able to participate in the chat room.
    /// </summary>
    public required bool FollowerMode { get; init; }
    /// <summary>
    /// The length of time, in <b>minutes</b>, that users must follow the broadcaster before being able to participate in the chat room. 
    /// Is <see langword="null"/> if <see cref="FollowerMode"/> is <see langword="false"/>.
    /// </summary>
    public int? FollowerModeDuration { get; init; }
    /// <summary>
    /// The moderator’s user id that was used in the request. 
    /// This property is <see langword="null"/> if the request specifies a user access token that includes <see cref="Scope.ModeratorReadChatSettings"/>.
    /// </summary>
    public string? ModeratorId { get; init; }
    /// <summary>
    /// Determines whether the broadcaster adds a short delay before chat messages appear in the chat room. 
    /// This gives chat moderators and bots a chance to remove them before viewers can see the message. 
    /// See <see cref="NonModeratorChatDelayDuration"/> for the length of the delay. Is <see langword="true"/> if the broadcaster applies a delay; otherwise, <see langword="false"/>. 
    /// <br/>
    /// This property is <see langword="null"/> unless the request specifies a user access token that includes <see cref="Scope.ModeratorReadChatSettings"/> and the user in the moderator_id query parameter is one of the broadcaster’s moderators.
    /// </summary>
    public bool? NonModeratorChatDelay { get; init; }
    /// <summary>
    /// The amount of time, in <b>seconds</b>, that messages are delayed before appearing in chat. 
    /// Is <see langword="null"/> if <see cref="NonModeratorChatDelay"/> is <see langword="false"/>.
    /// <br/>
    /// This property is <see langword="null"/> unless the request specifies a user access token that includes the <see cref="Scope.ModeratorReadChatSettings"/> scope and the user in the moderator_id query parameter is one of the broadcaster’s moderators.
    /// </summary>
    public int? NonModeratorChatDelayDuration { get; init; }
    /// <summary>
    /// Determines whether the broadcaster limits how often users in the chat room are allowed to send messages.
    /// Is <see langword="true"/> if the broadcaster applies a delay; otherwise, <see langword="false"/>. 
    /// See <see cref="SlowModeWaitTime"/> for the delay.
    /// </summary>
    public required bool SlowMode { get; init; }
    /// <summary>
    /// The amount of time, in <b>seconds</b>, that users must wait between sending messages. 
    /// Is <see langword="null"/> if <see cref="SlowMode"/> is <see langword="false"/>.
    /// </summary>
    public int? SlowModeWaitTime { get; init; }
    /// <summary>
    /// Determines whether only users that subscribe to the broadcaster’s channel may talk in the chat room.
    /// Is <see langword="true"/> if the broadcaster restricts the chat room to subscribers only; otherwise, <see langword="false"/>.
    /// </summary>
    public required bool SubscriberMode { get; init; }
    /// <summary>
    /// Determines whether the broadcaster requires users to post only unique messages in the chat room.
    /// Is <see langword="true"/> if the broadcaster requires unique messages only; otherwise, <see langword="false"/>.
    /// </summary>
    public required bool UniqueChatMode { get; init; }
}
