using TwitchySharp.EventSub.Enums.Events.Automod.Message;
using TwitchySharp.EventSub.Models.Events.Automod.Message;

namespace TwitchySharp.EventSub.Interfaces.Events.Automod.Message;

/// <summary>
/// An Automod held message.
/// </summary>
public interface IHaveAutomodHeldMessage
{
    /// <summary>
    /// The message that was flagged.
    /// </summary>
    AutomodCaughtChatMessage Message { get; }
    /// <summary>
    /// The date and time when the Automod caught the message.
    /// </summary>
    DateTimeOffset HeldAt { get; }
    /// <summary>
    /// The category that the message was flagged under.
    /// </summary>
    AutomodMessageCategory Category { get; }
    /// <summary>
    /// The level of severity for the caught message.
    /// Ranges from 1 to 4.
    /// </summary>
    int Level { get; }
}
