using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Activates or deactivates the broadcaster's Shield Mode.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageShieldMode"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-shield-mode-status">Update Shield Mode Status</see> for more information.
/// </remarks>
public record UpdateShieldModeStatusRequest
    : TwitchHelixRequest<UpdateShieldModeStatusResponse>
{
    protected override string Path => "/moderation/shield_mode";
    public override HttpMethod Method => HttpMethod.Put;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(ModeratorId);
    public override IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ModeratorManageShieldMode);
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
