using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Updates the color used for the user's name in chat.
/// </summary>
/// <remarks>
/// <para>
/// Requires a user access token that includes <see cref="Scope.UserManageChatColor"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-user-chat-color">Update User Chat Color</see> for more information.
/// </remarks>
public record UpdateUserChatColorRequest
    : TwitchHelixRequest<UpdateUserChatColorResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/chat/color";
    public override HttpMethod Method => HttpMethod.Put;
    private UserWithScopesAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(UserId),
        ValidScopes = ImmutableHashSet.Create(Scope.UserManageChatColor)
    };
    public UserWithScopesAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("user_id", UserId)
            .Add("color", Color);

    /// <summary>
    /// The user id of the user whose color to change.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// Requires <see cref="Scope.UserManageChatColor"/>.
    /// </remarks>
    public required UserId UserId { get; init; }

    /// <summary>
    /// The color to use for the user's name in chat.
    /// </summary>
    public required ChatColor Color { get; init; }

    public override Func<Stream, CancellationToken, ValueTask<UpdateUserChatColorResponseContent>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new UpdateUserChatColorResponseContent());
}
