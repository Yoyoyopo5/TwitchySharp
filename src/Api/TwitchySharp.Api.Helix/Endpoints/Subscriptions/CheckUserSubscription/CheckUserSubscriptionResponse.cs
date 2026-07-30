namespace TwitchySharp.Api.Helix.Subscriptions;
/// <inheritdoc cref="UserSubscription"/>
public record CheckUserSubscriptionResponse
{
    /// <summary>
    /// A list containing a single object with information about the user's subscription.
    /// </summary>
    public required UserSubscription[] Data { get; init; }
}
