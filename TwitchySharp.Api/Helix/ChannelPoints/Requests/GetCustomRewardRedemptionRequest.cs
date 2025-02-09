using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.ChannelPoints;
/// <summary>
/// Gets a list of redemptions for the specified custom reward. 
/// The app used to create the reward is the only app that may get the redemptions.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-custom-reward-redemption">get custom reward redemption</see> for more information.
/// <br/>
/// Requires a user access token with <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token with <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster that owns the reward.
/// This must also be the user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="rewardId">
/// The ID that identifies the custom reward whose redemptions you want to get.
/// <b>NOTE:</b> either this parameter or <paramref name="status"/> is required.
/// </param>
/// <param name="status">
/// The status of the redemptions to return.
/// Canceled and fulfilled redemptions are returned for only a few days after they’re canceled or fulfilled.
/// <b>NOTE:</b> either this parameter or <paramref name="rewardId"/> is required.
/// </param>
/// <param name="ids">
/// A list of reward IDs to filter the redemptions by.
/// You may specify a maximum of 50 IDs.
/// Duplicate IDs are ignored. 
/// The response contains only the IDs that were found. 
/// If none of the IDs were found, the response is 404 Not Found.
/// </param>
/// <param name="sort">
/// The order to sort redemptions by.
/// The default is <see cref="CustomRewardRedemptionSortingMethod.Oldest"/>.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> property in the response contains the cursor’s value. 
/// </param>
/// <param name="first">
/// The maximum number of redemptions to return per page in the response.
/// The minimum page size is 1 redemption per page and the maximum is 50. 
/// The default is 20.
/// </param>
public class GetCustomRewardRedemptionRequest(
    string clientId, 
    string accessToken, 
    string broadcasterId, 
    string? rewardId = null, 
    RewardRedemptionStatus? status = null, 
    IEnumerable<string>? ids = null, 
    CustomRewardRedemptionSortingMethod? sort = null, 
    string? after = null, 
    int? first = null
    )
    : HelixApiRequest<GetCustomRewardRedemptionResponse>(
        "/channel_points/custom_rewards/redemptions" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("reward_id", rewardId)
            .Add("status", status?.ToString().ToUpperInvariant())
            .Add("id", ids)
            .Add("sort", sort?.ToString().ToUpperInvariant())
            .Add("after", after)
            .Add("first", first?.ToString()),
        clientId,
        accessToken
        );

/// <summary>
/// Possible values for sorting a <see cref="GetCustomRewardRedemptionRequest"/>.
/// </summary>
public enum CustomRewardRedemptionSortingMethod
{
    Oldest, Newest
}
