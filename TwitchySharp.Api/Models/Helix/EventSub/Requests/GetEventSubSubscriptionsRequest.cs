using System.Net.Http;
using TwitchySharp.Api.Models.Helix.EventSub.Models.Transports;
using TwitchySharp.Api.Models.Helix.EventSub.Responses;
using TwitchySharp.Api.Models.Shared;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.Api.Models.Helix.EventSub.Requests;
/// <summary>
/// Gets a list of EventSub subscriptions that an app created.
/// </summary>
/// <remarks>
/// If using <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/>, requires an app access token.
/// If using <see cref="WebsocketSubscriptionTransport"/>, requires a user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-eventsub-subscriptions">Get EventSub Subscriptions</see> for more information.
/// </remarks>
public record GetEventSubSubscriptionsRequest
    : TwitchHelixRequest<GetEventSubSubscriptionsResponse>
{
    /// <param name="clientId">The client id of the application to get EventSub subscriptions for.</param>
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
    public GetEventSubSubscriptionsRequest(
        string clientId,
        string accessToken,
        EventSubSubscriptionStatus? status = null,
        EventSubSubscriptionType? type = null,
        string? userId = null,
        string? after = null
        )
        : base(
            "/eventsub/subscriptions",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("status", status?.Value)
                .Add("type", type?.Type)
                .Add("user_id", userId)
                .Add("after", after)
            )
    {
        Method = HttpMethod.Get;
    }
}
