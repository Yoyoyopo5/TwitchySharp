namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains a list of sent messages.
/// </summary>
public record SendChatMessageResponseContent
{
    /// <summary>
    /// Contains a single entry for the sent message.
    /// </summary>
    public required SentMessage[] Data { get; init; }
}
