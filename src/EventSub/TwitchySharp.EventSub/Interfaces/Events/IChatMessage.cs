namespace TwitchySharp.EventSub.Interfaces.Events;

/// <summary>
/// A Twitch chat message.
/// </summary>
public interface IChatMessage
{
    /// <summary>
    /// The full message content in string format.
    /// </summary>
    string Text { get; }
    /// <summary>
    /// The message fragments.
    /// </summary>
    IEnumerable<IChatMessageFragment> Fragments { get; }
}
