namespace TwitchySharp.EventSub.Interfaces.Events.Channel.Shoutout;

/// <summary>
/// A shoutout event.
/// </summary>
public interface IHaveShoutout
{
    /// <summary>
    /// The number of viewers that were watching the sending broadcaster's stream at the time of the shoutout.
    /// </summary>
    int ViewerCount { get; }
    /// <summary>
    /// The date and time when the shoutout was sent.
    /// </summary>
    DateTimeOffset StartedAt { get; }
}
