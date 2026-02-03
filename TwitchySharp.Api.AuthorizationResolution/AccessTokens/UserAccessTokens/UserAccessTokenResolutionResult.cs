using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;
/// <summary>
/// Contains derived records for possible outcomes of <see cref="IResolveUserAccessToken.GetToken(UserAccessTokenKey, CancellationToken)"/>.
/// </summary>
public abstract record UserAccessTokenResolutionResult
{
    /// <summary>
    /// User access token was resolved successfully.
    /// </summary>
    /// <remarks>
    /// This can mean that the token was unexpired, or it was refreshed successfully.
    /// </remarks>
    public sealed record Success(UserAccessToken Token) : UserAccessTokenResolutionResult;
    /// <summary>
    /// The user access token is expired.
    /// </summary>
    /// <remarks>
    /// This can mean that the token could not be refreshed, or that refresh logic is not implemented.
    /// </remarks>
    public sealed record Expired(UserAccessToken Token) : UserAccessTokenResolutionResult;
    /// <summary>
    /// The user access token was not present or invalid, and a new manual authorization is required.
    /// </summary>
    public sealed record RequiresNewAuthorization() : UserAccessTokenResolutionResult;
    /// <summary>
    /// The user access token is unavailable.
    /// </summary>
    public sealed record Unavailable() : UserAccessTokenResolutionResult;
}
