using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Contains a list of currently live broadcasters that are using a specific extension.
/// </summary>
public record GetExtensionLiveChannelsResponse
{
    /// <summary>
    /// A list of broadcasters that are streaming live and have installed or activated the extension.
    /// </summary>
    public required ExtensionLiveChannel[] Data { get; init; }
    /// <summary>
    /// The cursor used to get the next page of results. Use the <see cref="Pagination.Cursor"/> property to set the request’s after parameter.
    /// </summary>
    public required Pagination Pagination { get; init; }
}

/// <summary>
/// Contains information about a broadcaster that has installed or activated a specific extension.
/// </summary>
public record ExtensionLiveChannel
{
    /// <summary>
    /// The user id of the broadcaster that is using the extension.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The broadcaster's display name.
    /// </summary>
    public required string BroadcasterName { get; init; }
    /// <summary>
    /// The name of the category being streamed.
    /// </summary>
    public required string GameName { get; init; }
    /// <summary>
    /// The game id of the category being streamed.
    /// </summary>
    public required string GameId { get; init; }
    /// <summary>
    /// The title of the broadcaster's livestream. This may be an empty string.
    /// </summary>
    public required string Title { get; init; }
}
