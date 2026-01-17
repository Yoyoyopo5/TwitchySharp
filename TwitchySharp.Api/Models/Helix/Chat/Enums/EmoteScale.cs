using System;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Models.Helix.Chat.Converters;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Chat.Enums;

/// <summary>
/// Contains static definitions for possible emote scales.
/// </summary>
/// <param name="Value">The string value of the emote scale.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<EmoteScale, string>))]
public record EmoteScale(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// 28px x 28px
    /// </summary>
    public static EmoteScale Small { get; } = new("1.0");
    /// <summary>
    /// 56px x 56px
    /// </summary>
    public static EmoteScale Medium { get; } = new("2.0");
    /// <summary>
    /// 112px x 112px
    /// </summary>
    public static EmoteScale Large { get; } = new("3.0");
}