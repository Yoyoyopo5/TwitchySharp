using TwitchySharp.Api.Models.Helix.Streams.Models;
using TwitchySharp.Api.Models.Shared;

namespace TwitchySharp.Api.Models.Helix.Streams.Responses;
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
