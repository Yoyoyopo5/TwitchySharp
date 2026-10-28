# TwitchySharp

TwitchySharp is a third-party Twitch API/EventSub wrapper for .NET Core 8.0+.

## Pre Release

TwitchySharp 2.0 is in beta pre-release. Additional breaking changes are possible.

## Features and Usage

Features of TwitchySharp are broken up into various modular packages. Depending on your application's use case, you may only need a subset of the included packages.

### Api

The core TwitchySharp.Api package includes the `ITwitchClient` abstraction, its default immutable implementation `TwitchClient`, and the base `TwitchRequest` and `TwitchResponse` types. **NOTE** the core Api package does **not** include any Authentication or Helix endpoints.

`ITwitchClient` defines the single generic method `SendAsync<T>` accepting a `TwitchRequest<T>` and returning `Task<TwitchResponse<T>>`. Each API endpoint is accessed by a specific type deriving from `TwitchRequest<T>` (e.g. `SendChatMessageRequest`). The generic parameter represents the response type. `TwitchResponse<T>` is a **result** type, containing either the `T` content or an `Error`. By default, `TwitchClient` leverages this result pattern to avoid throwing exceptions in most cases. `Content` and `Error` are both marked nullable. You can pattern match to handle the two cases:

```cs
var response = client.SendAsync(request, ct);
if (response.Content is not null)
{
  // Request was successful
}
if (response.Error is not null)
{
  // Handle the error
}
```

Alternatively, if your operation has a return type, you can use the functional `Match` method:

```cs
var response = client.SendAsync(request, ct);
bool success = response.Map(
  content => true 
  error => false
);
```

The `TwitchClient` implementation of `ITwitchClient` has been designed to support as many use-cases as possible through open-ended configuration. You probably do **not** need to implement `ITwitchClient` yourself. Extensions supporting common use-cases are included in the Api library.

*Example: Rate Limiting*
```cs
var client = new TwitchClient().WithRateLimiting();
```

A more detailed `TwitchClient` configuration guide is available here: [Configuring Twitch Client](docs/configuring-twitch-client.md).

#### Authentication

