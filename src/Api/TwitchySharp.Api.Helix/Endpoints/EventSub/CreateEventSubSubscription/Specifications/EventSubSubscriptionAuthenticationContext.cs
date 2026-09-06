using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// Contains information needed to determine how to authenticate requests made for a specific <see cref="EventSubSubscriptionSpecification"/>.
/// </summary>
public abstract record EventSubSubscriptionAuthenticationContext
{
    public sealed record None : EventSubSubscriptionAuthenticationContext
    {
        public readonly static None Instance = new();
    }
    public sealed record UserAuthorized : EventSubSubscriptionAuthenticationContext
    {
        public required TwitchIdentity.User Identity { get; init; }
        public IReadOnlySet<Scope> ValidScopes { get; init; }
            = ImmutableHashSet<Scope>.Empty;
        internal UserWithScopesAuthenticationContext ToUserWithScopesAuthenticationContext()
            => new() { Identity = Identity, ValidScopes = ValidScopes };
        internal UserSupportingPriorAuthorizationAuthenticationContext ToUserSupportingPriorAuthorizationAuthenticationContext(bool usePriorAuthorization)
            => new() { Identity = Identity, ValidScopes = ValidScopes, UsePriorAuthorization = usePriorAuthorization };
    }

    public sealed record ClientAuthorized : EventSubSubscriptionAuthenticationContext
    {
        public TwitchIdentity.Client Identity { get; init; }
            = TwitchIdentity.Client.Default;
        internal TwitchRequestAuthenticationContext<TwitchIdentity.Client> ToClientAuthenticationContext()
            => new() { Identity = Identity };
    }
}
