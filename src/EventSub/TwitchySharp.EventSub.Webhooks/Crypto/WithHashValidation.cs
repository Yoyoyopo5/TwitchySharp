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

            return async (request, ct) =>
            {
                await using RecyclableMemoryStream cryptoStream = memoryManager.GetStream();
                await using TeeStream teeStream = new(request.Content, cryptoStream, leaveOpen: true);

                EventSubWebhookRequest nextRequest = request with { Content = new(teeStream) };
                EventSubWebhookRequest toVerify = request with { Content = new(cryptoStream) };

                return await next(nextRequest, ct)
                    .BindAsync((result, ct) =>
                    {
                        // We assume the next function read the tee stream to completion,
                        // which copies it into the cryptoStream, then we can reset it to position 0
                        // for the verification step. This prevents the consumer from needing to
                        // ensure that the request stream is buffered. i.e., we buffer it here.
                        cryptoStream.Position = 0;
                        return verifyHash(result.Subscription, toVerify, ct).MatchAsync(
                        // We have to wrap the unit validation from the verifier back into a WebhookRequestResult validation.
                        onError: (e, _) => ValueTask.FromResult(new Validation<WebhookRequestContent>(e)),
                        onValid: _ => ValueTask.FromResult<Validation<WebhookRequestContent>>(result),
                        ct);
                    }, ct);
            };
        });
}
