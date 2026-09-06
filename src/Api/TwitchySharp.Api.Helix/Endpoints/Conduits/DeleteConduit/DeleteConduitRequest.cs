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
    : TwitchHelixRequest<DeleteConduitResponseContent>,
    IAuthenticatedTwitchRequest<TwitchRequestAuthenticationContext<TwitchIdentity.Client>>
{
    protected override string Path => "/eventsub/conduits";
    public override HttpMethod Method => HttpMethod.Delete;
    public TwitchRequestAuthenticationContext<TwitchIdentity.Client> AuthenticationContext { get; init; }
        = TwitchRequestAuthenticationContext.Default;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("id", ConduitId);

    /// <summary>
    /// The id of the conduit you want to delete.
    /// </summary>
    public required ConduitId ConduitId { get; init; }

    public override Func<Stream, CancellationToken, ValueTask<DeleteConduitResponseContent>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new DeleteConduitResponseContent());
}
