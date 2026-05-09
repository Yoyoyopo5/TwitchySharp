using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.User;
/// <summary>
/// A user receives a whisper.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadWhispers"/> or <see cref="Scope.UserManageWhispers"/>.
/// </remarks>
/// <param name="UserId">The user id of the user receiving the whisper.</param>
public sealed record WhisperReceived(UserId UserId)
    : IUserAuthorizedSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.WhisperReceived;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new("user_id");
    public IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.UserReadWhispers, Scope.UserManageWhispers);
    public UserId AuthorizingUser => UserId;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new("user_id"), UserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
