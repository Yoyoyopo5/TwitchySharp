using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Removes the word or phrase from the broadcaster’s list of blocked terms.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#remove-blocked-term">remove blocked term</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageBlockedTerms"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageBlockedTerms"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster (channel) to remove a blocked term from.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator of the broadcaster's channel.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="blockedTermId">The id of the blocked term to remove.</param>
public class RemoveBlockedTermRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string moderatorId,
    string blockedTermId
    )
    : HelixApiRequest<RemoveBlockedTermResponse>(
        "/moderation/blocked_terms" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId)
            .Add("id", blockedTermId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Delete;
}
