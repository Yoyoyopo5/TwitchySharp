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
    : TwitchHelixRequest<GetCustomRewardRedemptionResponse>, IPageableRequest
{
    protected override string Path => "/channel_points/custom_rewards/redemptions";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(BroadcasterId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ChannelReadRedemptions, Scope.ChannelManageRedemptions ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("reward_id", RewardId)
            .Add("status", Status?.ToString().ToUpperInvariant())
            .Add("id", Ids?.Select(x => x.ToString()))
            .Add("sort", Sort?.Value)
            .Add("after", After?.ToString())
            .Add("first", First?.ToString());

    /// <summary>
    /// The user id of the broadcaster that owns the reward to get redemptions for.
    /// </summary>
    /// <remarks>
    /// This must also be the user that created the user access token for the request.
    /// Requires <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The id that identifies the custom reward whose redemptions you want to get.
    /// </summary>
    /// <remarks>
    /// At least one of <see cref="RewardId"/> or <see cref="Status"/> should be specified.
    /// </remarks>
    public RewardId? RewardId { get; init; }

    /// <summary>
    /// The status of the redemptions to return.
    /// </summary>
    /// <remarks>
    /// Canceled and fulfilled redemptions are returned for only a few days after they're canceled or fulfilled.
    /// At least one of <see cref="RewardId"/> or <see cref="Status"/> should be specified.
    /// </remarks>
    public RewardRedemptionStatus? Status { get; init; }

    /// <summary>
    /// A list of redemption ids to filter the redemptions by.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 50 ids.
    /// Duplicate ids are ignored.
    /// The response contains only the ids that were found.
    /// If none of the ids were found, the response is 404 Not Found.
    /// </remarks>
    public IEnumerable<RewardRedemptionId>? Ids { get; init; }

    /// <summary>
    /// The order to sort redemptions by.
    /// </summary>
    /// <remarks>
    /// The default is <see cref="CustomRewardRedemptionSortingMethod.Oldest"/>.
    /// </remarks>
    public CustomRewardRedemptionSortingMethod? Sort { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }

    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 redemption per page and the maximum is 50.
    /// </remarks>
    public PaginationAmount? First { get; init; }
}
