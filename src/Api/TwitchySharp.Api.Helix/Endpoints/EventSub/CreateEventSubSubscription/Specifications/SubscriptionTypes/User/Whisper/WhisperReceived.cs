using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A user receives a whisper.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadWhispers"/> or <see cref="Scope.UserManageWhispers"/>.
/// </remarks>
/// <param name="UserId">The user id of the user receiving the whisper.</param>
public sealed record WhisperReceived(UserId UserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<WhisperReceived>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.WhisperReceived;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.WhisperReceived;
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.UserReadWhispers, Scope.UserManageWhispers);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(UserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("user_id"), UserId);
    public static Validation<WhisperReceived> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("user_id"), out UserId UserId, value => new(value))
            .Map(_ => new WhisperReceived(UserId));
}
