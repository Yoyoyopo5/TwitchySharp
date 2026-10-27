## Adding New Request Types

Adding new request types is fairly straightforward. Either inherit directly from the abstract record `TwitchRequest<T>` or the specialized request types `TwitchAuthenticationRequest<T>` or `TwitchHelixRequest<T>` in the TwitchySharp.Api.Authentication and TwitchySharp.Api.Helix packages, respectively.

### `TwitchRequest<T>`

The required abstract members are `HttpMethod Method` and `Uri RequestUri`. If you need to send `HttpContent`, there is a virtual `HttpContent? Content` and `object? ContentObject` members. By default, the `TwitchClient` will use `Content`, and if it is null, it will serialize `ContentObject` into JSON using `JsonSerializer`. If both are null, no content will be sent.

The other virtual member to be aware of is `ValueTask<TwitchResponse<TResponseContent>> CreateResponse(HttpResponseMessage response, CancellationToken ct)`. By default, the `HttpResponseMessage` content stream is JSON deserialized into the `T` type. If your request has a response that needs to map error codes to certain responses, is empty, or is otherwise not a standard JSON response, you will need to override `CreateResponse`. If you are developing for TwitchySharp, there is an internal static class `EmptyResponse` that should be used to create empty response types.

### `TwitchAuthenticationRequest<T>`

The `TwitchAuthenticationRequest<T>` type further derives `TwitchRequest<T>`, replacing `Uri RequestUri` with an `abstract string Path` (the path after "/oauth2") and `virtual HttpQueryParameters? QueryParameters`. These together are packaged into the request's `Uri`.

### `TwitchHelixRequest<T>`

Likewise, the `TwitchHelixRequest<T>` also further derives `TwitchRequest<T>` with `string Path` (the path after "/helix") and `HttpQueryParameters? QueryParameters`.

### `IAuthenticatedTwitchRequest<T>`

The `IAuthenticatedTwitchRequest<T>` should be implemented on any request that requires authentication. Its type parameter is a covariant `ITwitchRequestAuthenticationContext<TwitchIdentity>`, where `TwitchIdentity` can be any type deriving from `TwitchIdentity`.

Conceptually, the generic parameter represents the *type* of authentication that the request needs. This is evidenced by the types implementing `ITwitchRequestAuthenticationContext`:

#### `TwitchRequestAuthenticationContext<TIdentity>`

The generic authentication context with a variable identity type. This type of authentication has no extra features.

#### `UserWithScopesAuthenticationContext`

A context for `TwitchIdentity.User` that contains a `IReadOnlySet<Scope> ValidScopes` property. If a user access token has at least one of these scopes, it is considered valid. Implements `IHaveScopes`.

#### `UserSupportingPriorAuthorizationAuthenticationContext`

A context for `TwitchIdentity.User` that contains a flag property `bool UsePriorAuthorization`. It indicates whether to use an app access token in lieu of a user access token for a user authorized request. Some Helix API endpoints currently support this authentication type. The user must have authorized the app beforehand.