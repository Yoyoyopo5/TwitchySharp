using TwitchySharp.Api.Models.Helix.Chat.Models;

namespace TwitchySharp.Api.Models.Helix.Chat.Responses;
/// <summary>
/// Contains data about a broadcaster's custom chat badges.
/// These include custom bits and sub badges.
/// </summary>
public record GetChannelChatBadgesResponse
{
    /// <summary>
    /// The list of chat badges. 
    /// The list is sorted in ascending order by <see cref="ChatBadgeSet.SetId"/>.
    /// </summary>
    public required ChatBadgeSet[] Data { get; init; }
}