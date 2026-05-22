using System.Text;
using Microsoft.AspNetCore.Http;
using TwitchySharp.EventSub.Webhooks.Crypto;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.EventSub.Webhooks.Serialization;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

internal static class WebhookRequestResultExtensions
{
    public static IResult ToResult(this Validation<WebhookRequestContent> result)
        => result.Match(
            onError: error => error switch
            {
#if DEBUG
                // We won't send back error messages even in debug.
                // The actual message can already be accessed server-side via IEventSubHandler.OnError
                WebhookRequestDeserializer.DeserializationError e => Results.BadRequest(),
                WebhookHashVerifier.VerificationError e => Results.Unauthorized(),
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
