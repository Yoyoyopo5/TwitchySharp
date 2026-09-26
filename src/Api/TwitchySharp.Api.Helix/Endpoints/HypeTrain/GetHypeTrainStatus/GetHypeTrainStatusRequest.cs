using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.HypeTrain;
/// <summary>
/// Get the status of a Hype Train for the specified broadcaster.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadHypeTrain"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-hype-train-status">Get Hype Train Status</see> for more information.
/// </remarks>
public record GetHypeTrainStatusRequest
    : TwitchHelixRequest<GetHypeTrainStatusResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/hypetrain/status";
    public override HttpMethod Method => HttpMethod.Get;
    private UserWithScopesAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelReadHypeTrain)
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
    /// The user id of the broadcaster (channel) to get the Hype Train status for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }
}
