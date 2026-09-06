using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.ChannelPoints;
/// <summary>
/// Deletes a custom reward that the broadcaster created.
/// </summary>
/// <remarks>
/// The app used to create the reward is the only app that may delete it.
/// If the reward's redemption status is <see cref="RewardRedemptionStatus.Unfulfilled"/> at the time the reward is deleted,
/// its redemption status is marked as <see cref="RewardRedemptionStatus.Fulfilled"/>.
/// <br/>
/// Requires a user access token with <see cref="Scope.ChannelManageRedemptions"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-custom-reward">Delete Custom Reward</see> for more information.
/// </remarks>
public record DeleteCustomRewardRequest
    : TwitchHelixRequest<DeleteCustomRewardResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/channel_points/custom_rewards";
    public override HttpMethod Method => HttpMethod.Delete;
    private UserWithScopesAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManageRedemptions)
    };
    public UserWithScopesAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("id", RewardId);

    /// <summary>
    /// The user id of the broadcaster that created the custom reward.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token for the request.
    /// Requires <see cref="Scope.ChannelManageRedemptions"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The id of the custom reward to delete.
    /// </summary>
    public required RewardId RewardId { get; init; }

    public override Func<Stream, CancellationToken, ValueTask<DeleteCustomRewardResponseContent>>? ConvertResponseContent { get; init; } =
        (_, _) => ValueTask.FromResult(new DeleteCustomRewardResponseContent());
}
