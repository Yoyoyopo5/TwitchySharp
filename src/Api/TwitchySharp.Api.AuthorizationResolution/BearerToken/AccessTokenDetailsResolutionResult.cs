namespace TwitchySharp.Api.AuthorizationResolution;

internal abstract record AccessTokenDetailsResolutionResult
{
    public record Available<TDetails>(TDetails AccessTokenDetails) : AccessTokenDetailsResolutionResult
        where TDetails : AccessTokenDetails;
    public record Valid<TDetails>(TDetails AccessTokenDetails) : Available<TDetails>(AccessTokenDetails)
        where TDetails : AccessTokenDetails;
    public record Expired<TDetails>(TDetails AccessTokenDetails) : Available<TDetails>(AccessTokenDetails)
        where TDetails : AccessTokenDetails;
    public record Revoked<TDetails>(TDetails AccessTokenDetails) : Available<TDetails>(AccessTokenDetails)
        where TDetails : AccessTokenDetails;
    public record New<TDetails>(TDetails AccessTokenDetails) : Valid<TDetails>(AccessTokenDetails)
        where TDetails : AccessTokenDetails;
    public record Unavailable : AccessTokenDetailsResolutionResult
    {
        private Unavailable() { }
        public static Unavailable Instance { get; } = new();
    }
    public record NotRequired : AccessTokenDetailsResolutionResult
    {
        private NotRequired() { }
        public static NotRequired Instance { get; } = new();
    }

    internal static AccessTokenDetailsResolutionResult FromDetails<TDetails>(TDetails? details)
        where TDetails : AccessTokenDetails
        => details switch
        {
            TDetails exists when DateTimeOffset.UtcNow > exists.ExpiresAt => new Expired<TDetails>(details),
            TDetails => new Available<TDetails>(details),
            _ => Unavailable.Instance
        };
}
