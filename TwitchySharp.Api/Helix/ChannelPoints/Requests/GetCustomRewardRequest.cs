using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using static System.Net.Mime.MediaTypeNames;

namespace TwitchySharp.Api.Helix.ChannelPoints;
/// <summary>
/// Gets a list of custom rewards that the specified broadcaster created.
/// <b>NOTE:</b> A channel may offer a maximum of 50 rewards, which includes both enabled and disabled rewards.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-custom-reward">get custom reward</see> for more information.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster you want to get rewards for.
/// This should be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="rewardIds">
/// A list of reward IDs to filter the rewards by. 
/// You may specify a maximum of 50 IDs. 
/// Duplicate IDs are ignored.
/// The response contains only the IDs that were found. 
/// If none of the IDs were found, the response is 404 Not Found.
/// </param>
/// <param name="onlyManageableRewards">
/// Determines whether the response contains only the custom rewards that the app may manage (as identified by the <paramref name="clientId"/> parameter).
/// Set to <see langword="true"/> to get only the custom rewards that the app may manage. 
/// The default is <see langword="false"/>.
/// </param>
public class GetCustomRewardRequest(
    string clientId, 
    string accessToken, 
    string broadcasterId, 
    IEnumerable<string>? rewardIds = null, 
    bool? onlyManageableRewards = null
    )
    : HelixApiRequest<GetCustomRewardResponse>(
        "/channel_points/custom_rewards" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("id", rewardIds)
            .Add("only_manageable_rewards", onlyManageableRewards.ToString()),
        clientId,
        accessToken
        );
