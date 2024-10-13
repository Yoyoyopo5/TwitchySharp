using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.ChannelPoints;
/// <summary>
/// Deletes a custom reward that the broadcaster created. 
/// The app used to create the reward is the only app that may delete it.
/// If the reward’s redemption status is UNFULFILLED at the time the reward is deleted, its redemption status is marked as FULFILLED.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-custom-reward">delete custom reward</see> for more information.
/// <br/>
/// Requires a user access token with <see cref="Scope.ChannelManageRedemptions"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token with <see cref="Scope.ChannelManageRedemptions"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster that created the custom reward. 
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="rewardId">The ID of the custom reward to delete.</param>
public class DeleteCustomRewardRequest(string clientId, string accessToken, string broadcasterId, string rewardId)
    : HelixApiRequest<DeleteCustomRewardResponse>(
        "/channel_points/custom_rewards" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("id", rewardId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Delete;
}
