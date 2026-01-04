using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Interfaces;

namespace TwitchySharp.EventSub.Models.Events.Channel.Chat;

/// <summary>
/// Contains information about a specific chat message.
/// </summary>
public record ChannelChatMessage : IChatMessage
{
    /// <summary>
    /// The text of the message.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The message fragments.
    /// </summary>
    public required ChannelChatMessageFragment[] Fragments { get; init; }
    IEnumerable<IChatMessageFragment> IChatMessage.Fragments => Fragments;
}
