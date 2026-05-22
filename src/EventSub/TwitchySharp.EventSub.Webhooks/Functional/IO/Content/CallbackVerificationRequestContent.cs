namespace TwitchySharp.EventSub.Webhooks.Functional;

/// <summary>
/// The content of an EventSub webhook callback request.
/// </summary>
public record CallbackVerificationRequestContent : WebhookRequestContent
{
    public required string Challenge { get; init; }
}
