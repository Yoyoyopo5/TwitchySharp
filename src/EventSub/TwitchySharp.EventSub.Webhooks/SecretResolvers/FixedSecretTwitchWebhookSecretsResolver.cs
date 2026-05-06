namespace TwitchySharp.EventSub.Webhooks.SecretResolvers;

public class FixedSecretTwitchWebhookSecretsResolver(string secret) : ITwitchEventSubWebhookSecretsResolver
{
    public ValueTask<string> GetSecret(EventSubWebhookRequestHeader requestHeaders, string body, CancellationToken ct = default)
        => ValueTask.FromResult(secret);
    public ValueTask<string> GetSecret(EventSubWebhookRequestHeader requestHeaders, Stream body, CancellationToken ct = default)
        => ValueTask.FromResult(secret);
}
