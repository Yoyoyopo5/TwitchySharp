using System.Collections.Immutable;
using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Helix.Predictions;
/// <summary>
/// Creates a Channel Points Prediction.
/// </summary>
/// <remarks>
/// The prediction runs as soon as it's created.
/// The broadcaster may run only one prediction at a time.
/// <para>
/// Requires a user access token that includes <see cref="Scope.ChannelManagePredictions"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-prediction">Create Prediction</see> for more information.
/// </remarks>
public record CreatePredictionRequest
    : TwitchHelixRequest<CreatePredictionResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/predictions";
    public override HttpMethod Method => HttpMethod.Post;
    private UserWithScopesAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(Prediction.BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManagePredictions)
    };
    public UserWithScopesAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    public override object? ContentObject => Prediction;

    /// <summary>
    /// The new prediction to create and start.
    /// </summary>
    public required CreatePredictionRequestData Prediction { get; init; }
}

/// <summary>
/// Data used to create a new chat prediction.
/// </summary>
public record CreatePredictionRequestData
{
    /// <summary>
    /// The user id of the broadcaster (channel) to create the prediction for.
    /// This must be the same user that created the user access token in the <see cref="CreatePredictionRequest"/>.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The question that the prediction is asking.
    /// This is limited to a maximum of 45 characters.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The list of possible outcomes that the viewers may choose from.
    /// This list must contain a minimum of 2 choices and up to a maximum of 10 choices.
    /// </summary>
    public required CreatePredictionOutcome[] Outcomes { get; init; }
    /// <summary>
    /// The length of time that the prediction will be active for.
    /// The minimum is 30 seconds and the maximum is 1800 seconds (30 minutes).
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan PredictionWindow { get; init; }
}
