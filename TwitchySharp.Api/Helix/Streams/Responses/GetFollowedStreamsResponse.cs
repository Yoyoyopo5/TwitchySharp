using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Contains a list of active streams that a specific user follows.
/// </summary>
public record GetFollowedStreamsResponse
{
    /// <summary>
    /// The list of active followed streams.
    /// </summary>
    public required TwitchStream[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}
