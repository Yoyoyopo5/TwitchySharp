using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

public interface IHaveAccessToken<out TToken>
    where TToken : AccessToken
{
    public TToken? AccessToken { get; }
}

public abstract record AccessTokenResolutionResult
{
    public record Available<TToken>(TToken AccessToken) : AccessTokenResolutionResult, IHaveAccessToken<TToken>
        where TToken : AccessToken;
    public record Valid<TToken>(TToken AccessToken) : Available<TToken>(AccessToken)
        where TToken: AccessToken;
    public record Expired<TToken>(TToken AccessToken) : Available<TToken>(AccessToken)
        where TToken : AccessToken;
    public record Revoked<TToken>(TToken AccessToken) : Available<TToken>(AccessToken)
        where TToken : AccessToken;
    public record Unavailable() : AccessTokenResolutionResult
    {
        public static Unavailable Instance { get; } = new();
    }
    public record NotRequired() : AccessTokenResolutionResult
    {
        public static NotRequired Instance { get; } = new();
    }
}