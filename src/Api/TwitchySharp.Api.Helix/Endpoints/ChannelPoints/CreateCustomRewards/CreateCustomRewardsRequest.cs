using System.Collections.Immutable;
using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Helix.ChannelPoints;
/// <summary>
/// Creates a Custom Reward in the broadcaster's channel.
/// </summary>
/// <remarks>
/// The maximum number of custom rewards per channel is 50, which includes both enabled and disabled rewards.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageRedemptions"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-custom-rewards">Create Custom Rewards</see> for more information.
/// </remarks>
public record CreateCustomRewardsRequest
    : TwitchHelixRequest<CreateCustomRewardsResponse>
{
    protected override string Path => "/channel_points/custom_rewards";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManageRedemptions)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);
    public override object? ContentObject => Reward;

    /// <summary>
    /// The user id of the broadcaster (channel) to add the custom reward for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token for the request.
    /// Requires <see cref="Scope.ChannelManageRedemptions"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The reward to create.
    /// </summary>
    public required CreateCustomRewardsRequestData Reward { get; init; }
}

/// <summary>
/// Contains information used to create a new Channel Points reward.
/// </summary>
public record CreateCustomRewardsRequestData
{
    /// <summary>
    /// The custom reward's title.
    /// The title may contain a maximum of 45 characters and it must be unique amongst all of the broadcaster's custom rewards.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The cost of the reward, in Channel Points.
    /// The minimum is 1 point.
    /// </summary>
    public required long Cost { get; init; }
    /// <summary>
    /// The prompt shown to the viewer when they redeem the reward.
    /// Specify a prompt if <see cref="IsUserInputRequired"/> is <see langword="true"/>.
    /// The prompt is limited to a maximum of 200 characters.
    /// </summary>
    public string? Prompt { get; init; }
    /// <summary>
    /// Determines whether the reward is enabled.
    /// Viewers see only enabled rewards. The default is <see langword="true"/>.
    /// </summary>
    public bool IsEnabled { get; init; } = true;
    /// <summary>
    /// The background color to use for the reward.
    /// </summary>
    public RgbColor? BackgroundColor { get; init; }
    /// <summary>
    /// Determines whether the user needs to enter information when redeeming the reward.
    /// The default is <see langword="false"/>.
    /// </summary>
    public bool IsUserInputRequired { get; init; } = false;
    /// <summary>
    /// Determines whether to limit the maximum number of redemptions allowed per live stream.
    /// The default is <see langword="false"/>.
    /// </summary>
    public bool IsMaxPerStreamEnabled { get; init; } = false;
    /// <summary>
    /// The maximum number of redemptions allowed per live stream.
    /// Applied only if <see cref="IsMaxPerStreamEnabled"/> is <see langword="true"/>.
    /// The minimum value is 1.
    /// </summary>
    public int? MaxPerStream { get; init; }
    /// <summary>
    /// Determines whether to limit the maximum number of redemptions allowed per user per stream.
    /// The default is <see langword="false"/>.
    /// </summary>
    public bool IsMaxPerUserPerStreamEnabled { get; init; } = false;
    /// <summary>
    /// The maximum number of redemptions allowed per user per stream.
    /// Applied only if <see cref="IsMaxPerUserPerStreamEnabled"/> is <see langword="true"/>.
    /// The minimum value is 1.
    /// </summary>
    public int? MaxPerUserPerStream { get; init; }
    /// <summary>
    /// Determines whether to apply a cooldown period between redemptions.
    /// The default is <see langword="false"/>.
    /// </summary>
    public bool IsGlobalCooldownEnabled { get; init; } = false;
    /// <summary>
    /// The cooldown period, in <b>seconds</b>.
    /// Applied only if <see cref="IsGlobalCooldownEnabled"/> is <see langword="true"/>.
    /// The minimum value is 1; however, the minimum value is 60 for it to be shown in the Twitch UX.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    [JsonPropertyName("global_cooldown_seconds")]
    public TimeSpan? GlobalCooldown { get; init; }
    /// <summary>
    /// Determines whether redemptions should be set to <see cref="RewardRedemptionStatus.Fulfilled"/> immediately when a reward is redeemed.
    /// If <see langword="false"/>, status is set to <see cref="RewardRedemptionStatus.Unfulfilled"/> and follows the normal request queue process.
    /// The default is <see langword="false"/>.
    /// </summary>
    public bool ShouldRedemptionsSkipRequestQueue { get; init; } = false;
}
