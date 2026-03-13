namespace TwitchySharp.Api.AuthorizationResolution;

public abstract record AccessTokenRefreshResult
{
    internal abstract AccessTokenDetailsResolutionResult ToResolutionResult();
    public record Refreshed<TDetails>(TDetails AccessTokenDetails) : AccessTokenRefreshResult 
        where TDetails : AccessTokenDetails
    {
        internal override AccessTokenDetailsResolutionResult ToResolutionResult()
            => new AccessTokenDetailsResolutionResult.New<TDetails>(AccessTokenDetails);
    }
    public record Expired<TDetails>(TDetails AccessTokenDetails) : AccessTokenRefreshResult
        where TDetails : AccessTokenDetails
    {
        internal override AccessTokenDetailsResolutionResult ToResolutionResult()
            => new AccessTokenDetailsResolutionResult.Expired<TDetails>(AccessTokenDetails);
    }
    public record Valid<TDetails>(TDetails AccessTokenDetails) : AccessTokenRefreshResult
        where TDetails : AccessTokenDetails
    {
        internal override AccessTokenDetailsResolutionResult ToResolutionResult()
            => new AccessTokenDetailsResolutionResult.Expired<TDetails>(AccessTokenDetails);
    }
}