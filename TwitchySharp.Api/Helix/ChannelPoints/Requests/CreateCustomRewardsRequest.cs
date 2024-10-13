using System;
using System.Collections.Generic;
using System.Text;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.ChannelPoints;
public class CreateCustomRewardsRequest(
    string clientId, 
    string accessToken, 
    string broadcasterId, 
    CreateCustomRewardsRequestData reward)
    : HelixApiRequest<CreateCustomRewardsResponse, CreateCustomRewardsRequestData>(
        "/channel_points/custom_rewards" + 
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId),
        clientId,
        accessToken,
        reward
        );

public record CreateCustomRewardsRequestData
{
    public required string Title { get; init; }
    public required long Cost { get; init; }
    public string? Prompt { get; init; }
    public bool IsEnabled { get; init; } = true;
    public string? BackgroundColor { get; init; }
    public bool IsUserInputRequired { get; init; } = false;
    public bool IsMaxPerStreamEnabled { get; init; } = false;
    public int? MaxPerStream { get; init; }
    public bool IsMaxPerUserPerStreamEnabled { get; init; } = false;
    public int? MaxPerUserPerStream { get; init; }
    public bool IsGlobalCooldownEnabled { get; init; } = false;
    public int? GlobalCooldownSeconds { get; init; }
    public bool ShouldRedemptionsSkipRequestQueue { get; init; } = false;
}
