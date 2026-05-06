using System.Collections.Generic;
using TwitchySharp.Shared.EventSub;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A Bits transaction occurred for a specified Twitch Extension.
/// </summary>
/// <remarks>
/// <b>Note:</b> This subscription type is only supported by the webhooks transport. It cannot be used with WebSockets.
/// Requires an app access token created by the <paramref name="ExtensionClientId"/>.
/// </remarks>
/// <param name="ExtensionClientId">The client id of the extension.</param>
public sealed record ExtensionBitsTransactionCreate(ClientId ExtensionClientId)
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ExtensionBitsTransactionCreate;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("extension_client_id"), ExtensionClientId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
