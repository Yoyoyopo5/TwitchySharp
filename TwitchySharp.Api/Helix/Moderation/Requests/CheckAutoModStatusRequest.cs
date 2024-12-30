using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Checks whether AutoMod would flag the specified message for review.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#check-automod-status">check AutoMod status</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModerationRead"/>.
/// <br/>
/// <b>Rate Limits:</b> Rates are limited <b>per channel</b> based on the account type rather than per access token:
/// <list type="table">
///     <item>
///         <term>Normal</term>
///         <description>Max 5 per minute | 50 per hour</description>
///     </item>
///     <item>
///         <term>Affiliate</term>
///         <description>Max 10 per minute | 100 per hour</description>
///     </item>
///     <item>
///         <term>Partner</term>
///         <description>Max 30 per minute | 300 per hour</description>
///     </item>
/// </list>
/// <br/>
/// The above limits are in <b>addition to</b> the standard <see href="https://dev.twitch.tv/docs/api/guide#twitch-rate-limits">Twitch API rate limits</see>. 
/// The rate limit headers in the response represent the Twitch rate limits and not the above limits.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModerationRead"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster whose AutoMod settings and list of blocked terms are used to check the message. 
/// This must be the same user who created the <paramref name="accessToken"/>.
/// </param>
/// <param name="data">
/// The messages to check against the channel's AutoMod.
/// </param>
public class CheckAutoModStatusRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    CheckAutoModStatusRequestData data
    ) 
    : HelixApiRequest<CheckAutoModStatusResponse, CheckAutoModStatusRequestData>(
        "/moderation/enforcements/status" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId),
        clientId,
        accessToken,
        data
        );

/// <summary>
/// Contains a list of messages to check against a channel's AutoMod.
/// </summary>
public record CheckAutoModStatusRequestData
{
    /// <summary>
    /// The list of messages to check against a channel's AutoMod.
    /// The list must contain at least one message and may contain up to a maximum of 100 messages.
    /// </summary>
    public required AutoModStatusMessage[] Data { get; set; }
}

/// <summary>
/// A message to be checked against a channel's AutoMod.
/// </summary>
public record AutoModStatusMessage
{
    /// <summary>
    /// A caller-defined ID used to correlate this message with the same message in the response.
    /// The value of this property will be the same as <see cref="AutoModStatus.MessageId"/> in the response.
    /// </summary>
    [JsonPropertyName("msg_id")]
    public required string MessageId { get; set; }
    /// <summary>
    /// The message to check against the AutoMod.
    /// </summary>
    [JsonPropertyName("msg_text")]
    public required string MessageText { get; set; }
}
