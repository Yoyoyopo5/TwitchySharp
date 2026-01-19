using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.GuestStar;

/// <summary>
/// Contains static definitions for possible guest star invite statuses.
/// </summary>
/// <param name="Value">The string value of the guest star invite status.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<GuestStarInviteStatus, string>))]
public record GuestStarInviteStatus(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// The user has been invited to the session but has not acknowledged it.
    /// </summary>
    public static GuestStarInviteStatus Invited { get; } = new("invited");

    /// <summary>
    /// The invited user has acknowledged the invite and joined the waiting room, but may still be setting up their media devices or otherwise preparing to join the call.
    /// </summary>
    public static GuestStarInviteStatus Accepted { get; } = new("accepted");

    /// <summary>
    /// The invited user has signaled they are ready to join the call from the waiting room.
    /// </summary>
    public static GuestStarInviteStatus Ready { get; } = new("ready");
}
