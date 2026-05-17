namespace TwitchySharp.EventSub.Webhooks.Http;

public record CallbackVerificationResponse : WebhookResponse
{
    public required string Challenge { get; init; }
    public int ChallengeLength => Challenge.Length;
}
