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
