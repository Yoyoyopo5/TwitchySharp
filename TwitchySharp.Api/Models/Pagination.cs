using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Models;
/// <summary>
/// Contains information used to page through a list of results. 
/// The object is empty if there are no more pages left to page through.
/// See <see href="https://dev.twitch.tv/docs/api/guide/#pagination">pagination</see> for more information.
/// </summary>
public record Pagination
{
    /// <summary>
    /// The cursor used to get the next page of results. Usage depends on request type.
    /// </summary>
    [JsonInclude]
    public string? Cursor { get; private set; }
}
