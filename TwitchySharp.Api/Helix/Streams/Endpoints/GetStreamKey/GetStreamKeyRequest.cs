using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Gets the channel’s stream key.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadStreamKey"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-stream-key">Get Stream Key</see> for more information.
/// </remarks>
public record GetStreamKeyRequest
    : TwitchHelixRequest<GetStreamKeyResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelReadStreamKey"/>.</param>
    /// <param name="broadcasterId">
    /// The user id of the broadcaster (channel) to get the stream key for.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    public GetStreamKeyRequest(
        string clientId,
        string accessToken,
        string broadcasterId
        ) : base(
            "/streams/key",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
            )
    {
        Method = HttpMethod.Get;
    }
}
