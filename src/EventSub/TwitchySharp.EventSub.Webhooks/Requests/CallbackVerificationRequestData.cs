namespace TwitchySharp.EventSub.Webhooks.Requests;

internal record CallbackVerificationRequestData : WebhookRequestData
{
    public required string Challenge { get; init; }
}
