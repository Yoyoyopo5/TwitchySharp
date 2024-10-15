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
/// Updates a custom reward.
/// The app used to create the reward is the only app that may update the reward.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-custom-reward">update custom reward</see> for more information.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageRedemptions"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token with <see cref="Scope.ChannelManageRedemptions"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster whose reward you want to update.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="rewardId">The id of the reward to update.</param>
/// <param name="updatedReward">The data that the reward should be updated to.</param>
public class UpdateCustomRewardRequest(
    string clientId, 
    string accessToken, 
    string broadcasterId, 
    string rewardId, 
    UpdateCustomRewardRequestData updatedReward
    )
    : HelixApiRequest<UpdateCustomRewardResponse, UpdateCustomRewardRequestData>(
        "/channel_points/custom_rewards" + 
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("id", rewardId),
        clientId,
        accessToken,
        updatedReward
        )
{
    public override HttpMethod Method => HttpMethod.Patch;
}

/// <summary>
/// Contains data used to update a single custom channel point reward.
/// </summary>
public record UpdateCustomRewardRequestData
{
    /// <summary>
    /// The reward’s title. 
    /// The title may contain a maximum of 45 characters and it must be unique amongst all of the broadcaster’s custom rewards.
    /// </summary>
    public string? Title { get; init; }
    /// <summary>
    /// The prompt shown to the viewer when they redeem the reward. 
    /// Specify a prompt if <see cref="IsUserInputRequired"/> is <see langword="true"/>.
    /// The prompt is limited to a maximum of 200 characters.
    /// </summary>
    public string? Prompt { get; init; }
    /// <summary>
    /// The cost of the reward, in channel points. The minimum is 1 point.
    /// </summary>
    public long? Cost { get; init; }
    /// <summary>
    /// The background color to use for the reward. 
    /// Specify the color using Hex format (for example, \#00E5CB).
    /// </summary>
    public string? BackgroundColor { get; init; }
    /// <summary>
    /// Determines whether the reward is enabled. 
    /// Set to <see langword="true"/> to enable the reward. Viewers see only enabled rewards.
    /// </summary>
    public bool? IsEnabled { get; init; }
    /// <summary>
    /// Determines whether users must enter information to redeem the reward. 
    /// Set to true if user input is required.
    /// The <see cref="Prompt"/> is shown to the user if set to <see langword="true"/>.
    /// </summary>
    public bool? IsUserInputRequired { get; init; }
    /// <summary>
    /// Determines whether to limit the maximum number of redemptions allowed per live stream (amount specified with <see cref="MaxPerStream"/>). 
    /// Set to <see langword="true"/> to limit redemptions.
    /// </summary>
    public bool? IsMaxPerStreamEnabled { get; init; }
    /// <summary>
    /// The maximum number of redemptions allowed per live stream. 
    /// Applied only if <see cref="IsMaxPerStreamEnabled"/> is <see langword="true"/>. The minimum value is 1.
    /// </summary>
    public long? MaxPerStream { get; init; }
    /// <summary>
    /// Determines whether to limit the maximum number of redemptions allowed per user per stream (specified with <see cref="MaxPerUserPerStream"/>). 
    /// The minimum value is 1. Set to <see langword="true"/> to limit redemptions.
    /// </summary>
    public bool? IsMaxPerUserPerStreamEnabled { get; init; }
    /// <summary>
    /// The maximum number of redemptions allowed per user per stream. 
    /// Applied only if <see cref="IsMaxPerUserPerStreamEnabled"/> is <see langword="true"/>.
    /// </summary>
    public long? MaxPerUserPerStream { get; init; }
    /// <summary>
    /// Determines whether to apply a cooldown period between redemptions. 
    /// Set to <see langword="true"/> to apply a cooldown period. 
    /// The duration is specified by <see cref="GlobalCooldownSeconds"/>.
    /// </summary>
    public bool? IsGlobalCooldownEnabled { get; init; }
    /// <summary>
    /// The cooldown period, in seconds. 
    /// Applied only if <see cref="IsGlobalCooldownEnabled"/> is <see langword="true"/>. 
    /// The minimum value is 1; however, for it to be shown in the Twitch UX, the minimum value is 60.
    /// </summary>
    public long? GlobalCooldownSeconds { get; init; }
    /// <summary>
    /// Determines whether to pause the reward. 
    /// Set to <see langword="true"/> to pause the reward. Viewers can’t redeem paused rewards.
    /// </summary>
    public bool? IsPaused { get; init; }
    /// <summary>
    /// Determines whether redemptions should be set to FULFILLED status immediately when a reward is redeemed. 
    /// If <see langword="false"/>, status is set to UNFULFILLED and follows the normal request queue process.
    /// </summary>
    public bool? ShouldRedemptionsSkipRequestQueue { get; init; }
}
