using System.Net.Http;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Checks whether AutoMod would flag the specified message for review.
/// </summary>
/// <remarks>
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
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModerationRead"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#check-automod-status">Check AutoMod Status</see> for more information.
/// </remarks>
public record CheckAutoModStatusRequest
    : TwitchHelixRequest<CheckAutoModStatusResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModerationRead"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    /// <param name="messages">
    /// The messages to check against the channel's AutoMod.
    /// </param>
    public CheckAutoModStatusRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        CheckAutoModStatusRequestParameters parameters,
        CheckAutoModStatusRequestData messages
        ) : base(
            "/moderation/enforcements/status",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
            )
    {
        Method = HttpMethod.Post;
        ContentObject = messages;
    }
}

/// <summary>
/// Request parameters for a <see cref="CheckAutoModStatusRequest"/>.
/// </summary>
public record CheckAutoModStatusRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster whose AutoMod settings and list of blocked terms are used to check the message.
    /// </summary>
    /// <remarks>
    /// This must be the same user who created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
}

/// <summary>
/// Contains a list of messages to check against a channel's AutoMod.
/// </summary>
public record CheckAutoModStatusRequestData
{
    /// <summary>
    /// The list of messages to check against a channel's AutoMod.
    /// The list must contain at least one message and may contain up to a maximum of 100 messages.
    /// </summary>
    [JsonPropertyName("data")]
    public required AutoModStatusMessage[] Messages { get; set; }
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
