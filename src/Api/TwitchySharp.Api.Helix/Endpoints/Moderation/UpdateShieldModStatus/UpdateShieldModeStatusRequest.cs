using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Activates or deactivates the broadcaster's Shield Mode.
/// </summary>
/// <remarks>
/// <para>
/// Requires a user access token with <see cref="Scope.ModeratorManageShieldMode"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ModeratorManageShieldMode"/> for the <see cref="ModeratorId"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-shield-mode-status">Update Shield Mode Status</see> for more information.
/// </remarks>
public record UpdateShieldModeStatusRequest
    : TwitchHelixRequest<UpdateShieldModeStatusResponseContent>,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    protected override string Path => "/moderation/shield_mode";
    public override HttpMethod Method => HttpMethod.Put;
    private UserSupportingPriorAuthorizationAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorManageShieldMode)
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
    public override object? ContentObject => ShieldModeStatus;

    /// <summary>
    /// The user id of the broadcaster (channel) to update Shield Mode status for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

    /// <summary>
    /// The Shield Mode status to update to.
    /// </summary>
    public required UpdateShieldModeStatusRequestData ShieldModeStatus { get; init; }
}

/// <summary>
/// Data used to set the status of Shield Mode on a channel.
/// </summary>
public record UpdateShieldModeStatusRequestData
{
    /// <summary>
    /// Determines whether to activate or deactivate Shield Mode. 
    /// </summary>
    /// <remarks>
    /// Set to <see langword="true"/> to activate Shield Mode; otherwise, <see langword="false"/> to deactivate Shield Mode.
    /// </remarks>
    public required bool IsActive { get; init; }
}
