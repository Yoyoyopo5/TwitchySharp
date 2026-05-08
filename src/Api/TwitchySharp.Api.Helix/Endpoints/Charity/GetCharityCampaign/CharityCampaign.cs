namespace TwitchySharp.Api.Helix.Charity;

/// <summary>
/// Contains information about a Twitch charity campaign.
/// </summary>
public record CharityCampaign
{
    /// <summary>
    /// An ID that identifies the charity campaign.
    /// </summary>
    public required CharityId Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster that's running the campaign.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The user login (username) of the broadcaster that's running the campaign.
    /// </summary>
    public required UserLogin BroadcasterLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster that's running the campaign.
    /// </summary>
    public required UserName BroadcasterName { get; init; }
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
    public required Uri CharityLogo { get; init; }
    /// <summary>
    /// A URL to the charity’s website.
    /// </summary>
    public required Uri CharityWebsite { get; init; }
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
