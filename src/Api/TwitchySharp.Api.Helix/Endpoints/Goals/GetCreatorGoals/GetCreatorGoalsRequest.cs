using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Goals;
/// <summary>
/// Gets the broadcaster's list of active goals.
/// </summary>
/// <remarks>
/// Use this to get the current progress of each goal.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelReadGoals"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-creator-goals">Get Creator Goals</see> for more information.
/// </remarks>
public record GetCreatorGoalsRequest
    : TwitchHelixRequest<GetCreatorGoalsResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/goals";
    public override HttpMethod Method => HttpMethod.Get;
    private UserWithScopesAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelReadGoals)
    };
    public UserWithScopesAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);

    /// <summary>
    /// The user id of the broadcaster (channel) to get goals for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }
}
