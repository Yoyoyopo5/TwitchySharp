using System.Collections.Immutable;
using TwitchySharp.Api.Helix.Channels;
using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Gets the list of users that are connected to the broadcaster's chat session.
/// </summary>
/// <remarks>
/// To determine whether a user is a moderator or VIP, use the <see cref="GetModeratorsRequest"/> and <see cref="GetVipsRequest"/> endpoints.
/// You can check the roles of up to 100 users.
/// <para>
/// <b>NOTE:</b> There is a delay between when users join and leave a chat and when the list is updated accordingly.
/// The list is usually not very accurate (in real-time) for this reason.
/// Often a user will not be in this list when they are active in chat.
/// </para>
/// <para>
/// Requires a user access token with <see cref="Scope.ModeratorReadChatters"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ModeratorReadChatters"/> for the <see cref="ModeratorId"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-chatters">Get Chatters</see> for more information.
/// </remarks>
public record GetChattersRequest
    : TwitchHelixRequest<GetChattersResponseContent>, IForwardPageableRequest,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    protected override string Path => "/chat/chatters";
    public override HttpMethod Method => HttpMethod.Get;
    private UserSupportingPriorAuthorizationAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorReadChatters)
    };
    public UserSupportingPriorAuthorizationAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId)
            .Add("first", First?.ToString())
            .Add("after", After?.Value);

    /// <summary>
    /// The user id of the broadcaster whose chatters you want to get.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster OR one of the broadcaster's moderators.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// Requires <see cref="Scope.ModeratorReadChatters"/>.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 1,000.
    /// The default is 100.
    /// </remarks>
    public PaginationAmount? First { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }
}
