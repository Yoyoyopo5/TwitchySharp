using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A user has updated their account.
/// </summary>
/// <remarks>
/// No authorization required.
/// If the client id used to make the request has a user access token that includes <see cref="Scope.UserReadEmail"/>, the notification will include the email field.
/// </remarks>
/// <param name="UserId">The user id for the user you want update notifications for.</param>
public sealed record UserUpdate(UserId UserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<UserUpdate>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.UserUpdate;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.UserUpdate;

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("user_id"), UserId);
    public static Validation<UserUpdate> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("user_id"), out UserId UserId, value => new(value))
            .Map(_ => new UserUpdate(UserId));
}
