using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Enums.Events.Channel.Goals;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.Goals;

/// <summary>
/// The interface for Channel Goal events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.GoalBegin"/>,
/// <see cref="EventSubSubscriptionType.GoalProgress"/>,
/// <see cref="EventSubSubscriptionType.GoalEnd"/>.
/// </remarks>
public interface IGoalEvent : IHaveBroadcaster
{
    /// <summary>
    /// The id of the event.
    /// </summary>
    string Id { get; }
    /// <summary>
    /// The type of goal.
    /// </summary>
    ChannelGoalType Type { get; }
    /// <summary>
    /// The description of the goal, if specified by the broadcaster.
    /// This can be a maximum of 40 characters.
    /// </summary>
    string Description { get; } // May be nullable.
    /// <summary>
    /// The current value of the goal.
    /// The exact meaning of this integer is determined by <see cref="Type"/>.
    /// </summary>
    int CurrentAmount { get; }
    /// <summary>
    /// The target value of the goal.
    /// </summary>
    int TargetAmount { get; }
    /// <summary>
    /// The date and time when the goal was created.
    /// </summary>
    DateTimeOffset StartedAt { get; }
}
