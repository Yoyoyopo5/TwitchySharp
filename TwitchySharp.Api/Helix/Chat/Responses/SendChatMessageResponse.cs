using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains a list of sent messages.
/// </summary>
public record SendChatMessageResponse
{
    /// <summary>
    /// Contains a single entry for the sent message.
    /// </summary>
    public required SentMessageInfo[] Data { get; init; }
}

/// <summary>
/// Contains information about a message that was sent through the API.
/// </summary>
public record SentMessageInfo
{
    /// <summary>
    /// The message id of the message that was sent.
    /// </summary>
    public required string MessageId { get; init; }
    /// <summary>
    /// If the message passed all checks and was sent.
    /// </summary>
    public required bool IsSent { get; init; }
    /// <summary>
    /// The reason the message was dropped, if any.
    /// </summary>
    public MessageDropReason? DropReason { get; init; }
}

/// <summary>
/// Contains information about why a chat message was not sent successfully.
/// </summary>
public record MessageDropReason
{
    /// <summary>
    /// Code for why the message was dropped.
    /// </summary>
    public required string Code { get; init; }
    /// <summary>
    /// Message for why the message was dropped.
    /// </summary>
    public required string Message { get; init; }
}
