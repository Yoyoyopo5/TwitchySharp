using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets the broadcaster’s list of non-private, blocked words or phrases. 
/// These are the terms that the broadcaster or moderator added manually or that were denied by AutoMod.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-blocked-terms">get blocked terms</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadBlockedTerms"/> or <see cref="Scope.ModeratorManageBlockedTerms"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorReadBlockedTerms"/> or <see cref="Scope.ModeratorManageBlockedTerms"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster (channel) to get blocked terms for.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator of the broadcaster's channel.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="first">
/// The maximum number of items to return per page in the response. 
/// The minimum page size is 1 item per page and the maximum is 100 items per page. 
/// The default is 20.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> property in the response contains the cursor’s value.
/// </param>
public class GetBlockedTermsRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string moderatorId,
    int? first = null,
    string? after = null
    )
    : HelixApiRequest<GetBlockedTermsResponse>(
        "/moderation/blocked_terms" + 
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId)
            .Add("first", first?.ToString())
            .Add("after", after),
        clientId,
        accessToken
        );
