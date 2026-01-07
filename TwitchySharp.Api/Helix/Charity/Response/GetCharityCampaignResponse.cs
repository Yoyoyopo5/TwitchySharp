using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Charity;
/// <summary>
/// Contains a list of charity campaigns.
/// </summary>
public record GetCharityCampaignResponse
{
    /// <summary>
    /// A list that contains the charity campaign that the broadcaster is currently running. 
    /// The list is empty if the broadcaster is not running a charity campaign; the campaign information is not available after the campaign ends.
    /// </summary>
    public required CharityCampaign[] Data { get; init; }
}

/// <summary>
/// Contains information about a Twitch charity campaign.
/// </summary>
public record CharityCampaign
{
    /// <summary>
    /// An ID that identifies the charity campaign.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster that's running the campaign.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The user login (username) of the broadcaster that's running the campaign.
    /// </summary>
    public required string BroadcasterLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster that's running the campaign.
    /// </summary>
    public required string BroadcasterName { get; init; }
    /// <summary>
    /// The charity's name.
    /// </summary>
    public required string CharityName { get; init; }
    /// <summary>
    /// A description of the charity.
    /// </summary>
    public required string CharityDescription { get; init; }
    /// <summary>
    /// A URL to an image of the charity’s logo. 
    /// The image’s type is PNG and its size is 100px X 100px.
    /// </summary>
    public required string CharityLogo { get; init; }
    /// <summary>
    /// A URL to the charity’s website.
    /// </summary>
    public required string CharityWebsite { get; init; }
    /// <summary>
    /// The current amount of donations that the campaign has received.
    /// </summary>
    public required CharityAmount CurrentAmount { get; init; }
    /// <summary>
    /// The campaign’s fundraising goal. 
    /// This field is <see langword="null"/> if the broadcaster has not defined a fundraising goal.
    /// </summary>
    public CharityAmount? TargetAmount { get; init; }
}
