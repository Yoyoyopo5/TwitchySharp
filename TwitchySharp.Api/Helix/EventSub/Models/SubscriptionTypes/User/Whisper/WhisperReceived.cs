using System.Collections.Generic;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Constants;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A user receives a whisper.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadWhispers"/> or <see cref="Scope.UserManageWhispers"/>.
/// </remarks>
/// <param name="UserId">The user id of the user receiving the whisper.</param>
public sealed record WhisperReceived(string UserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.WHISPER_RECEIVED;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("user_id", UserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
