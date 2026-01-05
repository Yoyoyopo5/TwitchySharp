using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Channel.GuestStar;

/// <summary>
/// Contains static definitions for possible Guest Star guest states.
/// </summary>
/// <param name="Value">The string value of the state.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<GuestStarGuestState, string>))]
public record GuestStarGuestState(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// The guest has transitioned to the invite queue. 
    /// This can take place when the guest was previously assigned a slot, but have been removed from the call and are sent back to the invite queue.
    /// </summary>
    public static GuestStarGuestState Invited { get; } = new("invited");
    /// <summary>
    /// The guest has accepted the invite and is currently in the process of setting up to join the session.
    /// </summary>
    public static GuestStarGuestState Accepted { get; } = new("accepted");
    /// <summary>
    /// The guest has signaled they are ready and can be assigned a slot.
    /// </summary>
    public static GuestStarGuestState Ready { get; } = new("ready");
    /// <summary>
    /// The guest has been assigned a slot in the session, 
    /// but is not currently seen live in the broadcasting software.
    /// </summary>
    public static GuestStarGuestState Backstage { get; } = new("backstage");
    /// <summary>
    /// The guest is now live in the host's broadcasting software.
    /// </summary>
    public static GuestStarGuestState Live { get; } = new("live");
    /// <summary>
    /// The guest was removed from the call or queue.
    /// </summary>
    public static GuestStarGuestState Removed { get; } = new("removed");
}
