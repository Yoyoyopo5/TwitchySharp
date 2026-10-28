## Adding new EventSub Notifications

Adding support for new EventSub types requires not only creating the types necessary but also ensuring they can be deserialized polymorphically. Types can be added if you are contributing to TwitchySharp or just need to add an unsupported type without modifying the library.

### Add the Api Type

In order to create subscriptions of the new type, support must be added to `CreateEventSubSubscriptionRequest`.

The relevant base types and interfaces are in the `TwitchySharp.Api.Helix.EventSub` namespace. First, derive a type from `EventSubSubscriptionTypeSpecification`. If you are adding the type to the library, it should also implement `IConditionConstructable<T>` to work correctly with `DeleteEventSubSubscriptionRequest`.

New type specifications require an `EventSubSubscriptionType`. This contains the name (e.g. "automod.message.hold") and version (e.g. "2") of the subscription. 

They also require a `IReadOnlyDictionary<ConditionKey, object>` for the condition of the subscription. This holds subscription-specific metadata such as the broadcaster id or moderator id. You can map C# properties on the new type to the condition. Values are `object` and are serialized as JSON.

Finally, they require an `EventSubSubscriptionAuthenticationContext`, which determines what identity subscription creation and deletion should be made under. Currently, subscriptions can be `UserAuthorized`, `ClientAuthorized`, or `None`. Create one of these, and assign the correct identity (e.g. `AuthenticationContext => new EventSubSubscriptionAuthenticationContext.UserAuthorized() { Identity = new(userId) }`). `UserAuthorized` also supports scopes via the `ValidScopes` property.

#### Supporting `DeleteEventSubSubscription`

In order to be able to pass an `EventSubSubscription` (which is returned by `CreateEventSubSubscriptionRequest` and `GetEventSubSubscriptionRequest`) into `DeleteEventSubSubscriptionRequest` and have the request assign the correct authentication context, the `DeleteEventSubSubscriptionRequest` must know how to create the specification from its condition. It does this via a "specification registry" of type `IReadOnlyDictionary<EventSubSubscriptionType, Func<IReadOnlyDictionary<ConditionKey, string>, Result<EventSubSubscriptionTypeSpecification>>>`.

When adding new specification types to the library, the type must be added to the default registry at `EventSubSubscriptionExtensions.DefaultSubscriptionTypeSpecificationRegistry`. If adding a new specification type outside of TwitchySharp (e.g. in a consumer application), `DeleteEventSubSubscription` can be supported by passing the optional `specificationRegistry` into its constructor along with the subscription to delete. The `DefaultSubscriptionTypeSpecificationRegistry` is public, so it can be passed into `DeleteEventSubSubscription` with additional registrations.

### Add the EventSub Type

Receiving notifications for the new subscription type requires adding a type that implements `IEventSubNotification` and registering it with the polymorphic deserializer. The relevant namespaces are `TwitchySharp.EventSub.Notifications` and `TwitchySharp.EventSub.Serialization` in the TwitchySharp.EventSub package.

Adding the new type is fairly straightforward. The easiest way to accomplish it is to derive `EventSubNotification<TEvent, TCondition>` (which implements `IEventSubNotification`), then define the event and condition types. There is a set of common condition types that can be used in the `TwitchySharp.EventSub.Notifications` namespace (e.g. `BroadcasterCondition`, `BroadcasterModeratorCondition`). The event type should be a simple POCO.

There is also `EventSubNotificationWithMultipleEvents<TEvent, TCondition>` if the new type supports multiple events in one notification, although currently this is only used by `drop.entitlement.grant`.

#### Registering the new Type

If you're adding a new type to the library itself, simply register the new type in the private `DeserializeNotificationExtensions._defaultMap` with its `EventSubSubscriptionType`. If you want to add support for a new type defined in another assembly, the following process applies.

The `DeserializeNotification` delegate in `TwitchySharp.EventSub.Serialization` has a static extension `ByPolymorphicJsonDeserialization` that creates the function. It takes one optional parameter, the higher-order-function type:

```cs
Func<
    Func<EventSubSubscriptionType, Func<JsonDocument, Result<IEventSubNotification>>?>,
    Func<EventSubSubscriptionType, Func<JsonDocument, Result<IEventSubNotification>>?>
    >?
```

It appears deceptively intimidating. `Func<EventSubSubscriptionType, Func<JsonDocument, Result<IEventSubNotification>>?>` is really just a map ("dictionary") from `EventSubSubscriptionType` to a deserialize function. The higher-order function receives the default map, which can be extended and returned back to the function. For adding a new type that deserializes via `JsonSerializer`:

```cs
DeserializeNotification.ByPolymorphicJsonDeserialization(map => type => {
    if (type == new EventSubSubscriptionType(new("new.subscription.type"), new("1")))
        return json => JsonSerializer.Deserialize<NewSubscriptionTypeNotification>(json);
    return map(type);
});
```

The reason for passing `JsonDocument` instead of `Stream` is because the incoming notification must be parsed to determine its `EventSubSubscriptionType`, so the parsed `JsonDocument` is reused. If you want to use JSON deserializer other than `System.Text.Json`, you will have to map `JsonDocument` into a type it understands. The `JsonDocument` is disposed by the returned `DeserializeNotification` function, so you don't need to worry about disposing it after deserialization.

The new `DeserializeNotification` function needs to be passed into whichever transport's processing pipeline you end up using. Their factory functions accept the instance of `DeserializeNotification` to use when deserializing notifications. `ProcessWebhookRequest`, for example:

```cs
ProcessWebhookRequest.ByJsonDeserialization(options => options with { DeserializeNotification = myCustomDeserializer });
```
