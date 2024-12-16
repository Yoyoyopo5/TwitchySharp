using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub;
/// <summary>
/// Deletes an EventSub subscription.
/// See <see cref="https://dev.twitch.tv/docs/api/reference/#delete-eventsub-subscription">delete EventSub subscription</see> for more information.
/// </summary>
/// <remarks>
/// If you use the <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/> to receive the subscription notifications, the <paramref name="accessToken"/> must be an app access token.
/// <br/>
/// If you use the <see cref="WebsocketSubscriptionTransport"/>, the <paramref name="accessToken"/> must be a user access token. No particular <see cref="Scope"/> is required.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">
/// If using <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/>, an app access token. 
/// If using <see cref="WebsocketSubscriptionTransport"/>, a user access token.
/// </param>
/// <param name="subscriptionId">The id of the subscription to delete.</param>
public class DeleteEventSubSubscriptionRequest(string clientId, string accessToken, string subscriptionId)
    : HelixApiRequest<DeleteEventSubSubscriptionResponse>(
        "/eventsub/subscriptions" +
        new HttpQueryParameters()
            .Add("id", subscriptionId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Delete;
}
