using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Charity;
/// <summary>
/// Contains a list of donations for a charity campaign.
/// </summary>
public record GetCharityCampaignDonationsResponse
{
    /// <summary>
    /// A list that contains the donations that users have made to the broadcaster’s charity campaign. 
    /// The list is empty if the broadcaster is not currently running a charity campaign; the donation information is not available after the campaign ends.
    /// </summary>
    public required CharityDonation[] Data { get; init; }
    /// <summary>
    /// An object that contains the information used to page through the list of results. 
    /// The <see cref="Pagination.Cursor"/> property is <see langword="null"/> if there are no more pages left to page through.
    /// </summary>
    public required Pagination Pagination { get; init; }
}

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
