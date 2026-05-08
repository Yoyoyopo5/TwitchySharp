using System.Net.Http;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Retrieves the active shared chat session for a channel.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference#get-shared-chat-session">Get Shared Chat Session</see> for more information.
/// </remarks>
public record GetSharedChatSessionRequest
    : TwitchHelixRequest<GetSharedChatSessionResponse>
{
    protected override string Path => "/shared_chat/session";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);

    /// <summary>
    /// The user id of the broadcaster whose shared chat you want to get.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
}
