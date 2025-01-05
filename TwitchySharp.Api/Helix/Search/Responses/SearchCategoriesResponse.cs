using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Search;
/// <summary>
/// Contains a list of found categories.
/// </summary>
public record SearchCategoriesResponse
{
    /// <summary>
    /// The list of categories.
    /// </summary>
    public required TwitchCategory[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}

/// <summary>
/// Contains information about a specific Twitch category (game).
/// </summary>
public record TwitchCategory
{
    /// <summary>
    /// A URL to an image of the game’s box art or streaming category.
    /// </summary>
    public required string BoxArtUrl { get; init; }
    /// <summary>
    /// The name of the game or category.
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// The id of the category.
    /// </summary>
    public required string Id { get; init; }
}
