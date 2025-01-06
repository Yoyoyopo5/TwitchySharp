using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Streams;
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
