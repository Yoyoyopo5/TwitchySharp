using System.Collections.Immutable;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// <b>BETA</b> Allows a user to update the assigned slot for a particular user within the active Guest Star session.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-guest-star-slot">Update Guest Star Slot</see> for more information.
/// </remarks>
public record UpdateGuestStarSlotRequest
    : TwitchHelixRequest<UpdateGuestStarSlotResponse>
{
    protected override string Path => "/guest_star/slot";
    public override HttpMethod Method => HttpMethod.Patch;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManageGuestStar, Scope.ModeratorManageGuestStar)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId)
            .Add("session_id", SessionId)
            .Add("source_slot_id", SourceSlotId)
            .Add("destination_slot_id", DestinationSlotId?.ToString());

    /// <summary>
    /// The user id of the broadcaster hosting the Guest Star session.
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
    /// The id of the Guest Star session in which to update a slot.
    /// </summary>
    public required GuestStarSessionId SessionId { get; init; }

    /// <summary>
    /// The id of the slot containing the user you want to move.
    /// </summary>
    public required GuestStarSlotId SourceSlotId { get; init; }

    /// <summary>
    /// The id of the slot to move the source user to.
    /// </summary>
    /// <remarks>
    /// If the destination slot is occupied, the user assigned will be swapped into the source slot.
    /// </remarks>
    public GuestStarSlotId? DestinationSlotId { get; init; }

    protected override ValueTask<UpdateGuestStarSlotResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
        => ValueTask.FromResult(new UpdateGuestStarSlotResponse());
}
