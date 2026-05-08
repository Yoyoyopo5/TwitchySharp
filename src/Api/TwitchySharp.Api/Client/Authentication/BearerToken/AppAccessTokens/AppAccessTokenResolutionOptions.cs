using Microsoft.Extensions.Logging;

namespace TwitchySharp.Api;

// TODO: Remove token-specific pipelines

//public record AppAccessTokenResolutionOptions : ITokenResolutionOptions<AccessTokenDetails.App>
//{
//    public AccessTokenDetailsResolver<AccessTokenDetails.App>? GetCachedToken { get; init; }
//    public Func<AccessTokenDetails.App, CancellationToken, ValueTask>? OnNewToken { get; init; }

//    public required ITwitchClient AuthenticationClient { get; init; }
//    public required ClientSecretResolver ClientSecretResolver { get; init; }

//    public ILoggerFactory? LoggerFactory { get; init; }

//    TokenResolutionOptions<AccessTokenDetails.App> ITokenResolutionOptions<AccessTokenDetails.App>.ToTokenResolutionOptions()
//        => new()
//        {
//            GetCachedToken = GetCachedToken,
//            AcquireNewToken = AppAccessTokenAcquisition.CreateNewAppAccessTokenRequester(
//                AuthenticationClient,
//                ClientSecretResolver,
//                LoggerFactory
//                ),
//            RefreshToken = AppAccessTokenAcquisition.CreateNewAppAccessTokenRefresher(
//                AuthenticationClient,
//                ClientSecretResolver,
//                LoggerFactory
//                ),
//            OnNewToken = OnNewToken
//        };
//}
