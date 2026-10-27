# TwitchySharp.Api.Authentication

This package extends the TwitchySharp.Api package, adding request types for the [Twitch Developer API](https://dev.twitch.tv/docs/authentication/) authentication endpoints. These include endpoints for acquiring access tokens required for most Helix API operations. In addition, there are helper types for creating oauth2 URLs for user authorization and reading Twitch Oidc JWTs.

This package also adds a number of authentication-related extensions to the `TwitchClient` that handle many authentication operations such as token acquisition, caching, and refreshing.