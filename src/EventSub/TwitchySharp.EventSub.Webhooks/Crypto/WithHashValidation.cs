using Microsoft.IO;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.Crypto;

public static partial class ProcessWebhookRequestExtensions
{
    private static readonly RecyclableMemoryStreamManager _memoryManager = new();
    public static ProcessWebhookRequest WithHashValidation(this ProcessWebhookRequest process, VerifyWebhookHash verifyHash)
        => process.With(next =>
        {
            RecyclableMemoryStreamManager memoryManager = _memoryManager;

            return (request, ct) =>
            {
                using RecyclableMemoryStream cryptoStream = memoryManager.GetStream();
                using TeeStream teeStream = new(request.Content, cryptoStream);

                EventSubWebhookRequest nextRequest = request with { Content = new(teeStream) };
                EventSubWebhookRequest toVerify = request with { Content = new(cryptoStream) };

                return next(nextRequest, ct)
                    .BindAsync((result, ct) => verifyHash(result.Subscription, toVerify, ct).MatchAsync(
                        // We have to wrap the unit validation from the verifier back into a WebhookRequestResult validation.
                        onError: (e, _) => ValueTask.FromResult(new Validation<WebhookRequestContent>(e)),
                        onValid: (_, _) => ValueTask.FromResult<Validation<WebhookRequestContent>>(result),
                        ct
                        ), ct);
            };
        });
}
