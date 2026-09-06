using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Gets the channel's stream key.
/// </summary>
/// <remarks>
/// <para>
/// Requires a user access token that includes <see cref="Scope.ChannelReadStreamKey"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-stream-key">Get Stream Key</see> for more information.
/// </remarks>
public record GetStreamKeyRequest
    : TwitchHelixRequest<GetStreamKeyResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/streams/key";
    public override HttpMethod Method => HttpMethod.Get;
    private UserWithScopesAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelReadStreamKey)
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
    /// The user id of the broadcaster (channel) to get the stream key for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }
}
