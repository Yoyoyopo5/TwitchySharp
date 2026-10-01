using System.Text;
using Microsoft.AspNetCore.Http;
using TwitchySharp.EventSub.Webhooks.Crypto;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

internal static class WebhookRequestContentExtensions
{
    public static IResult ToResult(this Validation<IWebhookRequestContent> result)
        => result.Match(
            onError: error => error switch
            {
#if DEBUG
                // We won't send back error messages even in debug.
                // The actual message can already be accessed server-side via MapError
                ProcessWebhookRequestSerializationExtensions.NullPayloadError or
                ProcessWebhookRequestSerializationExtensions.DeserializationExceptionError or
                ProcessWebhookRequestSerializationExtensions.UnsupportedMessageTypeError => Results.BadRequest(),
                VerifyWebhookHashExtensions.VerificationFailedError e => Results.Unauthorized(),
                _ => Results.StatusCode(500)
#else
                _ => Results.Ok()
#endif
            },
            onValid: content => content switch
            {
                CallbackVerificationRequestContent callback => Results.Text(callback.Challenge, "text/plain", Encoding.UTF8),
                NotificationRequestContent => Results.Ok(),
                RevocationRequestContent => Results.NoContent(),
                _ => Results.StatusCode(500)
            });
}
