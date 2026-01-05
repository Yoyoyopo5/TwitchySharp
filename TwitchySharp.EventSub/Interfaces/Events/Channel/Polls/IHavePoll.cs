using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models.Events.Channel.Polls;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.Polls;

/// <summary>
/// A chat poll.
/// </summary>
public interface IHavePoll
{
    /// <summary>
    /// The id of the poll.
    /// </summary>
    string Id { get; }
    /// <summary>
    /// The title of the poll.
    /// </summary>
    string Title { get; }
    /// <summary>
    /// The choices for the poll, including their vote count.
    /// </summary>
    ChannelPollChoice[] Choices { get; }
    /// <summary>
    /// The setting for Channel Points voting.
    /// </summary>
    ChannelPollChannelPointsVotingSetting ChannelPointsVoting { get; }
    /// <summary>
    /// The date and time the poll began.
    /// </summary>
    DateTimeOffset StartedAt { get; }
}
