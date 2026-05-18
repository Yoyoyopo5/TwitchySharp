namespace TwitchySharp.EventSub.Webhooks.Http;

internal record CallbackVerificationRequestContent : WebhookRequestContent
{
    public required string Challenge { get; init; }
}
