using System.Collections.Immutable;
using TwitchySharp.Api.Helix.EventSub.Subscriptions;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// Contains information about an existing EventSub subscription.
/// </summary>
public record EventSubSubscription
{
    /// <summary>
    /// The id of the subscription.
    /// </summary>
    public required EventSubSubscriptionId Id { get; init; }
    /// <summary>
    /// The subscription's status.
    /// </summary>
    /// <remarks>
    /// Note that the subscriber receives events only for <see cref="EventSubSubscriptionStatus.Enabled"/> subscriptions.
    /// </remarks>
    public required EventSubSubscriptionStatus Status { get; init; }
    /// <summary>
    /// The subscription’s type name.
    /// </summary>
    /// <remarks>
    /// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#subscription-types">Subscription Types</see>.
    /// </remarks>
    public required EventSubSubscriptionTypeName Type { get; init; }
    /// <summary>
    /// The version number that identifies this definition of the subscription type's data.
    /// </summary>
    /// <remarks>
    /// This in addition to the <see cref="Type"/> property identify exactly what notification will be sent through this subscription.
    /// </remarks>
    public required EventSubSubscriptionTypeVersion Version { get; init; }
    /// <summary>
    /// The subscription’s parameter values.
    /// </summary>
    /// <remarks>
    /// The exact keys depend on what the subscription type expects.
    /// </remarks>
    public required ImmutableDictionary<ConditionKey, string> Condition { get; init; }
    /// <summary>
    /// The date and time when the subscription was created.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
    /// <summary>
    /// The transport details used to send the notifications.
    /// </summary>
    public required EventSubSubscriptionTransport Transport { get; init; }
    /// <summary>
    /// The amount that the subscription counts against the application's limit.
    /// </summary>
    /// <remarks>
    /// See <see href="https://dev.twitch.tv/docs/eventsub/manage-subscriptions/#subscription-limits">subscription limits</see>.
    /// </remarks>
    public required int Cost { get; init; }
}

public record MissingEventSubSubscriptionTypeError(EventSubSubscriptionType UnregisteredType)
    : Error("The EventSubSubscription subscription type was not found in the provided registry."); 

public static class EventSubSubscriptionExtensions
{
    /// <summary>
    /// Get a <see cref="EventSubSubscriptionType"/> based on the type name and version of the subscription.
    /// </summary>
    /// <param name="subscription">The subscription to get the subscription type of.</param>
    /// <returns>The <see cref="EventSubSubscriptionType"/> of the subscription.</returns>
    public static EventSubSubscriptionType GetSubscriptionType(this EventSubSubscription subscription)
        => new(subscription.Type, subscription.Version);

    /// <summary>
    /// The default set of <see cref="EventSubSubscriptionType"/> mapped to a function creating the respective <see cref="EventSubSubscriptionTypeSpecification"/> from an <see cref="EventSubSubscription.Condition"/>.
    /// </summary>
    public static ImmutableDictionary<EventSubSubscriptionType, Func<IReadOnlyDictionary<ConditionKey, string>, Validation<EventSubSubscriptionTypeSpecification>>> DefaultSubscriptionTypeSpecificationRegistry { get; }
        = new Dictionary<EventSubSubscriptionType, Func<IReadOnlyDictionary<ConditionKey, string>, Validation<EventSubSubscriptionTypeSpecification>>>()
        .Register<AutomodMessageHold>()
        .Register<AutomodMessageHoldV2>()
        .Register<AutomodMessageUpdate>()
        .Register<AutomodMessageUpdateV2>()
        .Register<AutomodSettingsUpdate>()
        .Register<AutomodTermsUpdate>()
        .Register<ChannelBitsUse>()
        .Register<ChannelUpdate>()
        .Register<ChannelFollow>()
        .Register<ChannelAdBreakBegin>()
        .Register<ChannelChatClear>()
        .Register<ChannelChatClearUserMessages>()
        .Register<ChannelChatMessage>()
        .Register<ChannelChatMessageDelete>()
        .Register<ChannelChatNotification>()
        .Register<ChannelChatSettingsUpdate>()
        .Register<ChannelChatUserMessageHold>()
        .Register<ChannelChatUserMessageUpdate>()
        .Register<ChannelSharedChatSessionBegin>()
        .Register<ChannelSharedChatSessionUpdate>()
        .Register<ChannelSharedChatSessionEnd>()
        .Register<ChannelSubscribe>()
        .Register<ChannelSubscriptionEnd>()
        .Register<ChannelSubscriptionGift>()
        .Register<ChannelSubscriptionMessage>()
        .Register<ChannelCheer>()
        .Register<ChannelRaid>()
        .Register<ChannelBan>()
        .Register<ChannelUnban>()
        .Register<ChannelUnbanRequestCreate>()
        .Register<ChannelUnbanRequestResolve>()
        .Register<ChannelModerate>()
        .Register<ChannelModerateV2>()
        .Register<ChannelModeratorAdd>()
        .Register<ChannelModeratorRemove>()
        .Register<ChannelGuestStarSessionBegin>()
        .Register<ChannelGuestStarSessionEnd>()
        .Register<ChannelGuestStarGuestUpdate>()
        .Register<ChannelGuestStarSettingsUpdate>()
        .Register<ChannelPointsAutomaticRewardRedemptionAdd>()
        .Register<ChannelPointsAutomaticRewardRedemptionAddV2>()
        .Register<ChannelPointsCustomRewardAdd>()
        .Register<ChannelPointsCustomRewardUpdate>()
        .Register<ChannelPointsCustomRewardRemove>()
        .Register<ChannelPointsCustomRewardRedemptionAdd>()
        .Register<ChannelPointsCustomRewardRedemptionUpdate>()
        .Register<ChannelPollBegin>()
        .Register<ChannelPollProgress>()
        .Register<ChannelPollEnd>()
        .Register<ChannelPredictionBegin>()
        .Register<ChannelPredictionProgress>()
        .Register<ChannelPredictionLock>()
        .Register<ChannelPredictionEnd>()
        .Register<ChannelSuspiciousUserMessage>()
        .Register<ChannelSuspiciousUserUpdate>()
        .Register<ChannelVipAdd>()
        .Register<ChannelVipRemove>()
        .Register<ChannelWarningAcknowledgement>()
        .Register<ChannelWarningSend>()
        .Register<CharityDonation>()
        .Register<CharityCampaignStart>()
        .Register<CharityCampaignProgress>()
        .Register<CharityCampaignStop>()
        .Register<ConduitShardDisabled>()
        .Register<DropEntitlementGrant>()
        .Register<ExtensionBitsTransactionCreate>()
        .Register<GoalBegin>()
        .Register<GoalProgress>()
        .Register<GoalEnd>()
        .Register<HypeTrainBegin>()
        .Register<HypeTrainProgress>()
        .Register<HypeTrainEnd>()
        .Register<ShieldModeBegin>()
        .Register<ShieldModeEnd>()
        .Register<ShoutoutCreate>()
        .Register<ShoutoutReceived>()
        .Register<StreamOnline>()
        .Register<StreamOffline>()
        .Register<UserAuthorizationGrant>()
        .Register<UserAuthorizationRevoke>()
        .Register<UserUpdate>()
        .Register<WhisperReceived>()
        .ToImmutableDictionary();

