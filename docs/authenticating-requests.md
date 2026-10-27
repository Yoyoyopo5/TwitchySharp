## Authenticating Requests

### Bearer Token Types

Request authentication is one of the more difficult concepts in the Twitch API. There are three types of bearer tokens that the Twitch API uses:

1. App Access Token - Obtained via `ClientCredentialsRequest`.
2. User Access Token - Obtained via `AuthorizationCodeRequest` or `DeviceCodeTokenRequest`.
3. Extension JSON Web Token - Obtained via `ExtensionJwtPayload.Sign`.

Each authenticated request type (implementing `IAuthenticatedTwitchRequest<T>`) uses one of these tokens. Some requests can use an app *or* user access token.

### `IAuthenticatedTwitchRequest<T>`

Requests that require authentication (most Helix endpoints) will implement this interface. Its presence tells the default `TwitchClient` to add the `Client-Id` and `Authorization` headers to the `HttpRequestMessage`.

The interface adds the `AuthenticationContext` property of type `T`. `T` is constrained to be the covariant `ITwitchRequestAuthenticationContext<TwitchIdentity>`, which contains information used to authenticate the request.

`ITwitchRequestAuthenticationContext<T>` contains the `BearerTokenType`, an optional `BearerToken?`, and the `T` `TwitchIdentity` type, of which there are three corresponding to the token types listed above:

1. `TwitchIdentity.Client`
2. `TwitchIdentity.User`
3. `TwitchIdentity.Extension`

`AuthenticationContext` *can* be set manually, but usually the request will configure it automatically based on the other properties of the request. The `BearerToken?` property can be set to override any further identity-based bearer token resolution by the `TwitchClient`.

### `TwitchIdentity`

The `TwitchIdentity` abstract record contains itself a `ClientId`, then derives itself with the nested records listed above to add information describing an identity that a request is to be made under. This identity is then used by the `TwitchClient`, along with the `BearerTokenType` property in `ITwitchRequestAuthenticationContext<T>` to set the `Client-Id` and `Authorization` headers on the request.

Obviously an identity does not carry the `BearerToken` itself. Rather, it represents information that can map to a specific `BearerToken`. This interacts with the `UseAppAccessTokens()`, `UseUserAccessTokens()`, and `UseExtensionJwts()` `TwitchClient` extension methods provided by TwitchySharp.Api.Authentication.