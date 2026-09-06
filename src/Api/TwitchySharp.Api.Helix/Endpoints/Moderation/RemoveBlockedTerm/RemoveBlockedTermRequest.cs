using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Removes the word or phrase from the broadcaster's list of blocked terms.
/// </summary>
/// <remarks>
/// <para>
/// Requires a user access token with <see cref="Scope.ModeratorManageBlockedTerms"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ModeratorManageBlockedTerms"/> for the <see cref="ModeratorId"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#remove-blocked-term">Remove Blocked Term</see> for more information.
/// </remarks>
public record RemoveBlockedTermRequest
    : TwitchHelixRequest<RemoveBlockedTermResponseContent>,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    protected override string Path => "/moderation/blocked_terms";
    public override HttpMethod Method => HttpMethod.Delete;
    private UserSupportingPriorAuthorizationAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorManageBlockedTerms)
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
            .Add("id", BlockedTermId);

    /// <summary>
    /// The user id of the broadcaster (channel) to remove a blocked term from.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

    /// <summary>
    /// The id of the blocked term to remove.
    /// </summary>
    public required AutomodBlockedTermId BlockedTermId { get; init; }

    public override Func<Stream, CancellationToken, ValueTask<RemoveBlockedTermResponseContent>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new RemoveBlockedTermResponseContent());
}
