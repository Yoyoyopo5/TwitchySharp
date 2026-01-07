using TwitchySharp.EventSub.Enums.Events.Automod.Message;

namespace TwitchySharp.EventSub.Interfaces.Events.Automod.Message;

/// <summary>
/// A status update for an Automod held message.
/// </summary>
public interface IHaveAutomodHeldMessageStatusUpdate
{
    /// <summary>
    /// The status of the updated automod message.
    /// </summary>
    AutomodMessageUpdateStatus Status { get; }
}