    private static Dictionary<EventSubSubscriptionType, Func<IReadOnlyDictionary<ConditionKey, string>, Validation<EventSubSubscriptionTypeSpecification>>> Register<T>(
        this Dictionary<EventSubSubscriptionType, Func<IReadOnlyDictionary<ConditionKey, string>, Validation<EventSubSubscriptionTypeSpecification>>> subscriptionTypes
        )
        where T : EventSubSubscriptionTypeSpecification, IConditionConstructable<T>
    {
        subscriptionTypes.Add(T.SubscriptionType, condition => T.FromCondition(condition).Map(s => s as EventSubSubscriptionTypeSpecification));
        return subscriptionTypes;
    }

    /// <summary>
    /// Create a <see cref="EventSubSubscriptionTypeSpecification"/> from an existing <see cref="EventSubSubscription"/>.
    /// </summary>
    /// <param name="subscription">The <see cref="EventSubSubscription"/> to create the <see cref="EventSubSubscriptionTypeSpecification"/> from.</param>
    /// <param name="registry">
    /// The <see cref="EventSubSubscriptionType"/> registry to use.
    /// This represents a mapping between the <see cref="EventSubSubscription.Condition"/> and a derived <see cref="EventSubSubscriptionTypeSpecification"/> factory function.
    /// Leave <see langword="null"/> to use the <see cref="DefaultSubscriptionTypeSpecificationRegistry"/>.
    /// You may extend the default with your own types that implement <see cref="IConditionConstructable{T}"/>.
    /// </param>
    /// <returns></returns>
    public static Validation<EventSubSubscriptionTypeSpecification> ToSubscriptionTypeSpecification(
        this EventSubSubscription subscription,
        IReadOnlyDictionary<EventSubSubscriptionType, Func<IReadOnlyDictionary<ConditionKey, string>, Validation<EventSubSubscriptionTypeSpecification>>>? registry = null)
    {
        EventSubSubscriptionType subscriptionType = subscription.GetSubscriptionType();
        return (registry ?? DefaultSubscriptionTypeSpecificationRegistry).TryGetValue(subscriptionType, out Func<IReadOnlyDictionary<ConditionKey, string>, Validation<EventSubSubscriptionTypeSpecification>>? fromCondition)
            ? fromCondition(subscription.Condition)
            : new MissingEventSubSubscriptionTypeError(subscriptionType);
    }

    public static Validation<TwitchRequestAuthorizationContext> ToAuthorizationContext(
        this EventSubSubscription subscription,
        IReadOnlyDictionary<EventSubSubscriptionType, Func<IReadOnlyDictionary<ConditionKey, string>, Validation<EventSubSubscriptionTypeSpecification>>>? registry = null
        )
        => subscription.ToSubscriptionTypeSpecification(registry)
            .Map(typeSpec => new TwitchRequestAuthorizationContext()
                {
                    Identity = typeSpec.GetRequestIdentity(subscription.Transport.Method),
                    ValidScopes = typeSpec.ValidScopes
                });
}
