using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Helpers.JsonConverters.DateTime;

namespace TwitchySharp.EventSub.Notifications.Automod;
public record AutomodMessageHoldNotification : EventSubNotification<AutomodMessageHoldEvent, AutomodMessageHoldCondition>;
public record AutomodMessageHoldCondition
{
    public required string BroadcasterUserId { get; init; }
    public required string ModeratorUserId { get; init; }
}
public record AutomodMessageHoldEvent
{
    public required string BroadcasterUserId { get; init; }
    public required string BroadcasterUserLogin { get; init; }
    public required string BroadcasterUserName { get; init; }
    public required string UserId { get; init; }
    public required string UserLogin { get; init; }
    public required string UserName { get; init; }
    public required string MessageId { get; init; }
    public required AutomodCaughtChatMessage Message { get; init; }
    public required string Category { get; init; }
    public required int Level { get; init; }
    public required DateTimeOffset HeldAt { get; init; }
}

public record AutomodCaughtChatMessage
{
    public required string Text { get; init; }
    public required AutomodCaughtMessageFragment[] Fragments { get; init; }
}

public record AutomodCaughtMessageFragment
{
    public required AutomodCaughtMessageFragmentType Type { get; init; }
    public required string Text { get; init; }
    public ChatEmote? Emote { get; init; }
    public Cheermote? Cheermote { get; init; }
}

[JsonConverter(typeof(ValueBackedEnumJsonConverter<AutomodCaughtMessageFragmentType, string>))]
public record AutomodCaughtMessageFragmentType(string Value)
    : ValueBackedEnum<string>(Value)
{
    public static AutomodCaughtMessageFragmentType Text { get; } = new("text");
    public static AutomodCaughtMessageFragmentType Emote { get; } = new("emote");
    public static AutomodCaughtMessageFragmentType Cheermote { get; } = new("cheermote");
}

public record ChatEmote
{
    public required string Id { get; init; }
    public required string EmoteSetId { get; init; }
}

public record Cheermote
{
    public required string Prefix { get; init; }
    public required int Bits { get; init; }
    public required int Tier { get; init; }
}