The TwitchySharp.Api.Authentication package extends the TwitchySharp.Api package by adding request types for the [Twitch Developer API](https://dev.twitch.tv/docs/authentication/) authentication endpoints. These include endpoints for acquiring access tokens required for most Helix API operations. In addition, there are helper types for creating oauth2 URLs for user authorization.

The package also includes additional configuration extensions for `TwitchClient` that make it easy to deal with request authentication. These are:

##### `UseAppAccessTokens`

Enables automatic app access token acquisition and caching. A client secret must also be configured (typically using `TwitchClient.WithClient()`). Adding this feature will automatically resolve app access tokens for API requests that require them.

```cs
new TwitchClient()
  .UseAppAccessTokens()
  .WithClient(clientId, clientSecret);
```

Configuring multiple clients is supported. `WithClient` will associate the secret with the client id. You may also configure a secret resolver directly:

```cs
// An example secret resolver using a hypothetical external secret store.
.SetResolver<TwitchClient, ClientSecret?>((scope, ct) => myExternalSecretCache.GetAsync(scope.ResolveOrDefault<ClientId?>(ct)));
```

If `WithClient` is not used and a dedicated resolver is not configured (or the resolver returns `null`), `SendAsync` will return a `MissingRequiredDependencyError` if the required token is not cached or is expired. This is because a client secret is required to obtain a new app access token from Twitch.

##### `UseUserAccessTokens`

Enables automatic user access token refreshing and caching. A client secret must also be configured to refresh tokens. Adding this feature will automatically resolve user access tokens for API requests that require them.

Since user access tokens **cannot** be acquired without manual user interaction (the authorizing user must interact with a browser), a token cache must be manually configured. Your application can then insert newly minted tokens into the cache (e.g. sending `AuthorizationCodeRequest` after receiving an authorization code from an HTTP listener). The `IRequestDependencyCache<TwitchIdentity.User, AccessTokenDetails.User>` is the abstraction defined for the cache.

```cs
class MyTokenCache : IRequestDependencyCache<TwitchIdentity.User, AccessTokenDetails.User>
{
    private Dictionary<TwitchIdentity.User, AccessTokenDetails.User> _cache = [];

    public ValueTask<AccessTokenDetails.User?> GetOrDefault(TwitchIdentity.User key, CancellationToken ct)
      => ValueTask.FromResult(_cache.GetValueOrDefault(key));

    public ValueTask Set(TwitchIdentity.User key, AccessTokenDetails.User value, CancellationToken ct)
    {
        _cache[key] = value;
        return ValueTask.CompletedTask;
    }
}

MyTokenCache cache = new();
cache.Set(user, accessTokenDetails);

new TwitchClient()
  .UseUserAccessTokens(cache)
  .WithClient(clientId, clientSecret);
```

Cache and refresh operations are serialized against the `TwitchIdentity.User` (i.e. two concurrent requests with the same user identity will perform these operations sequentially instead of in parallel). The locking mechanism can be further configured using the `LockFactory` property with the optional `Func<Options, Options> configureOptions` method parameter.

##### `UseExtensionJwts`

Enables automatic JWT signing and caching. Certain Helix endpoints interacting with Twitch Extensions require JSON Web Tokens signed with an extension secret. Adding this feature will automatically sign and resolve JWTs for these requests. Using this feature requires a configured resolver for `ExtensionSecret` on the `TwitchClient`, typically added via `WithExtension`. A custom resolver for `ExtensionOwnerId` and `ExtensionSecret` can also be configured if you require more than a fixed extension configuration. If either of these cannot be resolved and there is no unexpired cached JWT, `SendAsync` will return a `MissingRequiredDependencyError`.

```cs
new TwitchClient()
  .UseExtensionJwts()
  .WithExtension(extensionId, extensionOwnerId, extensionSecret);
```

This will associate a specific extension id to its owner's user id and one of its configured secrets. Because they are commonly confused, note that extension secrets are different than client secrets. An extension actually has both, since an extension is also a client. The important one for signing JWTs is the `ExtensionSecret` (sometimes referred to as the "shared secret").

---

Each extension exposes some configuration via the optional `Func<Options, Options> configureOptions` method parameter. You can also roll your own authentication stack via `SetResolver` and associated configuration methods on `BearerToken`.

Authentication of requests is one of the more complicated aspects of the Twitch API, and the TwitchySharp library tries to make it as painless as possible while still allowing deep configuration. More information on how authentication works in TwitchySharp is available [here](docs/authenticating-requests.md).

#### Helix

The TwitchySharp.Api.Helix package adds the [Twitch Helix API](https://dev.twitch.tv/docs/api/) request types. Types are divided by namespaces that roughly follow the official Twitch developer documentation.

Each request type is designed the avoid invalid operations as much as possible. As such, each type has its various idiosyncrasies to mirror the unique invariants of the corresponding endpoint. This may appear as required initializer properties, abstract base "union" types, or request-specific configuration methods, among others.

### EventSub

The core TwitchySharp.EventSub package includes all of the types for [EventSub](https://dev.twitch.tv/docs/eventsub/) notifications, along with the polymorphic deserializer needed to create them at runtime.

The `IEventSubNotification` is the main abstraction to be aware of, since it is returned from the polymorphic deserializer, and needs to be pattern matched into specific notification types.

#### Websocket

The TwitchySharp.EventSub.Websocket package handles the [EventSub WebSocket transport](https://dev.twitch.tv/docs/eventsub/handling-websocket-events/). It's client agnostic and can be used with almost any WebSocket client implementation with minor set up.

Starting with `EventSubWebsocketClient.Create`, pass a WebSocket client's lifecycle functions to construct the `StartEventSubWebsocketClient`:

```cs
var startEventSubClient = EventSubWebsocketClient.Create(context => {
  var client = new MyWebSocketClient();
  // Map your client's message event, observable, or delegate to the context's OnMessage.
  client.OnMessage = (stream, ct) => context.OnMessage(stream, ct);
  // Set the client url to connect to the context's Uri.
  client.Url = context.Uri;
  // Return a function that starts the connection...
  return async ct => {
    await client.StartAsync(ct);
    // and returns its stop function. 
    return ct => client.StopAsync(ct);
  };
});
```

I recommend also using the `WithReconnects` extension on the client. This will allow it to gracefully hand over connections when Twitch sends  a reconnect message. This does not handle network disconnects, server closing the connection, or other conditions (often those are handled by the WebSocket implementation).

```cs
startEventSubClient = startEventSubClient.WithReconnects();
```

Then this start function can be used to create the connection and wrap it into the message handler pipeline. The start function, when called, will create and start the client. It does not reuse created clients. The start function takes a message handler pipeline and a URL and returns a task of the stop function. 

The message handler pipeline is configurable with various built-in extension methods. Its default entry point is `ProcessWebsocketMessage.ByJsonDeserialization()`. Various extensions exist for configuring the pipeline.

```cs
var pipeline = ProcessWebsocketMessage.ByJsonDeserialization()
  // Keep track of already received message ids
  .WithIdempotentMessages()
  // Map messages into functions (e.g. the welcome message)
  .MapWelcome((session, ct) => {
    Console.WriteLine("Connected to EventSub!");
    // typically you would create your subscriptions here via the CreateEventSubSubscription Helix request.
    return ValueTask.CompletedTask;
  })
  // Map notification types (e.g. ChannelChatMessage)
  .MapNotification<ChannelChatMessageNotification>((notification, ct) => {
    Console.WriteLine(notification.Event.Message.Text);
    return ValueTask.CompletedTask;
  });

// Start the client
var stop = startEventSubClient(pipeline, EventSubWebsocketUrl.Default);
```

#### Webhooks

The TwitchySharp.EventSub.Webhooks package handles the [EventSub webhooks transport](https://dev.twitch.tv/docs/eventsub/handling-webhook-events/). Like the WebSockets package, it is framework agnostic, and can be implemented with any HTTP server implementation. An ASP.NET Core-specific wrapper is provided by the TwitchySharp.EventSub.Webhooks.AspNetCore package.

The `ProcessWebhookRequest` delegate type represents the core pipeline. Its entrypoint is `ProcessWebhookRequest.ByJsonDeserialization()`. It takes an `EventSubWebhookRequest` and returns a `ValueTask` of `Result<IWebhookRequestContent>`. The `Result` monad type is part of the functional infrastructure of TwitchySharp, and can be eliminated via `.Match()`. 

Apart from mapping the headers and body of the HTTP request into `EventSubWebhookRequest`, an implementation must also respond to [callback verification requests](https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#responding-to-a-challenge-request) correctly. The `IWebhookRequestContent` can be pattern matched into its implementing types (the important one being `CallbackVerificationRequestContent`). This contains the data needed to respond to the challenge request. The TwitchySharp.EventSub.Webhooks.AspNetCore package handles this automatically if you plan to use ASP.NET Core as the HTTP server implementation.

The pipeline can be configured with various extension methods, or it can be wrapped into a new `ProcessWebhookRequest` function to create custom extensions.

```cs
var pipeline = ProcessWebhookRequest.ByJsonDeserialization()
  // Ignore repeated message ids
  .WithIdempotentRequests();
  // Map notifications to functions
  .MapSubscriptionRevoked((subscription, ct) => {
    Console.WriteLine($"Subscription with id {subscription.Id} was revoked!");
    return ValueTask.CompletedTask;
  })
  .MapNotification<ChannelChatMessageNotification>((notification, ct) => {
    Console.WriteLine(notification.Event.Message.Text);
    return ValueTask.CompletedTask;
  });
```

Since the pipeline function returns an `IWebhookRequestContent`, you can also handle notifications from the return value instead of configuring them on the pipeline directly as side effects.

##### Crypto

The webhooks transport has a unique security requirement in that incoming requests must be validated to have originated from Twitch. Without validation, it's possible for an attacker to send well-formed notifications to your webhook endpoint and have your server accept them as legitimate.

As such, Twitch sends a computed hash signature with all webhook requests as an HTTP header. Twitch computes this signature using a secret that is provided when the subscription is created via the Helix API. The TwitchySharp.EventSub.Webhooks package includes an extension on `ProcessWebhookRequest` to validate this signature. `WithHashValidation` requires a `VerifyWebhookHash` function, which can be created via the static `VerifyWebhookHash.UsingSecret()` methods. It allows for using a fixed secret or a resolver function. The full resolver function is preferred for improved security, as each webhook subscription can have a unique secret.

```cs
pipeline = pipeline.WithHashValidation(VerifyWebhookHash.UsingSecret(myWebhookSecret));
// OR
pipeline = pipeline.WithHashValidation(VerifyWebhookHash.UsingSecret((subscription, ct) => mySecretStore.GetAsync(subscription.Id, ct)));
```

#### Webhooks.AspNetCore

The TwitchySharp.EventSub.Webhooks.AspNetCore package integrates the TwitchySharp.EventSub.Webhooks package with ASP.NET Core. Setting up your ASP.NET Core server to handle Twitch webhook requests is a two step process when configuring the host:

##### 1. AddTwitchEventSubWebhooks

`AddTwitchEventSubWebhooks()` extends the `IServiceProvider`. It takes a function argument that configures the `ProcessWebhookRequest` pipeline:

```cs
serviceProvider.AddTwitchEventSubWebhooks(sp => pipeline => pipeline
  .MapNotification<ChannelChatMessageNotification>((notification, ct) => {
    Console.WriteLine(notification.Event.Message.Text);
    return ValueTask.CompletedTask;
  })
);
```

Where `sp` is the `IServiceProvider` and `pipeline` is a default pipeline. The function argument is actually returning another function, which is the reason for the two `=>`. The second function returns the configured pipeline.

##### 2. MapTwitchWebhooks

Once the handler pipeline for your application is registered, you can map the webhook endpoint. `MapTwitchWebhooks()` extends the `IEndpointRouteBuilder` and takes a string pattern:

```cs
endpoints.MapTwitchWebhooks("/eventsub");
```

Webhook requests that are received by this endpoint will be forwarded to the configured pipeline.

## AI-Generated Code

I experimented a bit using Claude Code on this project in early 2026, but ended up going back to handwritten code. The majority (if not all) of the code and documentation of TwitchySharp is human-written as of October 2026, including tests.

## Contributing

Anyone is welcome to contribute to TwitchySharp. Contributions and input from people using the package especially valuable. All I ask is to not flood the repository with AI slop, since I will have to filter through it, although AI use is not disallowed. You may want to familiarize yourself with the [design priorities](docs/design-priorities.md).

There are no *strict* rules for contributions, but generally:
- Avoid trimmer-unsafe reflection
- Include XML documentation on public members
- Use existing infrastructure if available
- Avoid primitive obsession, prefer semantic wrapper types
- Do more with less

Please also create an issue before submitting a PR.

## Support TwitchySharp

I have been working on TwitchySharp since 2024. If this package has benefited you, and you would like to support TwitchySharp, consider following my [Twitch channel](https://www.twitch.tv/yoyoyopo5/), where I occasionally livestream. You can also donate directly through my [tip link](https://streamlabs.com/yoyoyopo5/tip). All contributions are greatly appreciated since I am currently out of work.