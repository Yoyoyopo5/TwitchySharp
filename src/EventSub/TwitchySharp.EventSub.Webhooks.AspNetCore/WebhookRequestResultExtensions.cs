using System.Text;
using Microsoft.AspNetCore.Http;
using TwitchySharp.EventSub.Webhooks.Deserialization;
using TwitchySharp.EventSub.Webhooks.Http;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

internal static class WebhookRequestResultExtensions
{
    public static IResult ToResult(this WebhookRequestResult result)
        => result switch
        {
            WebhookRequestResult.CallbackVerification callback => Results.Text(callback.Challenge, "text/plain", Encoding.UTF8),

            WebhookRequestResult.Error error =>
#if DEBUG
                // We won't send back error messages even in debug.
                // The actual message can already be accessed server-side via IEventSubHandler.OnError
                error.InnerError switch
                {
                    WebhookRequestDeserializer.DeserializationError e => Results.BadRequest(),
                    WebhookHashVerifier.VerificationError e => Results.Unauthorized(),
                    _ => Results.StatusCode(500)
                },
#else
                Results.Ok(),
#endif
            WebhookRequestResult.Notification => Results.Ok(),
            WebhookRequestResult.Revocation => Results.NoContent(),
            _ => Results.StatusCode(500)
        };
}
