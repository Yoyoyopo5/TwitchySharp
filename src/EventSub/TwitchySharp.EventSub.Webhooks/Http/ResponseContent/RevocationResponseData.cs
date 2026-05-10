namespace TwitchySharp.EventSub.Webhooks.Responses;

public record RevocationResponseData : WebhookResponseData
{
    public RevocationResponseData()
    {
        StatusCode = 204;
    }
}
