using TwitchySharp.EventSub.Enums.Events.Channel.HypeTrain;
using TwitchySharp.EventSub.Models.Events.Channel.HypeTrain;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.HypeTrain;

/// <summary>
/// A chat Hype Train.
/// </summary>
public interface IHaveHypeTrain
{
    /// <summary>
    /// The id of the Hype Train.
    /// </summary>
    string Id { get; }
    /// <summary>
    /// The total number of points contributed to the Hype Train.
    /// </summary>
    int Total { get; }
    /// <summary>
    /// The current level of the Hype Train.
    /// </summary>
    int Level { get; }
    /// <summary>
    /// The list of broadcasters participating in the Hype Train, if it occurred in a shared chat.
    /// This is <see langword="null"/> if the Hype Train is not in a shared chat.
    /// </summary>
    SharedHypeTrainParticipant[]? SharedTrainParticipants { get; }
    /// <summary>
    /// The date and time when the Hype Train began.
    /// </summary>
    DateTimeOffset StartedAt { get; }
    /// <summary>
    /// The type of Hype Train.
    /// </summary>
    HypeTrainType Type { get; }
    /// <summary>
    /// Indicates whether the Hype Train is in a shared chat.
    /// </summary>
    bool IsSharedTrain { get; }
}
