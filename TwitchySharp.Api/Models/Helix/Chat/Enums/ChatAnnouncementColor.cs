using System.Text.Json.Serialization;
using TwitchySharp.Api.Models.Helix.Chat.Requests;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Chat.Enums;

/// <summary>
/// Contains static definitions for possible announcement colors.
/// See <see cref="SendChatAnnouncementRequest"/>.
/// </summary>
/// <param name="Value">The string value of the chat announcement color.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ChatAnnouncementColor, string>))]
public record ChatAnnouncementColor(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// Uses channel's accent color.
    /// </summary>
    public static ChatAnnouncementColor Primary { get; } = new("primary");
    public static ChatAnnouncementColor Blue { get; } = new("blue");
    public static ChatAnnouncementColor Green { get; } = new("green");
    public static ChatAnnouncementColor Orange { get; } = new("orange");
    public static ChatAnnouncementColor Purple { get; } = new("purple");
}
