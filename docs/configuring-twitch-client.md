## Configuring the `TwitchClient`

The default implementation of `ITwitchClient`, `TwitchClient`, is highly configurable. Not only can the entire send pipeline be decorated, but each individual request dependency can be configured.

### Decorating `SendAsync`

Many cross-cutting concerns need to wrap the entire send operation (e.g. logging requests and responses). This can be done in two ways.

First, the `ITwitchClient` interface can be reimplemented, wrapping an internal `TwitchClient` to actually send the requests. This is fine for basic decoration, but it does not give access to any of the internal dependencies of the send operation (e.g. the `ClientId` that was used, what `BearerTokenType` was requested).

A more robust option is provided directly on `TwitchClient` using its nested `Middleware` type. Since any decorator must be implemented generically so it can apply to any arbitrary `TwitchRequest<T>`, an abstract class is used to hold the generic operation (as opposed to a `delegate`, which an instance of cannot be passed in generic form). `Middleware` is similar to the `DelegatingHandler` of `HttpClient`.

#### Implementing `TwitchClient.Middleware`

As an example, we will implement a middleware that tracks execution-time metrics for each request type. `Middleware` has one abstract `SendAsync<T>` that takes two parameters (plus a `CancellationToken`) and returns `ValueTask<TwitchResponse<T>>`. Those parameters are:

##### 1. `Func<ITwitchRequestDependencyScope, CancellationToken, ValueTask<TwitchResponse<TResponseContent>>> send`

The inner send function provided by `TwitchClient`. Use this to actually send the request.

##### 2. `ITwitchRequestDependencyScope scope`

