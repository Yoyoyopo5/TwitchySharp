using TwitchySharp.Api.Models.Helix.Chat.Models;

namespace TwitchySharp.Api.Models.Helix.Chat.Responses;
/// <summary>
/// Contains a list of global Twitch chat badges.
/// </summary>
public record GetGlobalChatBadgesResponse
{
    /// <summary>
    /// The list of chat badges. 
    /// The list is sorted in ascending order by <see cref="ChatBadgeSet.SetId"/>.
    /// </summary>
    public required ChatBadgeSet[] Data { get; init; }
}
