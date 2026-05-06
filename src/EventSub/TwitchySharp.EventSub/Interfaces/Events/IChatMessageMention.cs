namespace TwitchySharp.EventSub.Interfaces.Events;

/// <summary>
/// A mention in a Twitch message.
/// </summary>
public interface IChatMessageMention
{
    /// <summary>
    /// The id of the user that was mentioned.
    /// </summary>
    string UserId { get; }
    /// <summary>
    /// The display name of the user that was mentioned.
    /// </summary>
    string UserName { get; }
    /// <summary>
    /// The login (username) of the user that was mentioned.
    /// </summary>
    string UserLogin { get; }
}
