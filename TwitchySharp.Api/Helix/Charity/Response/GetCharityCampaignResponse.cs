using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

/// <summary>
/// Contains information about charity fundraising amounts and currencies.
/// </summary>
public readonly record struct CharityAmount
{
    /// <summary>
    /// The monetary amount. 
    /// The amount is specified in the currency’s minor unit. 
    /// For example, the minor units for USD is cents, so if the amount is $5.50 USD, value is set to 550.
    /// </summary>
    public required int Value { get; init; }
    /// <summary>
    /// The number of decimal places used by the currency. 
    /// For example, USD uses two decimal places. 
    /// </summary>
    public required int DecimalPlaces { get; init; }
    /// <summary>
    /// Calculated monetary value (dollar value) as given by <see cref="Value"/> and <see cref="DecimalPlaces"/>.
    /// Note that this value is calculated each time it is called.
    /// </summary>
    public double MonetaryValue => (double)Value / Math.Pow(10, DecimalPlaces);
    /// <summary>
    /// The ISO-4217 three-letter currency code that identifies the type of currency in value.
    /// </summary>
    public required string Currency { get; init; }
}