The dependency scope context for the request. This can be used to resolve types that relate to the request, and is also used to execute the `send` parameter. The `ITwitchRequestDependencyScope` is discussed in more detail in the [next section](#configuring-request-dependencies).

---

First, we implement the method on the new middleware:

```cs
class MetricsMiddleware(MyMetricsService metrics) : TwitchClient.Middleware
{
    protected override async ValueTask<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(Func<ITwitchRequestDependencyScope, CancellationToken, ValueTask<TwitchResponse<TResponseContent>>> send, ITwitchRequestDependencyScope scope, CancellationToken ct)
    {
        var sw = Stopwatch.StartNew();
        var response = await send(scope, ct);
        sw.Stop();

        metrics.Add(scope.Request.GetType().Name, sw.Elapsed);

        return response;
    }
}
```

Then the new middleware must be added to the `TwitchClient`'s `ConfiguredMiddleware`. This can be done directly using `with` or you can use the `UseMiddleware` extension:

```cs
client = client.UseMiddleware(new MetricsMiddleware(metrics));
```

The execution order of middleware is in the order it is configured. This is the reverse order to typical `delegate` decoration. The first configured middleware receives a `send` function that calls the second configured middleware. The last middleware receives the function the actually sends the request and binds the response inside of `TwitchClient`.

### Configuring Request Dependencies

#### Background

One of the major tradeoffs of making `TwitchRequest<T>` immutable is that performing data transformations on it in a polymorphic context is difficult. For instance, some requests implement `IAuthenticatedTwitchRequest<T>`, which adds the immutable `AuthenticationContext` property to a request. When sending these requests through a client, it's useful for that client to be able to further customize the `AuthenticationContext` in a cross-cutting fashion. For example, the client could overwrite a default `ClientId` for all requests, so that it didn't have to be set manually for each request individually.

The problem is that there is currently no easy way to match a request as `IAuthenticatedTwitchRequest<T>` and then use `with` to set the `AuthenticationContext` property. The match cannot establish that the type is a record type, even though it obviously is. One option is to introduce a `WithX()` method on the interface, but this bloats the implementation and is cumbersome without a native `self` return type (which C# currently lacks).

Instead of using the request as its own transformable context, `TwitchClient` creates a general-purpose `IRequestDependencyScope` context for each request. This scope is created from a collection of dependency resolvers (`ITwitchRequestDependencyCollection`) that define how to resolve any particular dependency from the passed `TwitchRequest<T>`. The pattern is the same as the `IServiceCollection` and `IServiceProvider` in `Microsoft.Extensions.Hosting`, where `ITwitchRequestDependencyCollection` is analogous to `IServiceCollection` and `IRequestDependencyScope` to `IServiceProvider`.

When `SendAsync<T>(request, ct)` is called, the scope is created for the request, and the `HttpResponseMessage` dependency is resolved. By default, this in turn resolves `HttpRequestMessage` and `HttpClient` to obtain the `HttpResponseMessage`. Most configuration lives on the `HttpRequestMessage` resolver. For example, authenticated requests need to set `Client-Id` and `Authorization` headers on the `HttpRequestMessage`. This is accomplished by configuring ("decorating") the resolver for `HttpRequestMessage`.

#### Configuring `HttpRequestMessage`

As an example, we will configure the `HttpRequestMessage` resolver to include the `Client-Id` header (this is included by default on `TwitchClient`, so don't feel like you actually have to do this). First, notice that `TwitchClient` implements `ITwitchRequestDependencyCollection<TwitchClient>`. This gives access to a number of dependency configuration extensions. A few of the more important ones are:

##### 1. `SetResolver<T>(ResolveRequestDependency<T> resolve)`

Set a resolver function for a type `T` directly. This overwrites any previously configured resolver for that type, if it was present. There is also a `TrySetResolver` extension that only sets the resolver if one does not exist for that type.

##### 2. `Configure<TCollection, T>(Func<ResolveRequestDependency<T?>, ResolveRequestDependency<T>> configure)`

Configure an existing resolver for type `T` by wrapping it into a new resolver. `configure` receives the existing, previously configured resolver. Note the higher-order function parameter, since `ResolveRequestDependency<T>` is a delegate type.

##### 3. `SetFixed<TCollection, T>(T fixedValue)`

Set the resolver for type `T` to always return a fixed value.

##### 4. `When<TCollection>(ResolveRequestDependency<bool> predicate)`

Returns a conditional context that additional resolvers can be configured on. If `predicate` returns true, the conditional resolvers are used.

---

You'll notice that these functions all return `TCollection`, which is the `ITwitchRequestDependencyCollection` that is being configured. This is because most implementations of `ITwitchRequestDependencyCollection` are immutable, including `TwitchClient`. Unfortunately, extensions methods require `TCollection` to be specified as a generic parameter since C# has no partial type inference. There are many more extensions for more specialized scenarios, and these are documented in XML.

Continuing with our HTTP request header configuration, we can use the `Configure<TCollection, T>(configure)` extension method to use the previous resolver to actually create the `HttpRequestMessage`, then modify it in our own function:

```cs
client = client.Configure<TwitchClient, HttpRequestMessage?>(next => async (scope, ct) => {
    // `next` is the inner resolver function for HttpRequestMessage
    // `scope` is the IRequestDependencyScope, which we can use to resolve other types or read the `TwitchRequest<T>`.
    HttpRequestMessage request = await scope.ResolveOrDefault<HttpRequestMessage>(ct);
    
    if (await scope.ResolveOrDefault<ClientId?>(ct) is ClientId clientId)
    {
        request.Headers.Add("Client-Id", clientId.Value);
    }
    
    return request;
});
```

At first glance this can look intimidating. The two `=>` indicate the higher-order function, where we have a function that is returning another function. Remember that `next` is the previously configured resolver function for `HttpRequestMessage`. Then we are returning a new resolver with parameters `scope` and `ct`. The `scope` is `IRequestDependencyScope`, which has the `ResolveOrDefault<T>` method that can pull in other dependencies. In this case, we get the `ClientId?` (this dependency can also be configured in the same way). If it isn't null, we add it as the `Client-Id` header, then return the request.

#### `TwitchClient` Defaults

A new `TwitchClient` already contains a significant amount of dependency resolvers. By default, some of the resolvers it configures:

- `HttpContent`
- `HttpRequestMessage`
- `ITwitchAuthenticationContext<T>`
- `BearerToken?`
- `BearerTokenType?`
- `TwitchIdentity`
- `ClientId?`
- `UserId?`
- `ExtensionId?`
- `HttpClient`
- `HttpResponseMessage`

This will map the `TwitchRequest<T>` to an `HttpRequestMessage`, resolve the required bearer token, apply authentication headers if necessary, and send the request.

### Configuring Response Binding

The final step of sending a `TwitchRequest<T>` is binding `HttpResponseMessage` to the `T` response type. In most cases, this is defined by the request itself, and there is a virtual `CreateResponse(HttpResponseMessage response, CancellationToken ct)` method present on `TwitchRequest<T>` that is used by `SendAsync<T>` and returns `TwitchResponse<T>`.

Cross-cutting is still possible using client Middleware, or by configuring the resolver for `HttpResponseMessage`. This is used in rate limiting middleware, for example.