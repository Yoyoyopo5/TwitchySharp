using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.ChannelPoints.Enums;
using TwitchySharp.Api.Models.Helix.ChannelPoints.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.ChannelPoints.Requests;
/// <summary>
/// Updates a redemption’s status. 
/// </summary>
/// <remarks>
/// You may update a redemption only if its status is <see cref="RewardRedemptionStatus.Unfulfilled"/>. 
/// The app used to create the reward is the only app that may update the redemption.
/// <br/>
/// Requires a user access token with <see cref="Scope.ChannelManageRedemptions"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-redemption-status">update redemption status</see> for more information.
/// </remarks>
public record UpdateRedemptionStatusRequest
    : TwitchHelixRequest<UpdateRedemptionStatusResponse>
{
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
    public UpdateRedemptionStatusRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string rewardId,
        IEnumerable<string> redemptionIds,
        RewardRedemptionStatus redemptionStatus
        )
        : base(
            "/channel_points/custom_rewards/redemptions",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("id", redemptionIds)
                .Add("broadcaster_id", broadcasterId)
                .Add("reward_id", rewardId)
            )
    {
        Method = HttpMethod.Patch;
        ContentObject = new UpdateRedemptionStatusRequestData() { Status = redemptionStatus };
    }
}

public record UpdateRedemptionStatusRequestData
{
    /// <summary>
    /// The status code that the redemption should be updated to.
    /// </summary>
    public required RewardRedemptionStatus Status { get; init; }
}
