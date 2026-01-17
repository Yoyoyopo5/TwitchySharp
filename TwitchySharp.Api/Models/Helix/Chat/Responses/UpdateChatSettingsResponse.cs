using TwitchySharp.Api.Models.Helix.Chat.Models;

namespace TwitchySharp.Api.Models.Helix.Chat.Responses;
/// <summary>
/// Contains a list of chat settings.
/// </summary>
public record UpdateChatSettingsResponse
{
    /// <summary>
    /// A list containing a single value of the chat settings after being updated.
    /// </summary>
    public required ChatSettings[] Data { get; init; }
}
