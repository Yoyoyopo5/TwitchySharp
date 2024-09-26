using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TwitchySharp.Api.Helix.Channels.Ads.Responses;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Channels.Ads.Requests;
/// <summary>
/// Starts a commercial on the specified channel.
/// Only partners and affiliates may run commercials and they must be streaming live at the time.
/// Only the broadcaster may start a commercial; the broadcaster’s editors and moderators may not start commercials on behalf of the broadcaster.
/// Requires a user access token that includes <see cref="Scope.ChannelEditCommercial"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelEditCommercial"/></param>
/// <param name="broadcasterId">The user ID of the partner or affiliate broadcaster that wants to run the commercial. This ID must match the user ID of the access token.</param>
/// <param name="length">The length of the commercial to run, in seconds. Twitch tries to serve a commercial that’s the requested length, but it may be shorter or longer. The maximum length you should request is 180 seconds.</param>
public class StartCommericalRequest(string clientId, string accessToken, string broadcasterId, int length)
    : HelixChannelRequest<StartCommericalResponse, StartCommericalRequestData>(
        "/commercial",
        clientId,
        accessToken,
        new StartCommericalRequestData(broadcasterId, length)
        );

/// <summary>
/// See <see cref="StartCommericalRequest"/> for usage.
/// </summary>
/// <param name="BroadcasterId">The user ID of the partner or affiliate broadcaster that wants to run the commercial. This ID must match the user ID of the access token.</param>
/// <param name="Length">The length of the commercial to run, in seconds. Twitch tries to serve a commercial that’s the requested length, but it may be shorter or longer. The maximum length you should request is 180 seconds.</param>
public record StartCommericalRequestData(string BroadcasterId, int Length)
{
    public string BroadcasterId { get; set; } = BroadcasterId;
    public int Length { get; set; } = Length;
}
