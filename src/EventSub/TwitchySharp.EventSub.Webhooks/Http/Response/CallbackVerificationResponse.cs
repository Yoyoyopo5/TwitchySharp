namespace TwitchySharp.EventSub.Webhooks.Http;

public sealed record CallbackVerificationResponse : WebhookResponse
{
    public required string Challenge { get; init; }
    public int ChallengeLength => Challenge.Length;
}
