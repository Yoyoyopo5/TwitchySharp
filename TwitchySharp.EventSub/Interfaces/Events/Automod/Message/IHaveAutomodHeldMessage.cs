using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models.Events.Automod.Message;

namespace TwitchySharp.EventSub.Interfaces.Events.Automod.Message;

/// <summary>
/// An event that has a message that was held by the Automod.
/// </summary>
public interface IHaveAutomodHeldMessage
{
    /// <summary>
    /// The id of the message that was flagged by the Automod.
    /// </summary>
    string MessageId { get; }
    /// <summary>
    /// The message that was flagged.
    /// </summary>
    AutomodCaughtChatMessage Message { get; }
    /// <summary>
    /// The date and time when the Automod caught the message.
    /// </summary>
    DateTimeOffset HeldAt { get; }
}
