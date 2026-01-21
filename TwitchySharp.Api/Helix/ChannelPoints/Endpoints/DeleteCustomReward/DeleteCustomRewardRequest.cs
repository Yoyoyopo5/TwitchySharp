using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

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
    /// <param name="parameters">The request parameters.</param>
    public DeleteCustomRewardRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        DeleteCustomRewardRequestParameters parameters
        )
        : base(
            "/channel_points/custom_rewards",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("id", parameters.RewardId)
        )
    {
        Method = HttpMethod.Delete;
    }
}

/// <summary>
/// Request parameters for a <see cref="DeleteCustomRewardRequest"/>.
/// </summary>
public record DeleteCustomRewardRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster that created the custom reward. 
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token for the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The id of the custom reward to delete.
    /// </summary>
    public required RewardId RewardId { get; set; }
}
