using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch conduit shard.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<ConduitShardId, string>))]
public readonly record struct ConduitShardId(string Value) : IWrapValue<string>
{
    public static implicit operator string(ConduitShardId id)
        => id.Value;
    public override string ToString()
        => Value;
}