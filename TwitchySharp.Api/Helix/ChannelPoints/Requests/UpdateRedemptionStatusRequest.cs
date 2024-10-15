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
/// Updates a redemption’s status. 
/// You may update a redemption only if its status is <see cref="RewardRedemptionStatus.Unfulfilled"/>. 
/// The app used to create the reward is the only app that may update the redemption.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-redemption-status">update redemption status</see> for more information.
/// <br/>
/// Requires a user access token with <see cref="Scope.ChannelManageRedemptions"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token with <see cref="Scope.ChannelManageRedemptions"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster who owns the custom reward.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="rewardId">The unique id of the custom reward.</param>
/// <param name="redemptionIds">
/// A list of ids for the redemptions you want to update.
/// You may specify a maximum of 50 IDs.
/// </param>
/// <param name="redemptionStatus">
/// The status to set the redemption to.
/// </param>
public class UpdateRedemptionStatusRequest(
    string clientId, 
    string accessToken, 
    string broadcasterId, 
    string rewardId, 
    IEnumerable<string> redemptionIds,
    UpdatedRewardRedemptionStatus redemptionStatus // Primitive obsession? This seems easier to use.
    )
    : HelixApiRequest<UpdateRedemptionStatusResponse, UpdateRedemptionStatusRequestData>(
        "/channel_points/custom_rewards/redemptions" + 
        new HttpQueryParameters()
            .Add("id", redemptionIds)
            .Add("broadcaster_id", broadcasterId)
            .Add("reward_id", rewardId),
        clientId,
        accessToken,
        new UpdateRedemptionStatusRequestData() { Status = redemptionStatus }
        )
{
    public override HttpMethod Method => HttpMethod.Patch;
}

public readonly record struct UpdateRedemptionStatusRequestData
{
    /// <summary>
    /// The status code that the redemption should be updated to.
    /// </summary>
    public required UpdatedRewardRedemptionStatus Status { get; init; }
}

public enum UpdatedRewardRedemptionStatus
{
    /// <summary>
    /// Setting to cancelled refunds the user's channel points.
    /// </summary>
    Cancelled,
    /// <summary>
    /// Setting to fulfilled takes the user's channel points.
    /// </summary>
    Fulfilled
}
