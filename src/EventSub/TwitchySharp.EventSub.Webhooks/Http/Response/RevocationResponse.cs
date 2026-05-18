namespace TwitchySharp.EventSub.Webhooks.Http;

public sealed record RevocationResponse : WebhookResponse
{
    public RevocationResponse()
    {
        StatusCode = 204;
    }
}
