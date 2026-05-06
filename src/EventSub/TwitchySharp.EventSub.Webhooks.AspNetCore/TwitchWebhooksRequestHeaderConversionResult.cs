namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

internal record TwitchWebhooksRequestHeaderConversionResult
{
    public required EventSubWebhookRequestHeader ConvertedHeader { get; init; }
    public IEnumerable<string> MissingHeaders { get; init; } = [];
}