
namespace TwitchySharp.Api.Helix.Charity;

/// <summary>
/// Contains information about a specific donation to a charity campaign.
/// </summary>
public record CharityDonation
{
    /// <summary>
    /// The unique id of the specific donation.
    /// </summary>
    public required CharityDonationId Id { get; init; }
    /// <summary>
    /// The id of the charity campaign this donation belongs to.
    /// </summary>
    public required CharityCampaignId CampaignId { get; init; }
    /// <summary>
    /// The user id of the user that made the donation.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The user login (username) of the user that made the donation.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that made the donation.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// Contains information on the amount of money the user donated.
    /// </summary>
    public required CharityAmount Amount { get; init; }
}
