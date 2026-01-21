using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Gets the broadcaster’s list editors.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.ChannelReadEditors"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-editors">Get Channel Editors</see> for more information.
/// </remarks>
public record GetChannelEditorsRequest
    : TwitchHelixRequest<GetChannelEditorsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token with <see cref="Scope.ChannelReadEditors"/>.</param>
    /// <param name="broadcasterId">
    /// The user ID of the broadcaster that owns the channel. 
    /// This ID must match the user ID in the <paramref name="accessToken"/>.
    /// </param>
    public GetChannelEditorsRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetChannelEditorsRequestParameters parameters
        )
        : base(
            "/channels/editors",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetChannelEditorsRequest"/>.
/// </summary>
public record GetChannelEditorsRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster that owns the channel. 
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
}
