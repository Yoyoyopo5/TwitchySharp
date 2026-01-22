using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.HypeTrain;
/// <summary>
/// Get the status of a Hype Train for the specified broadcaster.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadHypeTrain"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-hype-train-status">Get Hype Train Status</see> for more information.
/// </remarks>
public record GetHypeTrainStatusRequest
    : TwitchHelixRequest<GetHypeTrainStatusResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">
    /// A user access token for that includes <see cref="Scope.ChannelReadHypeTrain"/>.
    /// </param>
    /// <param name="parameters">The request parameters.</param>
    public GetHypeTrainStatusRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetHypeTrainStatusRequestParameters parameters
        )
        : base(
            "/hypetrain/status",
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
/// Request parameters for a <see cref="GetHypeTrainStatusRequest"/>.
/// </summary>
public record GetHypeTrainStatusRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get the Hype Train status for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
}
