using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

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
    /// <param name="fromUserId">
    /// The id of the user sending the whisper.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="toUserId">The id of the user receiving the whisper.</param>
    /// <param name="whisper">The whisper to send.</param>
    public SendWhisperRequest(
        string clientId,
        string accessToken,
        string fromUserId,
        string toUserId,
        SendWhisperRequestData whisper
        ) : base(
            "/whispers",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("from_user_id", fromUserId)
                .Add("to_user_id", toUserId)
            )
    {
        Method = HttpMethod.Post;
        ContentObject = whisper;
    }
}

/// <summary>
/// Contains information used to send a whisper message.
/// </summary>
public record SendWhisperRequestData
{
    /// <summary>
    /// The message to send.
    /// This cannot be an empty string.
    /// The message can be up to 10,000 characters if the to user has whispered the from user before, otherwise, the message can only be 500 characters long.
    /// Messages that exceed the maximum length are truncated.
    /// </summary>
    public required string Message { get; set; }
}
