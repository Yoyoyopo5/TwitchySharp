using System.Collections.Immutable;
using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets a list of channels that the specified user has moderator privileges in.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.UserReadModeratedChannels"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-moderated-channels">Get Moderated Channels</see> for more information.
/// </remarks>
public record GetModeratedChannelsRequest
    : TwitchHelixRequest<GetModeratedChannelsResponse>, IPageableRequest
{
    protected override string Path => "/moderation/channels";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(UserId),
        ValidScopes = ImmutableHashSet.Create(Scope.UserReadModeratedChannels)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("user_id", UserId)
            .Add("after", After?.Value)
            .Add("first", First?.ToString());

    /// <summary>
    /// The user id of the user to get moderated channels for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token.
    /// </remarks>
    public required UserId UserId { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }

    /// <remarks>
    /// Minimum page size is 1 item per page and the maximum is 100.
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; init; }
}
