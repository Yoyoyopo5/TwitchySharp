using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix;
using TwitchySharp.Api.Models.Helix.Channels.Ads.Responses;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.Api.Models.Helix.Channels.Ads.Requests;
/// <summary>
/// Starts a commercial on the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelEditCommercial"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#start-commercial">Start Commerical</see> for more information.
/// </remarks>
public record StartCommericalRequest
    : TwitchHelixRequest<StartCommericalResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelEditCommercial"/></param>
    /// <param name="broadcasterId">The user ID of the partner or affiliate broadcaster that wants to run the commercial. This ID must match the user ID of the access token.</param>
    /// <param name="length">The length of the commercial to run. Twitch tries to serve a commercial that’s the requested length, but it may be shorter or longer. The maximum length you should request is 180 seconds.</param>
    public StartCommericalRequest(
        string clientId, 
        string accessToken, 
        string broadcasterId, 
        TimeSpan length
        )
        : base(
            "/channels/commercial",
            clientId,
            accessToken
            )
    {
        Method = HttpMethod.Post;
        ContentObject = new StartCommericalRequestData(broadcasterId, length);
    }
}

/// <summary>
/// See <see cref="StartCommericalRequest"/> for usage.
/// </summary>
/// <param name="BroadcasterId">The user ID of the partner or affiliate broadcaster that wants to run the commercial. This ID must match the user ID of the access token.</param>
/// <param name="Length">The length of the commercial to run. Twitch tries to serve a commercial that’s the requested length, but it may be shorter or longer. The maximum length you should request is 180 seconds.</param>
internal record StartCommericalRequestData(string BroadcasterId, TimeSpan Length)
{
    public string BroadcasterId { get; set; } = BroadcasterId;
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public TimeSpan Length { get; set; } = Length;
}
