using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Ads;
/// <summary>
/// Starts a commercial on the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelEditCommercial"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#start-commercial">Start Commerical</see> for more information.
/// </remarks>
public record StartCommercialRequest
    : TwitchHelixRequest<StartCommercialResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelEditCommercial"/></param>
    /// <param name="data">The request data.</param>
    public StartCommercialRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        StartCommericalRequestData data
        )
        : base(
            "/channels/commercial",
            clientId,
            accessToken
            )
    {
        Method = HttpMethod.Post;
        ContentObject = data;
    }
}

/// <summary>
/// Request data for a <see cref="StartCommercialRequest"/>.
/// </summary>
public record StartCommericalRequestData
{
    /// <summary>
    /// The user id of the partner or affiliate broadcaster that wants to run the commercial. This ID must match the user ID of the access token.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The length of the commercial to run. 
    /// </summary>
    /// <remarks>
    /// Twitch tries to serve a commercial that’s the requested length, but it may be shorter or longer.
    /// The maximum length you should request is 180 seconds.
    /// </remarks>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan Length { get; set; }
}
