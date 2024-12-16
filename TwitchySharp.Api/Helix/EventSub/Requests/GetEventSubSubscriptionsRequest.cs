using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minerals.StringCases;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.EventSub;
/// <summary>
/// Gets a list of EventSub subscriptions that the <paramref name="clientId"/> created.
/// </summary>
/// <remarks>
/// If using <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/>, requires an app access token.
/// If using <see cref="WebsocketSubscriptionTransport"/>, requires a user access token.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">
/// If using <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/>, an app access token.
/// If using <see cref="WebsocketSubscriptionTransport"/>, a user access token.
/// </param>
/// <param name="status">Specify this parameter to filter the returned list by subscription status.</param>
/// <param name="type">
/// Specify this parameter to filter the returned list by subscription type.
/// Note that this only filters by subscription type <b>name</b>, not version.
/// </param>
/// <param name="userId">Specify this parameter to filter the returned list by a specific user. 
/// Only subscriptions that were created for this user are returned.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> property in the response contains the cursor's value.
/// </param>
public class GetEventSubSubscriptionsRequest(
    string clientId, 
    string accessToken, 
    EventSubSubscriptionStatus? status = null,
    EventSubSubscriptionType? type = null, 
    string? userId = null, 
    string? after = null
    )
    : HelixApiRequest<GetEventSubSubscriptionsResponse>(
        "/eventsub/subscriptions" +
        new HttpQueryParameters()
            .Add("status", status?.ToString().ToSnakeCase())
            .Add("type", type?.Type)
            .Add("user_id", userId)
            .Add("after", after),
        clientId,
        accessToken
        );
