namespace TwitchySharp.EventSub.Webhooks.Crypto;

public static partial class SecretResolvers
{
    /// <summary>
    /// Creates a webhook secret resolver that returns the configured <paramref name="secret"/> for every subscription.
    /// </summary>
    /// <param name="secret">The secret to use.</param>
    /// <returns>A fixed webhook secret resolver function.</returns>
    public static ResolveWebhookSecret CreateFixedSecretResolver(WebhookSecret secret)
        => (subscription, ct) => ValueTask.FromResult<WebhookSecret?>(secret);
}
