using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Subscriptions;
/// <summary>
/// Checks whether the user subscribes to the broadcaster’s channel.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#check-user-subscription">check user subscription</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadSubscriptions"/>.
/// <para>
/// A Twitch extension may use an app access token if the broadcaster has granted <see cref="Scope.UserReadSubscriptions"/> from within the Twitch Extensions manager.
/// </para>
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.UserReadSubscriptions"/>, or an app access token if the application is an extension and the broadcaster has granted <see cref="Scope.UserReadSubscriptions"/> from within the Twitch Extensions manager.</param>
/// <param name="broadcasterId">The user id of the broadcaster that the subscription is to.</param>
/// <param name="userId">
/// The id of the user to get the subscription for.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
public class CheckUserSubscriptionRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string userId
    )
    : HelixApiRequest<CheckUserSubscriptionResponse>(
        "/subscriptions/user" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("user_id", userId),
        clientId,
        accessToken
        );
