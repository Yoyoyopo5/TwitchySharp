using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.ChannelPoints;
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
    /// <param name="parameters">The request parameters.</param>
    /// <param name="redemptionStatus">The status to set the redemption to.</param>
    public UpdateRedemptionStatusRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        UpdateRedemptionStatusRequestParameters parameters,
        RewardRedemptionStatus redemptionStatus
        )
        : base(
            "/channel_points/custom_rewards/redemptions",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("id", parameters.Ids.Select(x => x.ToString()))
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("reward_id", parameters.RewardId)
            )
    {
        Method = HttpMethod.Patch;
        ContentObject = new UpdateRedemptionStatusRequestData() { Status = redemptionStatus };
    }
}

/// <summary>
/// Request parameters for a <see cref="UpdateRedemptionStatusRequest"/>.
/// </summary>
public record UpdateRedemptionStatusRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster who owns the custom reward.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token for the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The id of the custom reward to update redemptions on.
    /// </summary>
    public required RewardId RewardId { get; set; }
    /// <summary>
    /// A list of ids for the redemptions you want to update.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 50 ids.
    /// </remarks>
    public required IEnumerable<RewardRedemptionId> Ids { get; set; }
}

/// <summary>
/// Request data for a <see cref="UpdateRedemptionStatusRequest"/>.
/// </summary>
public record UpdateRedemptionStatusRequestData
{
    /// <summary>
    /// The status code that the redemption should be updated to.
    /// </summary>
    public required RewardRedemptionStatus Status { get; init; }
}
