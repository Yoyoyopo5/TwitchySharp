using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets the broadcaster's Shield Mode activation status.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadShieldMode"/> or <see cref="Scope.ModeratorManageShieldMode"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-shield-mode-status">Get Shield Mode Status</see> for more information.
/// </remarks>
public record GetShieldModeStatusRequest
    : TwitchHelixRequest<GetShieldModeStatusResponse>
{
    protected override string Path => "/moderation/shield_mode";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(ModeratorId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ModeratorReadShieldMode, Scope.ModeratorManageShieldMode ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId);

    /// <summary>
    /// The user id of the broadcaster (channel) to get Shield Mode status for.
    /// </summary>
    public required UserId BroadcasterId { get; set; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }
}
