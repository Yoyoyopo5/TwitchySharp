using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Games;
public record GetTopGamesResponse
{
    /// <summary>
    /// The list of top games.
    /// </summary>
    public required Game[] Data { get; init; }
    /// <summary>
    /// Contains a cursor used to page through the list of results. 
    /// The <see cref="Pagination.Cursor"/> is <see langword="null"/> if there are no more pages left to page through.
    /// </summary>
    public required Pagination Pagination { get; init; }
}

/// <summary>
/// Contains data about a specific game (category) on Twitch.
/// </summary>
public record Game
{
    /// <summary>
    /// An id that identifies the category or game.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The category or game's name.
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// A url to the category or game's box art.
    /// </summary>
    public required GameBoxArtUrlTemplate BoxArtUrl { get; init; }
    /// <summary>
    /// The ID that <see href="https://www.igdb.com/">IGDB</see> uses to identify this game. 
    /// If the IGDB ID is not available to Twitch, this is an empty string.
    /// </summary>
    public required string IgdbId { get; init; }
}

/// <summary>
/// Helper class to create valid urls to game (category) cover images.
/// </summary>
/// <param name="TemplateUrl"></param>
[JsonConverter(typeof(GameBoxArtUrlTemplateJsonConverter))]
public record GameBoxArtUrlTemplate(string TemplateUrl)
{
    /// <summary>
    /// Creates a valid url to a game's box art.
    /// </summary>
    /// <param name="width">The width of the image to get, in pixels.</param>
    /// <param name="height">The height of the image to get, in pixels.</param>
    /// <returns>A url to an image of the specified size.</returns>
    public string MakeUrl(uint width, uint height)
        => TemplateUrl
            .Replace("{width}", width.ToString())
            .Replace("{height}", height.ToString());
}

internal class GameBoxArtUrlTemplateJsonConverter : JsonConverter<GameBoxArtUrlTemplate>
{
    public override GameBoxArtUrlTemplate? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.GetString() switch
        {
            null => null,
            string value => new GameBoxArtUrlTemplate(value)
        };

    public override void Write(Utf8JsonWriter writer, GameBoxArtUrlTemplate value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.TemplateUrl);
}
