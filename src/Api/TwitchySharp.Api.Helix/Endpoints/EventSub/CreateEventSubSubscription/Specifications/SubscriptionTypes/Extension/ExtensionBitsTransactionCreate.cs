using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A Bits transaction occurred for a specified Twitch Extension.
/// </summary>
/// <remarks>
/// <b>Note:</b> This subscription type is only supported by the webhooks transport. It cannot be used with WebSockets.
/// Requires an app access token created by the <paramref name="ExtensionClientId"/>.
/// </remarks>
/// <param name="ExtensionClientId">The client id of the extension.</param>
public sealed record ExtensionBitsTransactionCreate(ExtensionId ExtensionClientId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ExtensionBitsTransactionCreate>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ExtensionBitsTransactionCreate;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ExtensionBitsTransactionCreate;
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.Client(ExtensionClientId);
    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("extension_client_id"), ExtensionClientId);
    public static Validation<ExtensionBitsTransactionCreate> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("extension_client_id"), out ExtensionId ExtensionClientId, value => new(value))
            .Map(_ => new ExtensionBitsTransactionCreate(ExtensionClientId));
}
