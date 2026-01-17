using TwitchySharp.Api.Models.Helix.Streams.Models;
using TwitchySharp.Api.Models.Shared;

namespace TwitchySharp.Api.Models.Helix.Streams.Responses;
/// <summary>
/// Contains a list of found active streams.
/// </summary>
public record GetStreamsResponse
{
    /// <summary>
    /// The active streams that matched the request query.
    /// </summary>
    public required TwitchStream[] Data { get; init; }
    ///<inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}
