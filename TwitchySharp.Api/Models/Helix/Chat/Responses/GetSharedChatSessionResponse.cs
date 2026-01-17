using TwitchySharp.Api.Models.Helix.Chat.Models;

namespace TwitchySharp.Api.Models.Helix.Chat.Responses;
/// <summary>
/// Contains a list of shared chat sessions.
/// </summary>
public record GetSharedChatSessionResponse
{
    /// <summary>
    /// A list containing the single shared chat session.
    /// </summary>
    public required SharedChatSession[] Data { get; init; }
}