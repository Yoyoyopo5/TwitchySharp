using System;
using System.Collections.Generic;
using System.Text;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Gets the broadcaster’s list editors.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-editors">get channel editors</see> for more information.
/// <br/>
/// Requires a user access token with <see cref="Scope.ChannelReadEditors"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token with <see cref="Scope.ChannelReadEditors"/>.</param>
/// <param name="broadcasterId">
/// The user ID of the broadcaster that owns the channel. 
/// This ID must match the user ID in the <paramref name="accessToken"/>.
/// </param>
public class GetChannelEditorsRequest(string clientId, string accessToken, string broadcasterId)
    : HelixApiRequest<GetChannelEditorsResponse>(
        "/channels/editors" + 
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId), 
        clientId, 
        accessToken
        );
