using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Deletes a specified conduit.
/// </summary>
/// <remarks>
/// Note that it may take some time for Eventsub subscriptions on a deleted conduit to show as disabled when calling <see cref="GetEventSubSubscriptionsRequest"/>.
/// <br/>
/// Requires an app access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-conduit">Delete Conduit</see> for more information.
/// </remarks>
public record DeleteConduitRequest
    : TwitchHelixRequest<DeleteConduitResponse>
{
    protected override string Path => "/eventsub/conduits";
    public override HttpMethod Method => HttpMethod.Delete;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("id", ConduitId);

    /// <summary>
    /// The id of the conduit you want to delete.
    /// </summary>
    public required ConduitId ConduitId { get; init; }

    protected override ValueTask<DeleteConduitResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
        => ValueTask.FromResult(new DeleteConduitResponse());
}
