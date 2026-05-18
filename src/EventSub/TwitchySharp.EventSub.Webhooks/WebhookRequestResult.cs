namespace TwitchySharp.EventSub.Webhooks.Http;

public abstract record WebhookRequestResult
{
    public sealed record CallbackVerification : WebhookRequestResult
    {
        public required string Challenge { get; init; }
    }
    public sealed record Error : WebhookRequestResult
    {
        public Infrastructure.Functional.Error? InnerError { get; init; }
    };
    public sealed record Notification : WebhookRequestResult;
    public sealed record Revocation : WebhookRequestResult;
}
