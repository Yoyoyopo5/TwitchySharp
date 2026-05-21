namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.WhisperReceived"/> event.
/// </summary>
public record WhisperReceivedEvent
{
    /// <summary>
    /// The id of the user sending the message.
    /// </summary>
    public required UserId FromUserId { get; init; }
    /// <summary>
    /// The display name of the user sending the message.
    /// </summary>
    public required UserName FromUserName { get; init; }
    /// <summary>
    /// The login (username) of the user sending the message.
    /// </summary>
    public required UserLogin FromUserLogin { get; init; }
    /// <summary>
    /// The id of the user receiving the message.
    /// </summary>
    public required UserId ToUserId { get; init; }
    /// <summary>
    /// The display name of the user receiving the message.
    /// </summary>
    public required UserName ToUserName { get; init; }
    /// <summary>
    /// The login (username) of the user receiving the message.
    /// </summary>
    public required UserLogin ToUserLogin { get; init; }
    /// <summary>
    /// The id of the whisper.
    /// </summary>
    public required WhisperId WhisperId { get; init; }
    /// <summary>
    /// The whisper message.
    /// </summary>
    public required WhisperMessage Message { get; init; }
}
