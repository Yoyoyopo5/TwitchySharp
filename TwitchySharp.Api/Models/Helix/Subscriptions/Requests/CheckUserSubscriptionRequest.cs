using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Subscriptions.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Subscriptions.Requests;
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
    /// <param name="broadcasterId">The user id of the broadcaster that the subscription is to.</param>
    /// <param name="userId">
    /// The id of the user to get the subscription for.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    public CheckUserSubscriptionRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string userId
        ) : base(
            "/subscriptions/user",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("user_id", userId)
            )
    {
        Method = HttpMethod.Get;
    }
}
