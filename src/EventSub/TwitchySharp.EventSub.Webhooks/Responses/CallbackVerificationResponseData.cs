namespace TwitchySharp.EventSub.Webhooks.Responses;

public record CallbackVerificationResponseData : WebhookResponseData
{
    public required string Challenge { get; init; }
    public int ChallengeLength => Challenge.Length;
}
