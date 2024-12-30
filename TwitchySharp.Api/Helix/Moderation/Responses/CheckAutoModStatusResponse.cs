using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of AutoMod responses for messages.
/// </summary>
public record CheckAutoModStatusResponse
{
    /// <summary>
    /// The list of messages and their AutoMod statuses.
    /// </summary>
    public required AutoModStatus[] Data { get; init; }
}

/// <summary>
/// The AutoMod status for a specific message (whether or not the channel's AutoMod would allow the message to appear in chat).
/// </summary>
public record AutoModStatus
{
    /// <summary>
    /// The caller-defined id that was passed in the request.
    /// </summary>
    [JsonPropertyName("msg_id")]
    public required string MessageId { get; init; }
    /// <summary>
    /// Indicates whether Twitch would approve the message for chat or hold it for moderator review or block it from chat. 
    /// Is <see langword="true"/> if Twitch would approve the message; otherwise, <see langword="false"/> if Twitch would hold the message for moderator review or block it from chat.
    /// </summary>
    public required bool IsPermitted { get; init; }
}
