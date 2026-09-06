namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains a list of global Twitch chat badges.
/// </summary>
public record GetGlobalChatBadgesResponseContent
{
    /// <summary>
    /// The list of chat badges. 
    /// The list is sorted in ascending order by <see cref="ChatBadgeSet.SetId"/>.
    /// </summary>
    public required ChatBadgeSet[] Data { get; init; }
}
