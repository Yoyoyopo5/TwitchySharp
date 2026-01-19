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
