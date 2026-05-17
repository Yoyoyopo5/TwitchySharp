namespace TwitchySharp.EventSub.Webhooks.Http;

public abstract record WebhookResponse
{
    public int StatusCode { get; init; } = 200;
}
