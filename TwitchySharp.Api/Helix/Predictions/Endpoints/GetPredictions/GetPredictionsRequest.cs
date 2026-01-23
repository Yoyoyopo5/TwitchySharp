using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Predictions;
/// <summary>
/// Gets a list of Channel Points Predictions that the broadcaster created.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelReadPredictions"/> or <see cref="Scope.ChannelManagePredictions"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-predictions">Get Predictions</see> for more information.
/// </remarks>
public record GetPredictionsRequest
    : TwitchHelixRequest<GetPredictionsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelReadPredictions"/> or <see cref="Scope.ChannelManagePredictions"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetPredictionsRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetPredictionsRequestParameters parameters
        ) : base(
            "/predictions",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("id", parameters.PredictionIds?.Select(x => x.Value))
                .Add("first", parameters.First?.ToString())
                .Add("after", parameters.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetPredictionsRequest"/>.
/// </summary>
public record GetPredictionsRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get predictions for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// Filter the returned list by prediction id.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 25 ids. 
    /// The endpoint ignores duplicate ids and those not owned by the broadcaster.
    /// </remarks>
    public IEnumerable<PredictionId>? PredictionIds { get; set; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>.
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 25 items per page. 
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; set; }
    public PaginationCursor? After { get; set; }
}
