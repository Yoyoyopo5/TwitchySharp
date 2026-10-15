using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Bits;

/// <summary>
/// Gets a list of custom Power-ups that the specified broadcaster created.
/// </summary>
/// <remarks>
/// <para>
/// <b>NOTE:</b> A channel may offer a maximum of 50 custom Power-ups,
/// which includes both enabled and disabled Power-ups.
/// </para>
/// <para>
/// Requires a user access token that includes <see cref="Scope.BitsRead"/>.
/// </para>
/// <para>
/// See <see href="https://dev.twitch.tv/docs/api/reference#get-custom-power-up">Get Custom Power-Up</see> for more information.
/// </para>
/// </remarks>
public record GetCustomPowerUpsRequest
    : TwitchHelixRequest<GetCustomPowerUpsResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    public override HttpMethod Method => HttpMethod.Get;
    protected override string Path => "/bits/custom_power_ups";
    protected override HttpQueryParameters? QueryParameters =>
        new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("id", PowerUpIds?.Select(x => x.ToString()));

    public UserWithScopesAuthenticationContext AuthenticationContext
    {
        get => field ?? new UserWithScopesAuthenticationContext()
        {
            Identity = new TwitchIdentity.User(BroadcasterId),
            ValidScopes = ImmutableHashSet.Create(Scope.BitsRead)
        };
        init;
    }

    /// <summary>
    /// The user id of the boradcaster (channel) to get custom Bits power-ups for.
    /// </summary>
    /// <remarks>
    /// This is the authorizing user.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The id(s) of the power-ups to get.
    /// </summary>
    /// <remarks>
    /// There can be a maximum of 50 unique ids.
    /// Duplicate ids are ignored.
    /// </remarks>
    public IEnumerable<CustomBitsPowerUpId>? PowerUpIds { get; init; }
}
