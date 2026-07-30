using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// An id representing a specific Twitch conduit shard.
/// </summary>
/// <remarks>
/// This is a zero-based index of the shard per conduit.
/// </remarks>
/// <param name="Value">The string value of the id.</param>
[Wrapper<string>]
public readonly partial record struct ConduitShardId(string Value);
