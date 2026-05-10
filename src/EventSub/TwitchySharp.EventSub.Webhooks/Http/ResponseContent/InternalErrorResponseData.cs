namespace TwitchySharp.EventSub.Webhooks.Responses;

internal record InternalErrorResponseData : WebhookResponseData
{
    public InternalErrorResponseData()
    {
        StatusCode = 200; // We return OK here so Twitch doesn't revoke the webhook, and doesn't need to know we had an error.
    }
}
