using System.Collections.Generic;
using System.Collections.Immutable;
using TwitchySharp.Shared.EventSub;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A user receives a whisper.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadWhispers"/> or <see cref="Scope.UserManageWhispers"/>.
/// </remarks>
/// <param name="UserId">The user id of the user receiving the whisper.</param>
public sealed record WhisperReceived(UserId UserId)
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.WhisperReceived;
    public ConditionKey AuthorizingUserConditionKey => new ConditionKey("user_id");
    public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.UserReadWhispers, Scope.UserManageWhispers);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("user_id"), UserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
