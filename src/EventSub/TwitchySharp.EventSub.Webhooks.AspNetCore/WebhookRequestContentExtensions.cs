using System.Text;
using Microsoft.AspNetCore.Http;
using TwitchySharp.EventSub.Webhooks.Crypto;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

/// <summary>
/// Extensions mapping <see cref="IWebhookRequestContent"/>.
/// </summary>
public static class WebhookRequestContentExtensions
{
    /// <summary>
    /// Convert a <see cref="Result{T}"/> of <see cref="IWebhookRequestContent"/>
    /// into its corresponding <see cref="IResult"/>.
    /// </summary>
    /// <remarks>
    /// In <c>DEBUG</c>, this can create error results (e.g. <c>BadRequest</c>).
    /// In <c>RELEASE</c>, a success result is always returned until the <see cref="IWebhookRequestContent"/> is an unmatched type (returns <c>InternalServerError</c>).
    /// </remarks>
    /// <param name="result">The webhook content as a <see cref="Result{T}"/> to match.</param>
    /// <returns>The HTTP response as an <see cref="IResult"/>.</returns>
    public static IResult ToHttpResult(this Result<IWebhookRequestContent> result)
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
