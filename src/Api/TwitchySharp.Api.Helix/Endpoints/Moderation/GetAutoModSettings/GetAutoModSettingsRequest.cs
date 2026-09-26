using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets the broadcaster's AutoMod settings.
/// </summary>
/// <remarks>
/// The settings are used to automatically block inappropriate or harassing messages from appearing in the broadcaster's chat room.
/// <para>
/// Requires a user access token with <see cref="Scope.ModeratorReadAutomodSettings"/> or <see cref="Scope.ModeratorManageAutomodSettings"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ModeratorReadAutomodSettings"/> or <see cref="Scope.ModeratorManageAutomodSettings"/> for the <see cref="ModeratorId"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-automod-settings">Get AutoMod Settings</see> for more information.
/// </remarks>
public record GetAutoModSettingsRequest
    : TwitchHelixRequest<GetAutoModSettingsResponseContent>,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    protected override string Path => "/moderation/automod/settings";
    public override HttpMethod Method => HttpMethod.Get;
    private UserSupportingPriorAuthorizationAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorReadAutomodSettings)
    };
    public UserSupportingPriorAuthorizationAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId);

    /// <summary>
    /// The user id of the broadcaster (channel) to get AutoMod settings for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId ModeratorId { get; init; }
}
