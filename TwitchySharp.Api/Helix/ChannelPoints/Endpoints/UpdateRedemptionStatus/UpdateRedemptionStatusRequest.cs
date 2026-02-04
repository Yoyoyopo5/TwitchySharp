using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.ChannelPoints;
/// <summary>
/// Updates a redemption's status.
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
    protected override string Path => "/channel_points/custom_rewards/redemptions";
    public override HttpMethod Method => HttpMethod.Patch;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(BroadcasterId);
    public override IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ChannelManageRedemptions);
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("id", Ids.Select(x => x.ToString()))
            .Add("broadcaster_id", BroadcasterId)
            .Add("reward_id", RewardId);
    public override object? ContentObject => new UpdateRedemptionStatusRequestData { Status = Status };

    /// <summary>
    /// The user id of the broadcaster who owns the custom reward.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token for the request.
    /// Requires <see cref="Scope.ChannelManageRedemptions"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The id of the custom reward to update redemptions on.
    /// </summary>
    public required RewardId RewardId { get; init; }

    /// <summary>
    /// A list of ids for the redemptions you want to update.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 50 ids.
    /// </remarks>
    public required IEnumerable<RewardRedemptionId> Ids { get; init; }

    /// <summary>
    /// The status to set the redemptions to.
    /// </summary>
    public required RewardRedemptionStatus Status { get; init; }
}

/// <summary>
/// Request data for a <see cref="UpdateRedemptionStatusRequest"/>.
/// </summary>
internal record UpdateRedemptionStatusRequestData
{
    /// <summary>
    /// The status code that the redemption should be updated to.
    /// </summary>
    [JsonPropertyName("status")]
    public required RewardRedemptionStatus Status { get; init; }
}
