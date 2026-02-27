using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

public interface IHaveAccessTokenDetails<out TDetails>
    where TDetails : IAccessTokenDetails
{
    public TDetails AccessTokenDetails { get; }
}

public abstract record AccessTokenDetailsResolutionResult
{
    public record Available<TDetails>(TDetails AccessTokenDetails) : AccessTokenDetailsResolutionResult, IHaveAccessTokenDetails<TDetails>
        where TDetails : IAccessTokenDetails;
    public record Valid<TDetails>(TDetails AccessTokenDetails) : Available<TDetails>(AccessTokenDetails)
        where TDetails: IAccessTokenDetails;
    public record Expired<TDetails>(TDetails AccessTokenDetails) : Available<TDetails>(AccessTokenDetails)
        where TDetails : IAccessTokenDetails;
    public record Revoked<TDetails>(TDetails AccessTokenDetails) : Available<TDetails>(AccessTokenDetails)
        where TDetails : IAccessTokenDetails;
    public record New<TDetails>(TDetails AccessTokenDetails) : Valid<TDetails>(AccessTokenDetails)
        where TDetails : IAccessTokenDetails;
    public record Unavailable() : AccessTokenDetailsResolutionResult
    {
        public static Unavailable Instance { get; } = new();
    }
    public record NotRequired() : AccessTokenDetailsResolutionResult
    {
        public static NotRequired Instance { get; } = new();
    }
}
