using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Gets the channel’s stream key.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-stream-key"/> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadStreamKey"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelReadStreamKey"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster (channel) to get the stream key for.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
public class GetStreamKeyRequest(
    string clientId,
    string accessToken,
    string broadcasterId
    )
    : HelixApiRequest<GetStreamKeyResponse>(
        "/streams/key" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId),
        clientId,
        accessToken
        );
