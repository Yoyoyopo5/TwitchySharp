namespace TwitchySharp.EventSub.Webhooks.Responses;

public abstract record WebhookResponseData
{
    public int StatusCode { get; init; } = 200;
}
