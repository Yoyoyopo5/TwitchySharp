using System;

namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Contains information about a specific chat badge.
/// </summary>
public record ChatBadge
{
    /// <summary>
    /// An id that identifies this version of the badge. 
    /// </summary>
    /// <remarks>
    /// The id can be any value. 
    /// For example, for Bits, the id is the Bits tier level, but for World of Warcraft, it could be Alliance or Horde.
    /// </remarks>
    public required ChatBadgeId Id { get; init; }
    /// <summary>
    /// A URL to the small version (18px x 18px) of the badge.
    /// </summary>
    public required Uri ImageUrl_1x { get; init; }
    /// <summary>
    /// A URL to the medium version (36px x 36px) of the badge.
    /// </summary>
    public required Uri ImageUrl_2x { get; init; }
    /// <summary>
    /// A URL to the large version (72px x 72px) of the badge.
    /// </summary>
    public required Uri ImageUrl_4x { get; init; }
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
    public Uri? ClickUrl { get; init; }
}
