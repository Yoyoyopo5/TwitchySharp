using System.Collections.Generic;
using TwitchySharp.Shared.EventSub.Constants;
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
    public EventSubSubscriptionTypeName Name { get; } = new(EventSubSubscriptionTypeNames.EXTENSION_BITS_TRANSACTION_CREATE);
    public EventSubSubscriptionTypeVersion Version { get; } = new(EventSubSubscriptionTypeVersions.V1);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("extension_client_id", ExtensionClientId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
