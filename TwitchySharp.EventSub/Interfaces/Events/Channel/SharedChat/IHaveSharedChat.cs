using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models.Events.Channel.SharedChat;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.SharedChat;

/// <summary>
/// A shared chat session.
/// </summary>
public interface IHaveSharedChat
{
    /// <summary>
    /// The id of the shared chat session.
    /// </summary>
    string SessionId { get; }
    /// <summary>
    /// The user id of the broadcaster (channel) that is hosting the shared chat session. 
    /// </summary>
    string HostBroadcasterUserId { get; }
    /// <summary>
    /// The display name of the broadcaster (channel) that is hosting the shared chat session.
    /// </summary>
    string HostBroadcasterUserName { get; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that is hosting the shared chat session.
    /// </summary>
    string HostBroadcasterUserLogin { get; }
    /// <summary>
    /// The list of broadcasters participating in the shared chat session.
    /// </summary>
    SharedChatParticipant[] Participant { get; }
}
