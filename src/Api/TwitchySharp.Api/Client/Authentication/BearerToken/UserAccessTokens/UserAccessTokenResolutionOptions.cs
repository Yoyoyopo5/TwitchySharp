using Microsoft.Extensions.Logging;

namespace TwitchySharp.Api;

// TODO: Remove token-specific pipelines

///// <summary>
///// Set of options used for resolving user access tokens.
///// </summary>
///// <remarks>
///// Token refreshing logic is handled automatically using the <see cref="AuthenticationClient"/>.
///// </remarks>
//public record UserAccessTokenResolutionOptions : ITokenResolutionOptions<AccessTokenDetails.User>
//{
//    /// <inheritdoc/>
//    public AccessTokenDetailsResolver<AccessTokenDetails.User>? GetCachedToken { get; init; }
//    /// <inheritdoc/>
//    public Func<AccessTokenDetails.User, CancellationToken, ValueTask>? OnNewToken { get; init; }
//    /// <inheritdoc/>
//    public AccessTokenDetailsResolver<AccessTokenDetails.User>? AcquireNewToken { get; init; }

//    /// <summary>
//    /// The client secret resolver function to use when refreshing an expired user access token.
//    /// </summary>
//    public required ClientSecretResolver ClientSecretResolver { get; init; }
//    /// <summary>
//    /// The Twitch client to use when refreshing an expired user access token.
//    /// </summary>
//    /// <remarks>
//    /// Note that this client does not need its own authentication resolver because authorization headers
//    /// are not needed for the refresh endpoint.
//    /// </remarks>
//    public required ITwitchClient AuthenticationClient { get; init; }
//    /// <summary>
//    /// If the <see cref="AccessTokenDetails.User.Identity"/> has a <see langword="null"/> <see cref="ClientId"/>,
//    /// this function will be called to get the client id for the refresh request.
//    /// </summary>
//    public Func<AccessTokenDetails.User, CancellationToken, ValueTask<ClientId?>>? ResolveFallbackClientId { get; init; }
//    /// <summary>
//    /// The logger factory to use for the refresh pipeline, if any.
//    /// </summary>
//    public ILoggerFactory? LoggerFactory { get; init; }

//    TokenResolutionOptions<AccessTokenDetails.User> ITokenResolutionOptions<AccessTokenDetails.User>.ToTokenResolutionOptions()
//        => new()
//        {
//            GetCachedToken = GetCachedToken,
//            AcquireNewToken = AcquireNewToken,
//            RefreshToken = TokenRefreshing.CreateUserAccessTokenRefresher(
//                ClientSecretResolver,
//                AuthenticationClient,
//                ResolveFallbackClientId,
//                LoggerFactory
//                ),
//            OnNewToken = OnNewToken
//        };
//}
