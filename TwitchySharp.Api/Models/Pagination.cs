using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api;
/// <summary>
/// Contains information used to page through a list of results. 
/// The <see cref="Cursor"/> is <see langword="null"/> if there are no more pages left to page through.
/// See <see href="https://dev.twitch.tv/docs/api/guide/#pagination">pagination</see> for more information.
/// </summary>
public record Pagination
{
    /// <summary>
    /// The cursor used to get the next page of results. Usage depends on request type.
    /// </summary>
    public Cursor? Cursor { get; init; }
}

/// <summary>
/// A cursor used for pagination.
/// </summary>
/// <param name="Value">The cursor's string value.</param>
[JsonConverter(typeof(WrapperJsonConverter<Cursor, string>))]
public readonly record struct Cursor(string Value) : IWrapValue<string>;
