using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Streams;
/// <inheritdoc cref="ChannelStreamKey"/>
public record GetStreamKeyResponse
{
    /// <summary>
    /// A list containing a single object with the stream key.
    /// </summary>
    public required ChannelStreamKey[] Data { get; init; }
}

/// <summary>
/// Contains a broadcaster's stream key.
/// </summary>
public record ChannelStreamKey
{
    /// <summary>
    /// The broadcaster's stream key.
    /// </summary>
    public required string StreamKey { get; init; }
}
