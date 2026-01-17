using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Models.Helix.Charity.Models;

/// <summary>
/// Contains information about a specific donation to a charity campaign.
/// </summary>
public record CharityDonation
{
    /// <summary>
    /// The unique id of the specific donation.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The id of the charity campaign this donation belongs to.
    /// </summary>
    public required string CampaignId { get; init; }
    /// <summary>
    /// The user id of the user that made the donation.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The user login (username) of the user that made the donation.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that made the donation.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// Contains information on the amount of money the user donated.
    /// </summary>
    public required CharityAmount Amount { get; init; }
}
