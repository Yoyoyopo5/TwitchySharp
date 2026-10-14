using System.Net;
using System.Text.Json.Serialization;
using TwitchySharp.Infrastructure.Functional;
using System.Text.RegularExpressions;
using System.Text.Json;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Helix.EventSub;
/// <summary>
/// Creates an EventSub subscription.
/// </summary>
/// <remarks>
/// <para>
/// If you use the <see cref="WebsocketSubscriptionTransport"/>, an access token must be a user access token with the required <see cref="Scope"/> for the subscription type.
/// </para>
/// <para>
/// If you use the <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/>, the access token must be an app access token.
/// If the subscription type requires a user access token authorization, the user must have created a user access token with the required <see cref="Scope"/> for this application (i.e., for this application's client id).
/// It is not required to send this user access token in the request.
/// </para>
/// <para>
/// The identity and scopes for the request are automatically determined based on the <see cref="Subscription"/> transport and type.
/// You do not need to manually configure them unless you need to override the default behavior.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-eventsub-subscription">Create EventSub Subscription</see> for more information.
/// </remarks>
public partial record CreateEventSubSubscriptionRequest
    : TwitchHelixRequest<CreateEventSubSubscriptionResponseContent>,
    IAuthenticatedTwitchRequest<ITwitchRequestAuthenticationContext<TwitchIdentity>>
{
    private partial record ConflictResponseContent
    {
        public static async ValueTask<Result<ConflictResponseContent>> CreateFromResponse(HttpResponseMessage response, CancellationToken ct)
        {
            using HttpResponseMessage r = response;
            try
            {
                return await JsonSerializer.DeserializeAsync<ConflictResponseContent>(await response.Content.ReadAsStreamAsync(ct), JsonConfig.ApiOptions, ct) is not { } result
                    ? new Error("409 error content was JSON null literal.")
                    : result;
            }
            catch (Exception ex)
            {
                return new ExceptionError(ex);
            }
        }

        /* Example response content:
         {
            "error": "Conflict",
            "status": 409,
            "message": "subscription already exists; id=c3e419d8-4f1d-403c-80f5-1639a672a81d"
         }
         */

        public string? Message { get; init; }

        // So, for whatever reason they have the id ad-hoc in the message string.
        // Twitch doesn't guarantee their error message shapes, so be prepared for this to change.
        [GeneratedRegex(@"(?<=id=).*$")]
        private static partial Regex SubscriptionIdRegex();

        public Result<EventSubSubscriptionId> GetSubscriptionId()
        {
            return Message is null
                ? new Error("Twitch reports an existing EventSub subscription for the type and transport, but did not include an error message, or the error message was in an unexpected format.")
                : SubscriptionIdRegex().Matches(Message).FirstOrDefault() is not Match m
                ? new Error("Unable to parse existing the EventSub subscription's id from the 409 error message.")
                : new EventSubSubscriptionId(m.Value);
        }
    }
    public record SubscriptionAlreadyExistsError(EventSubSubscriptionId Id)
        : Error($"A subscription already exists with the specified type and condition.");

    protected override string Path => "/eventsub/subscriptions";
    public override HttpMethod Method => HttpMethod.Post;
    public ITwitchRequestAuthenticationContext<TwitchIdentity> AuthenticationContext
    {
        get => field ?? Subscription.AuthenticationContext;
        init;
    }
    public override object? ContentObject => (CreateEventSubSubscriptionRequestData)Subscription;

    /// <summary>
    /// The subscription to create.
    /// </summary>
    public required EventSubSubscriptionSpecification Subscription { get; init; }

    public override ValueTask<TwitchResponse<CreateEventSubSubscriptionResponseContent>> CreateResponse(HttpResponseMessage response, CancellationToken ct)
        => response.StatusCode switch
        {
            HttpStatusCode.Conflict => ConflictResponseContent.CreateFromResponse(response, ct)
                .BindAsync(content => content.GetSubscriptionId())
                .MatchAsync(
                    e => new TwitchResponse<CreateEventSubSubscriptionResponseContent>(e),
                    id => new TwitchResponse<CreateEventSubSubscriptionResponseContent>(new SubscriptionAlreadyExistsError(id))
                ),
            _ => base.CreateResponse(response, ct)
        };
}

internal record CreateEventSubSubscriptionRequestData
{
    [JsonPropertyName("type")]
    public required EventSubSubscriptionTypeName Type { get; init; }
    [JsonPropertyName("version")]
    public required EventSubSubscriptionTypeVersion Version { get; init; }
    [JsonPropertyName("condition")]
    public required IReadOnlyDictionary<ConditionKey, object> Condition { get; init; }
    [JsonPropertyName("transport")]
    public required EventSubSubscriptionTransportSpecification Transport { get; init; }
    [JsonPropertyName("is_batching_enabled")]
    public bool? IsBatchingEnabled => Type == EventSubSubscriptionTypeName.DropEntitlementGrant
        ? true : null;

    public static explicit operator CreateEventSubSubscriptionRequestData(EventSubSubscriptionSpecification subscription)
        => new()
        {
            Type = subscription.Type.Type.Type,
            Version = subscription.Type.Type.Version,
            Condition = subscription.Type.Condition.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
            Transport = subscription.Transport
        };
}
