using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Subscriptions;
/// <summary>
/// Checks whether the user subscribes to the broadcaster’s channel.
/// </summary>
/// <remarks>
/// <para>
/// Requires a user access token that includes <see cref="Scope.UserReadSubscriptions"/>.
/// A Twitch extension may use an app access token if the broadcaster has granted <see cref="Scope.UserReadSubscriptions"/> from within the Twitch Extensions manager.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#check-user-subscription">Check User Subscription</see> for more information.
/// </remarks>
public record CheckUserSubscriptionRequest
    : TwitchHelixRequest<CheckUserSubscriptionResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.UserReadSubscriptions"/>, or an app access token if the application is an extension and the broadcaster has granted <see cref="Scope.UserReadSubscriptions"/> from within the Twitch Extensions manager.</param>
    /// <param name="parameters">The request parameters.</param>
    public CheckUserSubscriptionRequest(
        ClientId clientId,
        AccessToken accessToken,
        CheckUserSubscriptionRequestParameters parameters
        ) : base(
            "/subscriptions/user",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("user_id", parameters.UserId)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="CheckUserSubscriptionRequest"/>.
/// </summary>
public record CheckUserSubscriptionRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster that the subscription is to.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The id of the user to get the subscription for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token.
    /// </remarks>
    public required UserId UserId { get; set; }
}
