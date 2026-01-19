using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.ChannelPoints;
/// <summary>
/// Deletes a custom reward that the broadcaster created.
/// </summary>
/// <remarks>
/// The app used to create the reward is the only app that may delete it.
/// If the reward’s redemption status is <see cref="RewardRedemptionStatus.Unfulfilled"/> at the time the reward is deleted, 
/// its redemption status is marked as <see cref="RewardRedemptionStatus.Fulfilled"/>.
/// <br/>
/// Requires a user access token with <see cref="Scope.ChannelManageRedemptions"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-custom-reward">Delete Custom Reward</see> for more information.
/// </remarks>
public record DeleteCustomRewardRequest
    : TwitchHelixRequest<DeleteCustomRewardResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token with <see cref="Scope.ChannelManageRedemptions"/>.</param>
    /// <param name="broadcasterId">
    /// The user id of the broadcaster that created the custom reward. 
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="rewardId">The ID of the custom reward to delete.</param>
    public DeleteCustomRewardRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string rewardId
        )
        : base(
            "/channel_points/custom_rewards",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("id", rewardId)
        )
    {
        Method = HttpMethod.Delete;
    }
}
