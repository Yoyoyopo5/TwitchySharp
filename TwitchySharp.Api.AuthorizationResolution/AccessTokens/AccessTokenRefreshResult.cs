namespace TwitchySharp.Api.AuthorizationResolution;

public abstract record AccessTokenRefreshResult
{
    public abstract AccessTokenDetailsResolutionResult ToResolutionResult();
    public record Refreshed<TDetails>(TDetails AccessTokenDetails) : AccessTokenRefreshResult 
        where TDetails : IAccessTokenDetails
    {
        public static implicit operator AccessTokenDetailsResolutionResult(Refreshed<TDetails> refreshResult)
            => new AccessTokenDetailsResolutionResult.New<TDetails>(refreshResult.AccessTokenDetails);
        public override AccessTokenDetailsResolutionResult ToResolutionResult()
            => this;
    }
    public record Expired<TDetails>(TDetails AccessTokenDetails) : AccessTokenRefreshResult
        where TDetails : IAccessTokenDetails
    {
        public static implicit operator AccessTokenDetailsResolutionResult(Expired<TDetails> refreshResult)
            => new AccessTokenDetailsResolutionResult.Expired<TDetails>(refreshResult.AccessTokenDetails);
        public override AccessTokenDetailsResolutionResult ToResolutionResult()
            => this;
    }
    public record Valid<TDetails>(TDetails AccessTokenDetails) : AccessTokenRefreshResult
        where TDetails : IAccessTokenDetails
    {
        public static implicit operator AccessTokenDetailsResolutionResult(Valid<TDetails> refreshResult)
            => new AccessTokenDetailsResolutionResult.Expired<TDetails>(refreshResult.AccessTokenDetails);
        public override AccessTokenDetailsResolutionResult ToResolutionResult()
            => this;
    }
}