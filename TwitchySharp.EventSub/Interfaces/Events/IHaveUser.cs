namespace TwitchySharp.EventSub.Interfaces.Events;

/// <summary>
/// An event associated with a specific user. 
/// </summary>
public interface IHaveUser
{
    /// <summary>
    /// The user id of the user associated with the event.
    /// </summary>
    string UserId { get; }
    /// <summary>
    /// The login (username) of the user associated with the event.
    /// </summary>
    string UserLogin { get; }
    /// <summary>
    /// The display name of the user associated with the event.
    /// </summary>
    string UserName { get; }
}
