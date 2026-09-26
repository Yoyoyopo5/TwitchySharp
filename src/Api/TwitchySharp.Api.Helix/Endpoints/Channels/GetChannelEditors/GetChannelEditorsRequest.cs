using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Gets the broadcaster's list of editors.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.ChannelReadEditors"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-editors">Get Channel Editors</see> for more information.
/// </remarks>
public record GetChannelEditorsRequest
    : TwitchHelixRequest<GetChannelEditorsResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/channels/editors";
    public override HttpMethod Method => HttpMethod.Get;
    private UserWithScopesAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelReadEditors)
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
    /// The user id of the broadcaster that owns the channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// Requires <see cref="Scope.ChannelReadEditors"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }
}
