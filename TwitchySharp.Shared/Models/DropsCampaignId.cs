using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch Drops campaign.
/// </summary>
/// <param name="Value">The string value of the id</param>
[JsonConverter(typeof(WrapperJsonConverter<DropsCampaignId, string>))]
public readonly record struct DropsCampaignId(string Value) : IWrapValue<string>
{
    public static implicit operator string(DropsCampaignId id)
        => id.Value;
    public override string ToString()
        => Value;
}