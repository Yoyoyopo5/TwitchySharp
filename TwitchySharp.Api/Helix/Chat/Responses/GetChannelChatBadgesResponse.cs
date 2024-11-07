using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains data about a broadcaster's custom chat badges.
/// These include custom bits and sub badges.
/// </summary>
public record GetChannelChatBadgesResponse
{
    /// <summary>
    /// The list of chat badges. 
    /// The list is sorted in ascending order by <see cref="ChannelChatBadgeSet.SetId"/>.
    /// </summary>
    public required ChannelChatBadgeSet[] Data { get; init; }
}

/// <summary>
/// Contains a list of <see cref="ChannelChatBadge"/> in a given set.
/// For example, a set may include subscriber badges.
/// </summary>
public record ChannelChatBadgeSet
{
    /// <summary>
    /// An ID that identifies this set of chat badges. 
    /// For example, Bits or Subscriber.
    /// </summary>
    public required string SetId { get; init; }
    /// <summary>
    /// The list of chat badges in this set.
    /// The list is sorted in ascending order by <see cref="ChannelChatBadge.Id"/>.
    /// </summary>
    public required ChannelChatBadge[] Versions { get; init; }
}

/// <summary>
/// Contains information about a specific custom chat badge.
/// </summary>
public record ChannelChatBadge
{
    /// <summary>
    /// An ID that identifies this version of the badge. 
    /// The ID can be any value. 
    /// For example, for Bits, the ID is the Bits tier level, but for World of Warcraft, it could be Alliance or Horde.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// A URL to the small version (18px x 18px) of the badge.
    /// </summary>
    public required string ImageUrl_1x { get; init; }
    /// <summary>
    /// A URL to the medium version (36px x 36px) of the badge.
    /// </summary>
    public required string ImageUrl_2x { get; init; }
    /// <summary>
    /// A URL to the large version (72px x 72px) of the badge.
    /// </summary>
    public required string ImageUrl_4x { get; init; }
    /// <summary>
    /// The title of the badge.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The description of the badge.
    /// </summary>
    public required string Description { get; init; }
    /// <summary>
    /// The action to take when clicking on the badge. 
    /// Set to <see langword="null"/> if no action is specified.
    /// </summary>
    public string? ClickAction { get; init; }
    /// <summary>
    /// The URL to navigate to when clicking on the badge. 
    /// Set to <see langword="null"/> if no URL is specified.
    /// </summary>
    public string? ClickUrl { get; init; }
}
