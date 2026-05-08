namespace TwitchySharp.Api;

internal interface ITokenResolutionOptions<TDetails>
    where TDetails : AccessTokenDetails
{
    TokenResolutionOptions<TDetails> ToTokenResolutionOptions();
}

internal record TokenResolutionOptions<TDetails>
    where TDetails : AccessTokenDetails
{
    /// <summary>
    /// The function used to get a cached token.
    /// </summary>
    /// <remarks>
    /// This is the first function that runs on token resolution, and if it returns <see cref="AccessTokenDetailsResolutionResult.Valid{TDetails}"/> 
    /// or <see cref="AccessTokenDetailsResolutionResult.Available{TDetails}"/>, the token is returned immediately.
    /// </remarks>
    public AccessTokenDetailsResolver<TDetails>? GetCachedToken { get; init; }
    /// <summary>
    /// The function to run when <see cref="GetCachedToken"/> returns <see cref="AccessTokenDetailsResolutionResult.Unavailable"/>
    /// or <see cref="AccessTokenDetailsResolutionResult.Revoked{TDetails}"/>.
    /// </summary>
    public AccessTokenDetailsResolver<TDetails>? AcquireNewToken { get; init; }
    /// <summary>
    /// The function to run when <see cref="GetCachedToken"/> returns <see cref="AccessTokenDetailsResolutionResult.Expired{TDetails}"/>.
    /// </summary>
    public AccessTokenRefresher<TDetails>? RefreshToken { get; init; }
    /// <summary>
    /// The side effect function to run when the <see cref="RefreshToken"/> or <see cref="AcquireNewToken"/> returns <see cref="AccessTokenDetailsResolutionResult.New{TDetails}"/>.
    /// </summary>
    public Func<TDetails, CancellationToken, ValueTask>? OnNewToken { get; init; }
}
