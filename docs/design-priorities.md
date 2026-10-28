## Design Priorities

### Immutability

Immutable data structures are preferred in TwitchySharp. You may notice most types are `record`. This preserves immutability while giving access to the powerful `with` operator to create variations of immutable instances.

TwitchySharp is expected to be used in highly asynchronous environments. Immutability prevents an enormous amount of race-condition scenarios and removes most of the guesswork of thread-safety, making parallelism easy. For example, `TwitchClient` is fully immutable, meaning its usage can be parallelized even if different threads need to change its configuration.

Some types remain mutable, such as caches, since they often need to maintain consistency across threads.

### Extensibility

TwitchySharp is designed to be as extensible as possible. The Twitch API is an ever moving target, and there will be points in time where TwitchySharp is lacking support for a specific Twitch API feature. As such, there are very few truly closed types. 

For example, enum-like types (e.g. `SuspiciousUserType`) are implemented through with wrapper structs containing static definitions. This gives discovery of common values while allowing consumers to define arbitrary values without modifying the library.

The abstract `TwitchRequest<T>` can be derived to add new request types. The same with `EventSubNotification<TEvent, TCondition>` for new EventSub types, although they also must be registered in the message pipeline's `DeserializeNotification` for polymorphic deserialization.

Many types in TwitchySharp are `delegate`. These can be extended by creating a new delegate (typically via a lambda) that uses the prior function. In cases where behavior must be generic at call time (a generic method on a non-generic type, e.g. `ITwitchClient.SendAsync<T>()`), interfaces are used.

### Type Safety

One of the major goals of TwitchySharp is to eliminate runtime errors that arise from malformed API requests. Using the .NET type system, many (but not all) of these malformations can be recognized at compile-time, meaning the invalid request is not even representable.

The Twitch Developer API is rife with complex invariants and parameter interactions. Capturing this complexity within the type system not only makes runtime errors less common, it also means consumers don't need to be as familiar with the Twitch API to use it effectively, since the type system can guide them into making well-formed requests.

Type safety also means eliminating primitive obsession. Most primitive types in the Twitch API have rich semantics. An integer can be Automod severity, Cheermote tier, Hype Train level, etc. A string can be hundreds of different types. In order to prevent accidentally mixing up two of these meaningful string types (e.g. `UserId` and `UserLogin`), opaque `Wrapper` source-generated types from the [`Yoyoyopo5.ValueWrapper`](https://github.com/Yoyoyopo5/ValueWrapper) package are used.

### Functional Focus

While TwitchySharp is not a purely functional library, it borrows many functional concepts in its design. Function passing and wrapping ("decorating") is common. `delegate` mostly replaces `interface` when possible. I am aware of the negative performance implications of this design. Given that the execution time of most operations will be mainly determined by network calls on the order of tens to hundreds of milliseconds, the minor penalty of reduced function inlining is worth the significantly reduced boilerplate afforded by a `delegate`-based approach, in my opinion. For example, instead of a consumer needing to implement a number of interfaces as concrete types, they can simply pass a lambda created in-place.
