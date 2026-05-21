namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.CharityCampaignStop"/> event.
/// </summary>
public record CharityCampaignStopEvent
{
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
    /// The id of the charity campaign.
    /// </summary>
    public required CharityCampaignId Id { get; init; }
    /// <summary>
    /// The current amount of donations for the campaign.
    /// </summary>
    public required CharityAmount CurrentAmount { get; init; }
    /// <summary>
    /// The target amount of donations for the campaign.
    /// </summary>
    public required CharityAmount TargetAmount { get; init; }
    /// <summary>
    /// The date and time when the charity campaign was stopped by the broadcaster.
    /// </summary>
    public required DateTimeOffset StoppedAt { get; init; }
}
