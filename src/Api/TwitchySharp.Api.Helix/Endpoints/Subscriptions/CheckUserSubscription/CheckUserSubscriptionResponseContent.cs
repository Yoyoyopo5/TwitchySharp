namespace TwitchySharp.Api.Helix.Subscriptions;
/// <inheritdoc cref="UserSubscription"/>
public record CheckUserSubscriptionResponseContent
{
    /// <summary>
    /// A list containing a single object with information about the user's subscription.
    /// </summary>
    /// <remarks>
    /// If empty, the user is not subscribed.
    /// </remarks>
    public required UserSubscription[] Data { get; init; }
}
