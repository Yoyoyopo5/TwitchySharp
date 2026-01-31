using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Removes a moderator from the broadcaster's chat room.
/// </summary>
/// <remarks>
/// <b>Rate Limits:</b> A broadcaster may remove a maximum of 10 moderators within a 10-second window.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageModerators"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#remove-channel-moderator">Remove Channel Moderator</see> for more information.
/// </remarks>
public record RemoveChannelModeratorRequest
    : TwitchHelixRequest<RemoveChannelModeratorResponse>
{
    protected override string Path => "/moderation/moderators";
    public override HttpMethod Method => HttpMethod.Delete;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(BroadcasterId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ChannelManageModerators ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("user_id", UserId);

    /// <summary>
    /// The user id of the broadcaster (channel) to remove a moderator for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the moderator to remove from the broadcaster's channel.
    /// </summary>
    public required UserId UserId { get; init; }
}
