using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Whispers;
/// <summary>
/// Sends a whisper message to the specified user.
/// </summary>
/// <remarks>
/// <para>
/// <b>Note:</b> The user sending the whisper must have a verified phone number.
/// </para>
/// <para>
/// <b>Note:</b> The API may silently drop whispers that it suspects of violating Twitch policies. 
/// (The API does not indicate that it dropped the whisper; it returns a 204 status code as if it succeeded.)
/// </para>
/// <para>
/// <b>Rate Limits:</b> You may whisper to a maximum of 40 unique recipients per day. 
/// Within the per day limit, you may whisper a maximum of 3 whispers per second and a maximum of 100 whispers per minute.
/// </para>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.UserManageWhispers"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-whisper">Send Whisper</see> for more information.
/// </remarks>
public record SendWhisperRequest
    : TwitchHelixRequest<SendWhisperResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.UserManageWhispers"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    /// <param name="whisper">The whisper to send.</param>
    public SendWhisperRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        SendWhisperRequestParameters parameters,
        SendWhisperRequestData whisper
        ) : base(
            "/whispers",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("from_user_id", parameters.FromUserId)
                .Add("to_user_id", parameters.ToUserId)
            )
    {
        Method = HttpMethod.Post;
        ContentObject = whisper;
    }
}

/// <summary>
/// Request parameters for a <see cref="SendWhisperRequest"/>.
/// </summary>
public record SendWhisperRequestParameters
{
    /// <summary>
    /// The id of the user sending the whisper.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId FromUserId { get; set; }
    /// <summary>
    /// The id of the user receiving the whisper.
    /// </summary>
    public required UserId ToUserId { get; set; }
}

/// <summary>
/// Contains information used to send a whisper message.
/// </summary>
public record SendWhisperRequestData
{
    /// <summary>
    /// The message to send.
    /// </summary>
    /// <remarks>
    /// This cannot be an empty string.
    /// The message can be up to 10,000 characters if the to user has whispered the from user before, otherwise, the message can only be 500 characters long.
    /// Messages that exceed the maximum length are truncated.
    /// </remarks>
    public required string Message { get; set; }
}
