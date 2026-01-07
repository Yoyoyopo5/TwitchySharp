namespace TwitchySharp.EventSub.Interfaces.Events;
/// <summary>
/// An event associated with a specific broadcaster.
/// </summary>
public interface IHaveBroadcaster
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the event is for.
    /// </summary>
    string BroadcasterUserId { get; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the event is for.
    /// </summary>
    string BroadcasterUserLogin { get; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the event is for.
    /// </summary>
    string BroadcasterUserName { get; }
}
