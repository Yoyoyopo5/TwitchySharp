using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Sends a Shoutout to the specified broadcaster. See <see href="https://help.twitch.tv/s/article/shoutouts">Shoutouts</see>.
/// </summary>
/// <remarks>
/// A broadcaster may send a Shoutout once every 2 minutes. They may send the same broadcaster a Shoutout once every 60 minutes.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageShoutouts"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-a-shoutout">Send a Shoutout</see> for more information.
/// </remarks>
public record SendShoutoutRequest
    : TwitchHelixRequest<SendShoutoutResponse>
{
    protected override string Path => "/chat/shoutouts";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(ModeratorId);
    public override IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ModeratorManageShoutouts);
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("from_broadcaster_id", FromBroadcasterId)
            .Add("to_broadcaster_id", ToBroadcasterId)
            .Add("moderator_id", ModeratorId);

    /// <summary>
    /// The user id of the broadcaster that's sending the shoutout.
    /// </summary>
    public required UserId FromBroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster that's receiving the shoutout.
    /// </summary>
    public required UserId ToBroadcasterId { get; init; }

    /// <summary>
    /// The user id of the moderator (or the broadcaster) to send the shoutout on behalf of.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// Requires <see cref="Scope.ModeratorManageShoutouts"/>.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

    protected override ValueTask<SendShoutoutResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
        => ValueTask.FromResult(new SendShoutoutResponse());
}
