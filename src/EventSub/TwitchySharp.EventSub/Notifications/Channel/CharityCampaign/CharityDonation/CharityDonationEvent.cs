namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.CharityDonation"/> event.
/// </summary>
public record CharityDonationEvent
{
    /// <summary>
    /// The id of the donation.
    /// </summary>
    public required CharityDonationId Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) who is hosting the charity campaign.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) who is hosting the charity campaign.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) who is hosting the charity campaign.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The name of the charity.
    /// </summary>
    public required CharityName CharityName { get; init; }
    /// <summary>
    /// The description of the charity.
    /// </summary>
    public required string CharityDescription { get; init; }
    /// <summary>
    /// A URL pointing to a 100x100 PNG image of the charity's logo.
    /// </summary>
    public required ImageUrl CharityLogo { get; init; }
    /// <summary>
    /// The URL of the charity's website.
    /// </summary>
    public required Url CharityWebsite { get; init; }
    /// <summary>
    /// The id of the charity campaign the donation was for.
    /// </summary>
    public required CharityCampaignId CampaignId { get; init; }
    /// <summary>
    /// The id of the user who made the donation.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user who made the donation.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user who made the donation.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The amount of money the user donated to the charity campaign.
    /// </summary>
    public required CharityAmount Amount { get; init; }
}
