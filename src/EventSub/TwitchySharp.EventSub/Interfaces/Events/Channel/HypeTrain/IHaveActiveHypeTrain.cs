namespace TwitchySharp.EventSub.Interfaces.Events.Channel.HypeTrain;

/// <summary>
/// An active chat Hype Train.
/// </summary>
public interface IHaveActiveHypeTrain : IHaveHypeTrain
{
    /// <summary>
    /// The number of points contributed to the Hype Train at its current level.
    /// </summary>
    int Progress { get; }
    /// <summary>
    /// The number of point required to reach the next level.
    /// </summary>
    int Goal { get; }
    /// <summary>
    /// The date and time when the Hype Train will expire.
    /// This is extended when the Hype Train reaches a new level.
    /// </summary>
    DateTimeOffset ExpiresAt { get; }
}
