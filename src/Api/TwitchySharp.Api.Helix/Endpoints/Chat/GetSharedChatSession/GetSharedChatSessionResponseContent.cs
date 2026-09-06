namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains a list of shared chat sessions.
/// </summary>
public record GetSharedChatSessionResponseContent
{
    /// <summary>
    /// A list containing the single shared chat session.
    /// </summary>
    public required SharedChatSession[] Data { get; init; }
}
