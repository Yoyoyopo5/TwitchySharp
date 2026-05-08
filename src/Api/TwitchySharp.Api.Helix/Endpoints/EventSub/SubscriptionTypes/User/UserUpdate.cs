namespace TwitchySharp.Api.Helix.EventSub.User;
/// <summary>
/// A user has updated their account.
/// </summary>
/// <remarks>
/// No authorization required.
/// If the client id used to make the request has a user access token that includes <see cref="Scope.UserReadEmail"/>, the notification will include the email field.
/// </remarks>
/// <param name="UserId">The user id for the user you want update notifications for.</param>
public sealed record UserUpdate(UserId UserId)
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.UserUpdate;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new("user_id"), UserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
