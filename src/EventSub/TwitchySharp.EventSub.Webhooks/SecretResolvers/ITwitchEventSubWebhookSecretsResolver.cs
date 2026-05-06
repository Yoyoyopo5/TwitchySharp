namespace TwitchySharp.EventSub.Webhooks.SecretResolvers;

public interface ITwitchEventSubWebhookSecretsResolver
{
    ValueTask<string> GetSecret(EventSubWebhookRequestHeader requestHeaders, string body, CancellationToken ct = default);
    ValueTask<string> GetSecret(EventSubWebhookRequestHeader requestHeaders, Stream body, CancellationToken ct = default);
}
