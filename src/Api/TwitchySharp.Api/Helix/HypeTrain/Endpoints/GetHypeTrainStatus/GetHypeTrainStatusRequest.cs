using System.Collections.Immutable;
using System.Net.Http;

namespace TwitchySharp.Api.Helix.HypeTrain;
/// <summary>
/// Get the status of a Hype Train for the specified broadcaster.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadHypeTrain"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-hype-train-status">Get Hype Train Status</see> for more information.
/// </remarks>
public record GetHypeTrainStatusRequest
    : TwitchHelixRequest<GetHypeTrainStatusResponse>
{
    protected override string Path => "/hypetrain/status";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelReadHypeTrain)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);

    /// <summary>
    /// The user id of the broadcaster (channel) to get the Hype Train status for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }
}
