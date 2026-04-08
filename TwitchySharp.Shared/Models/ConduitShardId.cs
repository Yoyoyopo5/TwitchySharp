using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch conduit shard.
/// </summary>
/// <param name="Value">The string value of the id.</param>
public readonly partial record struct ConduitShardId(string Value) : IWrapValue<string>;