﻿using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Contains details about an extension digital product’s cost.
/// </summary>
public record ExtensionProductCost
{
    /// <summary>
    /// The amount exchanged for the digital product.
    /// Essentially, this is the amount of bits used.
    /// </summary>
    public required int Amount { get; init; }
    /// <summary>
    /// The type of currency exchanged.
    /// As of now, this can only be bits.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required ExtensionProductCostType Type { get; init; }
}
