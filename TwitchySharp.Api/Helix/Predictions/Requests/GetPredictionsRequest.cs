using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Predictions;
/// <summary>
/// Gets a list of Channel Points Predictions that the broadcaster created.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-predictions">get predictions</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadPredictions"/> or <see cref="Scope.ChannelManagePredictions"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelReadPredictions"/> or <see cref="Scope.ChannelManagePredictions"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster (channel) to get predictions for.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="predictionIds">
/// Filter the returned list by prediction id.
/// You may specify a maximum of 25 ids. 
/// The endpoint ignores duplicate ids and those not owned by the broadcaster.
/// </param>
/// <param name="first">
/// The maximum number of items to return per page in the response. 
/// The minimum page size is 1 item per page and the maximum is 25 items per page. 
/// The default is 20.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> object in the response contains the cursor’s value.
/// </param>
public class GetPredictionsRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    IEnumerable<string>? predictionIds = null,
    int? first = null,
    string? after = null
    )
    : HelixApiRequest<GetPredictionsResponse>(
        "/predictions" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("id", predictionIds)
            .Add("first", first?.ToString())
            .Add("after", after),
        clientId,
        accessToken
        );
