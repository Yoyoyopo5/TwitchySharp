using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

public abstract record UserAccessTokenResolutionResult
{
    public sealed record Success(UserAccessToken Token) : UserAccessTokenResolutionResult;
    public sealed record Expired(UserAccessToken Token) : UserAccessTokenResolutionResult;
    public sealed record RequiresNewAuthorization() : UserAccessTokenResolutionResult;
    public sealed record Unavailable() : UserAccessTokenResolutionResult;
}
