namespace TwitchySharp.EventSub.Webhooks.Http;

public record InternalErrorResponse : WebhookResponse
{
    public InternalErrorResponse()
    {
        StatusCode = 200; // We return OK here so Twitch doesn't revoke the webhook, and doesn't need to know we had an error.
    }
}
