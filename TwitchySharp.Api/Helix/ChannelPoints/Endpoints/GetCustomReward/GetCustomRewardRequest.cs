using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.ChannelPoints;
/// <summary>
/// Gets a list of custom rewards that the specified broadcaster created.
/// </summary>
/// <remarks>
/// A channel may offer a maximum of 50 rewards, which includes both enabled and disabled rewards.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-custom-reward">Get Custom Reward</see> for more information.
/// </remarks>
public record GetCustomRewardRequest
    : TwitchHelixRequest<GetCustomRewardResponse>
{
    protected override string Path => "/channel_points/custom_rewards";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(BroadcasterId);
    public override IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ChannelReadRedemptions, Scope.ChannelManageRedemptions);
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("id", RewardIds?.Select(x => x.ToString()))
            .Add("only_manageable_rewards", OnlyManageableRewards?.ToString());

    /// <summary>
    /// The user id of the broadcaster you want to get rewards for.
    /// </summary>
    /// <remarks>
    /// This should be the same user that created the access token for the request.
    /// Requires <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// A list of reward ids to filter the rewards by.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 50 ids.
    /// Duplicate ids are ignored.
    /// The response contains only the ids that were found.
    /// If none of the ids were found, the response is 404 Not Found.
    /// </remarks>
    public IEnumerable<RewardId>? RewardIds { get; init; }

    /// <summary>
    /// Determines whether the response contains only the custom rewards that the app may manage.
    /// </summary>
    /// <remarks>
    /// Set to <see langword="true"/> to get only the custom rewards that the app may manage.
    /// The default is <see langword="false"/>.
    /// </remarks>
    public bool? OnlyManageableRewards { get; init; }
}
