using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.ChannelPoints;
/// <summary>
/// Gets a list of redemptions for the specified custom reward.
/// </summary>
/// <remarks>
/// The app used to create the reward is the only app that may get the redemptions.
/// <br/>
/// Requires a user access token with <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-custom-reward-redemption">Get Custom Reward Redemption</see> for more information.
/// </remarks>
public record GetCustomRewardRedemptionRequest
    : TwitchHelixRequest<GetCustomRewardRedemptionResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token with <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetCustomRewardRedemptionRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetCustomRewardRedemptionRequestParameters parameters
        )
        : base(
            "/channel_points/custom_rewards/redemptions",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("reward_id", parameters.RewardId)
                .Add("status", parameters.Status?.ToString().ToUpperInvariant())
                .Add("id", parameters.Ids?.Select(x => x.ToString()))
                .Add("sort", parameters.Sort?.Value)
                .Add("after", parameters.After?.ToString())
                .Add("first", parameters.First?.ToString())
        )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetCustomRewardRedemptionRequest"/>.
/// </summary>
public record GetCustomRewardRedemptionRequestParameters
    : IPageableRequest
{
    // Enforce constraint that one of these properties must be set.
    /// <summary>
    /// Get custom reward redemptions by reward id.
    /// </summary>
    /// <param name="rewardId"><inheritdoc cref="RewardId" path="/summary"/></param>
    public GetCustomRewardRedemptionRequestParameters(RewardId rewardId)
        => RewardId = rewardId;
    /// <summary>
    /// Get custom reward redemptions by status.
    /// </summary>
    /// <param name="status"><inheritdoc cref="Status" path="/summary"/></param>
    public GetCustomRewardRedemptionRequestParameters(RewardRedemptionStatus status)
        => Status = status;
    /// <summary>
    /// Get custom reward redemptions by reward id and status.
    /// </summary>
    /// <param name="rewardId"><inheritdoc cref="RewardId" path="/summary"/></param>
    /// <param name="status"><inheritdoc cref="Status" path="/summary"/></param>
    public GetCustomRewardRedemptionRequestParameters(RewardId rewardId, RewardRedemptionStatus status)
        => (RewardId, Status) = (rewardId, status);

    /// <summary>
    /// The user id of the broadcaster that owns the reward to get redemptions for.
    /// </summary>
    /// <remarks>
    /// This must also be the user that created the user access token for the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The id that identifies the custom reward whose redemptions you want to get.
    /// </summary>
    public RewardId? RewardId { get; }
    /// <summary>
    /// The status of the redemptions to return.
    /// </summary>
    /// <remarks>
    /// Canceled and fulfilled redemptions are returned for only a few days after they’re canceled or fulfilled.
    /// </remarks>
    public RewardRedemptionStatus? Status { get; }
    /// <summary>
    /// A list of redemption ids to filter the redemptions by.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 50 ids.
    /// Duplicate ids are ignored. 
    /// The response contains only the ids that were found. 
    /// If none of the ids were found, the response is 404 Not Found.
    /// </remarks>
    public IEnumerable<RewardRedemptionId>? Ids { get; set; }
    /// <summary>
    /// The order to sort redemptions by.
    /// </summary>
    /// <remarks>
    /// The default is <see cref="CustomRewardRedemptionSortingMethod.Oldest"/>.
    /// </remarks>
    public CustomRewardRedemptionSortingMethod? Sort { get; set; }
    public PaginationCursor? After { get; set; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 redemption per page and the maximum is 50. 
    /// </remarks>
    public PaginationAmount? First { get; set; }
}
