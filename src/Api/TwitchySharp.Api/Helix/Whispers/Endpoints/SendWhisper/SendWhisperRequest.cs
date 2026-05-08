using System.Collections.Immutable;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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
    protected override string Path => "/whispers";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(FromUserId),
        ValidScopes = ImmutableHashSet.Create(Scope.UserManageWhispers)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("from_user_id", FromUserId)
            .Add("to_user_id", ToUserId);
    public override object? ContentObject => Whisper;

    /// <summary>
    /// The id of the user sending the whisper.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId FromUserId { get; init; }
    /// <summary>
    /// The id of the user receiving the whisper.
    /// </summary>
    public required UserId ToUserId { get; init; }
    /// <summary>
    /// The whisper content to send.
    /// </summary>
    public required SendWhisperRequestData Whisper { get; init; }

    protected override ValueTask<SendWhisperResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
        => ValueTask.FromResult(new SendWhisperResponse());
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
    public required string Message { get; init; }
}
