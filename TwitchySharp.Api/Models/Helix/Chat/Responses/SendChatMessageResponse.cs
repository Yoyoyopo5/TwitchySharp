using TwitchySharp.Api.Models.Helix.Chat.Models;

namespace TwitchySharp.Api.Models.Helix.Chat.Responses;
/// <summary>
/// Contains a list of sent messages.
/// </summary>
public record SendChatMessageResponse
{
    /// <summary>
    /// Contains a single entry for the sent message.
    /// </summary>
    public required SentMessage[] Data { get; init; }
}