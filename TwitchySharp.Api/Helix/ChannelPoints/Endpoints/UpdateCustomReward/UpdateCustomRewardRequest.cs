using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.ChannelPoints;
/// <summary>
/// Updates a custom reward.
/// </summary>
/// <remarks>
/// The app used to create the reward is the only app that may update the reward.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageRedemptions"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-custom-reward">update custom reward</see> for more information.
/// </remarks>
public record UpdateCustomRewardRequest
    : TwitchHelixRequest<UpdateCustomRewardResponse>
{
    protected override string Path => "/channel_points/custom_rewards";
    public override HttpMethod Method => HttpMethod.Patch;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(BroadcasterId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ChannelManageRedemptions ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("id", RewardId);
    public override object? ContentObject => UpdatedReward;

    /// <summary>
    /// The user id of the broadcaster whose reward you want to update.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token for the request.
    /// Requires <see cref="Scope.ChannelManageRedemptions"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }

    /// <summary>
    /// The id of the reward to update.
    /// </summary>
    public required RewardId RewardId { get; set; }

    /// <summary>
    /// The data that the reward should be updated to.
    /// </summary>
    public required UpdateCustomRewardRequestData UpdatedReward { get; set; }
}

/// <summary>
/// Contains data used to update a single custom channel point reward.
/// </summary>
public record UpdateCustomRewardRequestData
{
    /// <summary>
    /// The reward's title.
    /// The title may contain a maximum of 45 characters and it must be unique amongst all of the broadcaster's custom rewards.
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// The prompt shown to the viewer when they redeem the reward.
    /// Specify a prompt if <see cref="IsUserInputRequired"/> is <see langword="true"/>.
    /// The prompt is limited to a maximum of 200 characters.
    /// </summary>
    public string? Prompt { get; set; }
    /// <summary>
    /// The cost of the reward, in channel points. The minimum is 1 point.
    /// </summary>
    public long? Cost { get; set; }
    /// <summary>
    /// The background color to use for the reward.
    /// </summary>
    public RgbColor? BackgroundColor { get; set; }
    /// <summary>
    /// Determines whether the reward is enabled.
    /// Set to <see langword="true"/> to enable the reward. Viewers see only enabled rewards.
    /// </summary>
    public bool? IsEnabled { get; set; }
    /// <summary>
    /// Determines whether users must enter information to redeem the reward.
    /// Set to true if user input is required.
    /// The <see cref="Prompt"/> is shown to the user if set to <see langword="true"/>.
    /// </summary>
    public bool? IsUserInputRequired { get; set; }
    /// <summary>
    /// Determines whether to limit the maximum number of redemptions allowed per live stream (amount specified with <see cref="MaxPerStream"/>).
    /// Set to <see langword="true"/> to limit redemptions.
    /// </summary>
    public bool? IsMaxPerStreamEnabled { get; set; }
    /// <summary>
    /// The maximum number of redemptions allowed per live stream.
    /// Applied only if <see cref="IsMaxPerStreamEnabled"/> is <see langword="true"/>. The minimum value is 1.
    /// </summary>
    public long? MaxPerStream { get; set; }
    /// <summary>
    /// Determines whether to limit the maximum number of redemptions allowed per user per stream (specified with <see cref="MaxPerUserPerStream"/>).
    /// The minimum value is 1. Set to <see langword="true"/> to limit redemptions.
    /// </summary>
    public bool? IsMaxPerUserPerStreamEnabled { get; set; }
    /// <summary>
    /// The maximum number of redemptions allowed per user per stream.
    /// Applied only if <see cref="IsMaxPerUserPerStreamEnabled"/> is <see langword="true"/>.
    /// </summary>
    public long? MaxPerUserPerStream { get; set; }
    /// <summary>
    /// Determines whether to apply a cooldown period between redemptions.
    /// Set to <see langword="true"/> to apply a cooldown period.
    /// The duration is specified by <see cref="GlobalCooldownSeconds"/>.
    /// </summary>
    public bool? IsGlobalCooldownEnabled { get; set; }
    /// <summary>
    /// The cooldown period.
    /// Applied only if <see cref="IsGlobalCooldownEnabled"/> is <see langword="true"/>.
    /// The minimum value is 1 second; however, for it to be shown in the Twitch UI, the minimum value is 60 seconds.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public TimeSpan? GlobalCooldownSeconds { get; set; }
    /// <summary>
    /// Determines whether to pause the reward.
    /// Set to <see langword="true"/> to pause the reward. Viewers can't redeem paused rewards.
    /// </summary>
    public bool? IsPaused { get; set; }
    /// <summary>
    /// Determines whether redemptions should be set to FULFILLED status immediately when a reward is redeemed.
    /// If <see langword="false"/>, status is set to UNFULFILLED and follows the normal request queue process.
    /// </summary>
    public bool? ShouldRedemptionsSkipRequestQueue { get; set; }
}
