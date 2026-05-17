namespace TwitchySharp.EventSub.Webhooks.Http;

public record RevocationResponse : WebhookResponse
{
    public RevocationResponse()
    {
        StatusCode = 204;
    }
}
